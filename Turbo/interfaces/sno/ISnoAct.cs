using System.Collections.Generic;

namespace Turbo.Plugins
{
    public interface ISnoAct
    {
        uint Sno { get; }
        int Index { get; }
        List<ISnoQuest> MainQuests { get; }
        List<ISnoQuest> Bounties { get; }
    }
}