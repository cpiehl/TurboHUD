using System.Collections.Generic;

namespace Turbo.Plugins
{
    public interface ISnoController
    {
        IAttributeList Attributes { get; }

        ISnoPowerList SnoPowers { get; }
        ISnoPower GetSnoPower(uint sno);
        IEnumerable<ISnoPower> AllSnoPower { get; }

        ISnoQuestList SnoQuests { get; }
        ISnoQuest GetSnoQuest(uint sno);
        IEnumerable<ISnoQuest> AllSnoQuest { get; }

        ISnoItemList SnoItems { get; }

        long TotalParagonExperienceRequired(uint paragonLevel);
    }
}