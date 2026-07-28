using System;
using UnityEngine;

namespace TimeManagement.Timers.Formats
{
    [CreateAssetMenu(menuName = "Timer/Formats/Hours Minutes Seconds Format")]
    public class HoursMinutesSecondsFormat : TimerFormat
    {
        public override string Format(float seconds)
        {
            return TimeSpan.FromSeconds(seconds).ToString(@"hh\:mm\:ss");
        }
    }
}