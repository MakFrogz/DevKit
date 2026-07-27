using System;
using FiniteStateMachine.API;

namespace FiniteStateMachine.Predicates
{
    public class FuncPredicate : IPredicate
    {
        private readonly Func<bool> _predicate;

        public FuncPredicate(Func<bool> predicate)
        {
            _predicate = predicate;
        }
        
        public bool Evaluate() => _predicate.Invoke();
    }
}