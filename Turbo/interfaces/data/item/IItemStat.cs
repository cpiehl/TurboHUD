namespace Turbo.Plugins
{
    public interface IItemStat
    {
        string Id { get; }
        IAttribute Attribute { get; }
        uint Modifier { get; }
        IAttributeProcessor Processor { get; }
        object Value { get; }
    }
}