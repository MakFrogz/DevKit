using System;
using UnityEngine;

namespace TimeManagement.Timers.Formats
{
    [CreateAssetMenu(menuName = "Timer/Formats/Seconds Milliseconds")]
    public class SecondsMillisecondsFormat : TimerFormat
    {
        public override string Format(float seconds)
        {
            return TimeSpan.FromSeconds(seconds).ToString(@"ss\:ff");
        }
    }
}