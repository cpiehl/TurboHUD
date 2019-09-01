namespace Turbo.Plugins
{
    public interface IPerfCounter
    {
        double LastCount { get; }
        double LastValue { get; }
    }
}