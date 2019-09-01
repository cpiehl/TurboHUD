namespace Turbo.Plugins
{
    public interface IBuff
    {
        IPlayer Player { get; }
        ISnoPower SnoPower { get; }
        IWatch FirstActive { get; }
        IWatch LastActive { get; }
        bool Active { get; }
        int[] IconCounts { get; }
        double[] TimeElapsedSeconds { get; }
        double[] TimeLeftSeconds { get; }
    }
}