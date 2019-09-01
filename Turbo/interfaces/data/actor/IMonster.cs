using System.Collections.Generic;

namespace Turbo.Plugins
{
    public interface IMonster : IActor
    {
        ISnoMonster SnoMonster { get; }

        bool IsAlive { get; }

        bool IsElite { get; }
        double MaxHealth { get; }
        double CurHealth { get; }
        ActorRarity Rarity { get; }

        IMonsterPack Pack { get; }

        // animation is only updated when SnoMonster.Priority >= MonsterPriority.Keywarden or IsElite == true due to the high cost of reading
        AnimSnoEnum Animation { get; }
        AcdAnimationState AnimationState { get; }

        bool IsQuestMonster { get; }

        float DotDpsApplied { get; }
        bool Frozen { get; }
        bool Chilled { get; }
        bool Slow { get; }
        bool Stunned { get; }
        bool Burrowed { get; }
        bool Invulnerable { get; }
        bool Hidden { get; }
        bool Stealthed { get; }
        bool Invisible { get; }
        bool Blind { get; }
        bool Bleeding { get; }
        bool Attackable { get; } // IsOnScreen && !Untargetable && !Invisible

        bool Illusion { get; } // equals to (IMonster.GetAttributeValue(Hud.Sno.Attributes.Power_Buff_0_Visual_Effect_None, 264185, 0) != 0)

        bool Palmed { get; }
        bool Haunted { get; }
        bool MarkedForDeath { get; }
        bool Locust { get; }
        bool Strongarmed { get; }
        bool Phoenixed { get; }
        bool Piranhas { get; }
        bool Cursed { get; }

        IEnumerable<ISnoMonsterAffix> AffixSnoList { get; }
    }
}