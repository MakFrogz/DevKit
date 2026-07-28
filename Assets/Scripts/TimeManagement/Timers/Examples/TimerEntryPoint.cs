using UnityEngine;

namespace TimeManagement.Timers.Examples
{
    public class TimerEntryPoint : MonoBehaviour
    {
        [SerializeField]
        private float _seconds;
        
        [SerializeField]
        private TimerViewTest _timerView;

        [SerializeField]
        private TimerManager _timerManager;
        
        private void Start()
        {
            var countdownTimer = new CountdownTimer(_seconds);
            _timerView.SetTimer(countdownTimer);
            _timerManager.RegisterTimer(countdownTimer);
            countdownTimer.Start();
        }
    }
}