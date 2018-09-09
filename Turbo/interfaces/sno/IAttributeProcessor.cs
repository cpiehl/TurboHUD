namespace Turbo.Plugins
{

    public interface IAttributeProcessor
    {
        IAttribute Attribute { get; }
        string Code { get; }
        byte CompactId { get; }
        uint Modifier { get; }
        float Multiplier { get; }
        int RoundDecimals { get; }

        double Process(double value);
    }

}