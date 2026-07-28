using System;
using UnityEngine;

namespace TimeManagement.Timers
{
    public abstract class Timer
    {
        public event Action OnStart;
        public event Action<float> OnTick; 
        public event Action OnStop;
        
        private float _initialTime;

        public float CurrentTime { get; protected set; }
        public bool IsRunning { get; private set; }
        public float Progress => _initialTime <= 0 ? 0f : Mathf.Clamp01(CurrentTime / _initialTime);
        public abstract bool IsFinished { get; }

        public Timer(float initialTime)
        {
            _initialTime = initialTime;
            IsRunning = false;
        }

        public void Start()
        {
            if (IsRunning)
            {
                return;
            }

            CurrentTime = _initialTime;
            IsRunning = true;
            OnStart?.Invoke();
        }

        public void Tick(float deltaTime)
        {
            if (!IsRunning)
            {
                return;
            }

            UpdateTime(deltaTime);

            OnTick?.Invoke(CurrentTime);

            if (!IsFinished)
            {
                return;
            }
            Stop();
        }

        protected abstract void UpdateTime(float deltaTime);

        public void Stop()
        {
            if (!IsRunning)
            {
                return;
            }
            IsRunning = false;
            OnStop?.Invoke();
        }

        public void Reset(float initialTime)
        {
            _initialTime = initialTime;
            Reset();
        }

        public virtual void Reset()
        {
            CurrentTime = _initialTime;
        }
    }
}
