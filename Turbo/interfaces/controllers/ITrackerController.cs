namespace Turbo.Plugins
{
    public interface ITrackerController
    {
        IStatTracker Session { get; }
        IStatTracker SessionAlwaysRunning { get; } // (ABS)

        IStatTracker CurrentAccountTotal { get; }
        IStatTracker CurrentAccountLastMonth { get; } // only available in menu
        IStatTracker CurrentAccountLastWeek { get; } // only available in menu
        IStatTracker CurrentAccountYesterday { get; }
        IStatTracker CurrentAccountToday { get; }
    }
}