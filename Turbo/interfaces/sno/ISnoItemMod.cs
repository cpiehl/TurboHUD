namespace Turbo.Plugins
{
    public interface ISnoItemMod
    {
        IAttribute Attribute { get; }
        uint Modifier { get; }
        float Min { get; }
        float Max { get; }
        ISnoItemAffix Affix { get; }
    }
}