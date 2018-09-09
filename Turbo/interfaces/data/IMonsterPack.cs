using System.Collections.Generic;

namespace Turbo.Plugins
{
    public interface IMonsterPack
    {
        IWatch LastActive { get; }
        bool IsFullChampionPack { get; }
        ISnoMonster LeadSnoMonster { get; }
        ISnoMonster MinionSnoMonster { get; }

        IEnumerable<ISnoMonsterAffix> AffixSnoList { get; }
        IEnumerable<IMonster> MonstersAlive { get; }
        IEnumerable<IMonster> MonstersKilled { get; }
        IEnumerable<IMonster> MonstersUnknown { get; }
    }
}