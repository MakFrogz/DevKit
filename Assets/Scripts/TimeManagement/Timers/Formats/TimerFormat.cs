using UnityEngine;

namespace TimeManagement.Timers.Formats
{
    public abstract class TimerFormat : ScriptableObject
    {
        public abstract string Format(float seconds);
    }
}