using System.Collections.Generic;

namespace Turbo.Plugins
{
    public interface IAttribute
    {
        uint GetId(uint modifier);
        string GetDescription(uint modifier);

        string Code { get; }
        uint Index { get; }
        AttributeValueType ValueType { get; }
        IEnumerable<IAttributeProcessor> Processors { get; }
        bool OrderIndexPrimary { get; }

        int GetModLowestOrderIndex(int mod);
    }
}