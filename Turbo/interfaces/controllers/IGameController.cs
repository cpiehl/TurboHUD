using System.Collections.Generic;

namespace Turbo.Plugins
{
    public interface IGameController
    {
        GameDifficulty GameDifficulty { get; }
        long CurrentRealTimeMilliseconds { get; }
        int CurrentGameTick { get; }
        bool IsLoading { get; }
        bool IsPaused { get; }        bool IsInGame { get; }
        bool IsInTown { get; }
        MapMode MapMode { get; }
        SpecialArea SpecialArea { get; }
        string ServerIpAddress { get; }

        double CurrentQuestProgress { get; }
        double MaxQuestProgress { get; }
        double RiftPercentage { get; } // same as CurrentQuestProgress / MaxQuestProgress * 100.0d;
        int CurrentTimedEventStartTick { get; }
        int CurrentTimedEventEndTick { get; }
        int CurrentTimedEventEndTickMod { get; }

        double AverageLatency { get; }
        double CurrentLatency { get; }

        int CurrentAct { get; }

        IPlayer Me { get; }

        IEnumerable<IQuest> Quests { get; }
        IEnumerable<IQuest> Bounties { get; }

        IEnumerable<IItem> Items { get; }

        IEnumerable<IPlayer> Players { get; }

        IEnumerable<IHeadStone> HeadStones { get; }

        int NumberOfPlayersInGame { get; }

        IEnumerable<IActor> Actors { get; }

        IEnumerable<IMonster> Monsters { get; }
        IEnumerable<IMonster> AliveMonsters { get; }
        IEnumerable<IMonsterPack> MonsterPacks { get; }
        bool IsEliteOnScreen { get; }
        bool IsEliteNearby { get; }
        bool IsGoblinOnScreen { get; }
        MonsterPriority MaxPriorityOnScreen { get; }

        IEnumerable<IShrine> Shrines { get; }
        IEnumerable<IPortal> Portals { get; }
        IEnumerable<IClickableActor> NormalChests { get; }
        IEnumerable<IClickableActor> ResplendentChests { get; }
        IEnumerable<IClickableActor> Doors { get; }

        IEnumerable<ISceneHint> SceneHints { get; }
        IEnumerable<IBanner> Banners { get; }
        IEnumerable<IMarker> Markers { get; }


        IEnumerable<IWaypoint> ActMapWaypoints { get; }
        BountyAct ActMapCurrentAct { get; }

        int InventorySpaceUsed { get; }

        IMonster SelectedMonster1 { get; }
        IMonster SelectedMonster2 { get; }

        double ExperiencePerHourToday { get; } // same as CurrentHeroTodayStatTracker.GainedExperiencePerHour

        // Run stats (Run tracker, and area trackers) will be exposed later.

        IStatTracker CurrentAccountTotalOnCurrentDifficulty { get; }
        IStatTracker CurrentAccountYesterdayOnCurrentDifficulty { get; }
        IStatTracker CurrentAccountTodayOnCurrentDifficulty { get; }

        IStatTracker CurrentHeroTotal { get; }
        IStatTracker CurrentHeroYesterday { get; }
        IStatTracker CurrentHeroToday { get; }

        IStatTracker CurrentHeroTotalOnCurrentDifficulty { get; }
        IStatTracker CurrentHeroYesterdayOnCurrentDifficulty { get; }
        IStatTracker CurrentHeroTodayOnCurrentDifficulty { get; }

    }
}