using System.Collections.Generic;

namespace Turbo.Plugins
{

    public interface IPlayerArmorySet
    {

        IPlayer Player { get; }
        int Index { get; }
        string Name { get; }
        IEnumerable<uint> ItemAnnIds { get; }

        bool ContainsItem(IItem item);
    }

}