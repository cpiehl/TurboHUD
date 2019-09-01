using System.Collections.Generic;

namespace Turbo.Plugins
{
    public interface IPlayer : IActor
    {
        bool IsMe { get; }
        bool IsInGame { get; }
        IHeadStone HeadStone { get; }
        bool HasValidActor { get; }

        IHero Hero { get; } // only available for the local player
        bool HeroIsHardcore { get; } // only available for the local player
        bool HeroIsMale { get; } // only available for the local player
        int GetSetItemCount(uint setId); // only available for the local player

        uint HeroId { get; }
        string HeroName { get; }
        IHeroClassDefinition HeroClassDefinition { get; }

        string BattleTagAbovePortrait { get; }
        bool CoordinateKnown { get; }

        ISnoArea SnoArea { get; }

        int Index { get; }
        int PortraitIndex { get; }
        IUiElement PortraitUiElement { get; }

        bool IsCubed(ISnoItem snoItem);
        ISnoItem CubeSnoItem1 { get; set; }
        ISnoItem CubeSnoItem2 { get; set; }
        ISnoItem CubeSnoItem3 { get; set; }
        IEnumerable<ISnoItem> CubedItems { get; }

        uint CurrentLevelNormal { get; }
        uint CurrentLevelNormalCap { get; }
        uint CurrentLevelParagon { get; }
        double CurrentLevelParagonDouble { get; }

        IPlayerOffenseInfo Offense { get; }
        IPlayerStatInfo Stats { get; }
        IPlayerDefenseInfo Defense { get; }
        IPlayerDamageInfo Damage { get; }
        IPlayerMaterialInfo Materials { get; }
        IPlayerPowerInfo Powers { get; }

        bool InCombat { get; }

        int LoopingAnimationStartTick { get; set; }
        int LoopingAnimationEndTick { get; set; }

        bool IsInTown { get; }
        bool InGreaterRift { get; }
        uint InGreaterRiftRank { get; }

        bool IsDead { get; }
        IWatch LastDied { get; }
        bool IsDeadSafeCheck { get; }

        AnimSnoEnum Animation { get; }
        AcdAnimationState AnimationState { get; }

        long ParagonTotalExp { get; }
        long ParagonExpInThisLevel { get; }
        long ParagonExpToNextLevel { get; }
        float BonusPoolPercent { get; }
        long BonusPoolRemaining { get; }

        int HighestSoloRiftLevel { get; }
        int HighestHeroSoloRiftLevel { get; }

        int InventorySpaceTotal { get; }

        int ParagonPointsAvailableTotal { get; }
        int[] ParagonPointsAvailable { get; }

        float GetPowerTagValue(ISnoPower snoPower, uint tagId);

        IPlayerArmorySet[] ArmorySets { get; }
    }
}