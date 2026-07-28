namespace TimeManagement.Timers
{
    public class CountdownTimer : Timer
    {
        public override bool IsFinished => CurrentTime <= 0;
        
        public CountdownTimer(float initialTime) : base(initialTime) { }

        protected override void UpdateTime(float deltaTime)
        {
            if (IsRunning && CurrentTime > 0)
            {
                CurrentTime -= deltaTime;
            }

            if (IsRunning && CurrentTime <= 0)
            {
                Stop();
            }
        }
    }
}