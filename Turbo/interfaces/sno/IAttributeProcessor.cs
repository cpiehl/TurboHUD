namespace Turbo.Plugins
{
    public interface IAttributeProcessor
    {
        IAttribute Attribute { get; }
        string Code { get; }
        byte CompactId { get; }
        uint Modifier { get; }
        double? Multiplier { get; }
        int RoundDecimals { get; }

        double ProcessDouble(double dv);
        int ProcessInt(int iv);
    }
}