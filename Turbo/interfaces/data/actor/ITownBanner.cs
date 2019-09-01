namespace Turbo.Plugins
{
    public interface ITownBanner : IActor
    {
        int Index { get; }
        bool Usable { get; }
    }
}