namespace Turbo.Plugins
{
    public interface IStatTracker : IStatTrackerCore
    {
        string Id { get; }
        string Title { get; }
        ISnoArea SnoArea { get; }
        bool AlwaysRunning { get; }

        bool IsMainTimerRunning { get; }
        bool IsTownTimerRunning { get; }
    }
}