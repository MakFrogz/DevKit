using System;
using UnityEngine;

namespace TimeManagement.Timers.Formats
{
    [CreateAssetMenu(menuName = "Timer/Formats/Minutes Seconds Format")]
    public class MinutesSecondsFormat : TimerFormat
    {
        public override string Format(float seconds)
        {
            return TimeSpan.FromSeconds(seconds).ToString(@"mm\:ss");
        }
    }
}