using System;

namespace Turbo.Plugins
{
    public interface IHero
    {
        uint Id { get; }
        string Name { get; }
        string BattleTag { get; }
        IHeroClassDefinition ClassDefinition { get; }
        ulong AccountIdHi { get; }
        ulong AccountIdLo { get; }
        int Flags { get; set; }
        int Level { get; set; }
        int ParagonLevel { get; }
        int PlayedSeconds { get; }
        bool Hardcore { get; }
        bool IsMale { get; }
        int Season { get; }
        bool Seasonal { get; }
        DateTime CreatedDT { get; }
        long LastPlayed { get; }
        DateTime LastPlayedDT { get; }
        ISnoQuestStep QuestStep { get; }
    }
}