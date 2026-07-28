using System;
using TimeManagement.Timers.Formats;
using TMPro;
using UnityEngine;

namespace TimeManagement.Timers.Examples
{
    public class TimerViewTest : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _timerText;
        
        [SerializeField]
        private TimerFormat _format;
        
        private Timer _timer;
        
        public void SetTimer(Timer timer)
        {
            _timer = timer;
            _timer.OnTick += SetTimerText;
        }

        private void SetTimerText(float value)
        {
            //_timerText.text = $"{TimeSpan.FromSeconds(value):ss}:{TimeSpan.FromSeconds(value):ff}";
            _timerText.text = _format.Format(value);
        }

        private void OnDestroy()
        {
            _timer.OnTick -= SetTimerText;
        }
    }
}