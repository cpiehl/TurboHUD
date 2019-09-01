namespace Turbo.Plugins
{
    public interface IItemStat
    {
        string Id { get; }
        IAttribute Attribute { get; }
        uint Modifier { get; }
        IAttributeProcessor Processor { get; }

        double DoubleValue { get; } // always has a value, even if the attribute value type is _int
        int? IntegerValue { get; } // contains the value as an integer - only if the attribute value type is _int
        string StringValue { get; } // there is only one stat (Id = "name") which has a StringValue
    }
}