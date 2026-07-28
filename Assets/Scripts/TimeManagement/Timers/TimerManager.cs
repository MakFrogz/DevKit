using System.Collections.Generic;
using TimeManagement.Timers.API;
using UnityEngine;

namespace TimeManagement.Timers
{
    public class TimerManager : MonoBehaviour, ITimerManager
    {
        private readonly HashSet<Timer> _timers = new();

        private void Update()
        {
            foreach (var timer in _timers)
            {
                timer.Tick(Time.deltaTime);
            }
        }

        public void RegisterTimer(Timer timer)
        {
            _timers.Add(timer);
        }

        public void UnregisterTimer(Timer timer)
        {
            _timers.Remove(timer);
        }
    }
}