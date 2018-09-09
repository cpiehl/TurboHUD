namespace Turbo.Plugins
{
    public interface IItemPerfection
    {
        ISnoItemMod SnoItemMod { get; }
        IAttribute Attribute { get; }
        uint Modifier { get; }
        double Min { get; }
        double Max { get; }
        double Cur { get; }

        string ToString();
    }
}