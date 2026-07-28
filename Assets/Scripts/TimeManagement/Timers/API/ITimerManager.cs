namespace TimeManagement.Timers.API
{
    public interface ITimerManager
    {
        void RegisterTimer(Timer timer);
        void UnregisterTimer(Timer timer);
    }
}