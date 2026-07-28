namespace TimeManagement.Timers
{
    public class StopwatchTimer : Timer
    {
        public override bool IsFinished => false;

        public StopwatchTimer() : base(0f) { }

        protected override void UpdateTime(float deltaTime)
        {
            if (!IsRunning)
            {
                return;
            }
            CurrentTime += deltaTime;
        }
    }
}