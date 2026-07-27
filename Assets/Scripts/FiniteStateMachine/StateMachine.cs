using System;
using System.Collections.Generic;
using FiniteStateMachine.API;

namespace FiniteStateMachine
{
    public class StateMachine
    {
        private StateNode _currentState;
        private Dictionary<Type, StateNode> _nodes = new Dictionary<Type, StateNode>();
        private HashSet<ITransition> _anyTransitions = new HashSet<ITransition>();

        public void Update()
        {
            var transition = GetTransition();
            if (transition != null)
            {
                ChangeState(transition.To);
            }
            
            _currentState?.State?.Update();
        }

        public void FixedUpdate()
        {
            _currentState?.State?.FixedUpdate();
        }

        public void SetState(IState state)
        {
            _currentState =  _nodes[state.GetType()];
            _currentState?.State?.OnEnter();
        }

        public void AddTransition(IState from, IState to, IPredicate condition)
        {
            var nodeFrom = AddOrGetNode(from);
            var nodeTo = AddOrGetNode(to);
            nodeFrom.AddTransition(nodeTo.State, condition);
        }

        public void AddAnyTransition(IState to, IPredicate condition)
        {
            var nodeTo = AddOrGetNode(to);
            var transition = new Transition(nodeTo.State, condition);
            _anyTransitions.Add(transition);
        }

        private void ChangeState(IState state)
        {
            if (state == _currentState.State)
            {
                return;
            }
            
            var previousState = _currentState.State;
            var nextState = _nodes[state.GetType()].State;
            
            previousState?.OnExit();
            nextState?.OnEnter();

            _currentState = _nodes[state.GetType()];
        }

        private ITransition GetTransition()
        {
            foreach (var transition in _anyTransitions)
            {
                if (transition.Condition.Evaluate())
                {
                    return transition;
                }
            }

            foreach (var transition in _currentState.Transitions)
            {
                if (transition.Condition.Evaluate())
                {
                    return transition;
                }
            }
            
            return null;
        }

        private StateNode AddOrGetNode(IState state)
        {
            var type = state.GetType();
            if (_nodes.TryGetValue(type, out var node))
            {
                return node;
            }
            
            node = new StateNode(state);
            _nodes[type] = node;
            return node;
        }
        
        private class StateNode
        {
            public IState State { get; }
            public HashSet<ITransition> Transitions { get; }

            public StateNode(IState state)
            {
                State = state;
                Transitions = new HashSet<ITransition>();
            }

            public void AddTransition(IState to, IPredicate condition)
            {
                Transitions.Add(new Transition(to, condition));
            }
        }
    }
}