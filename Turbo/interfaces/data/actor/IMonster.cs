using System.Collections.Generic;

namespace Turbo.Plugins
{

    public interface IMonster: IActor
    {

        ISnoMonster SnoMonster { get; }

        bool IsAlive { get; }

        bool IsElite { get; }
        double MaxHealth { get; }
        double CurHealth { get; }
        ActorRarity Rarity { get; }

        IMonsterPack Pack { get; }

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
        bool Attackable { get; } // IsOnScreen && !Untargetable && !Invisible

        bool Illusion { get; } // equals to (IMonster.GetAttributeValue(Hud.Sno.Attributes.Power_Buff_0_Visual_Effect_None, 264185, 0) != 0)

        // note: these will be removed later
        bool Palmed { get; }
        bool Haunted { get; }
        bool MarkedForDeath { get; }
        bool Locust { get; }
        bool Strongarmed { get; }
        bool Phoenixed { get; }

        IEnumerable<ISnoMonsterAffix> AffixSnoList { get; }
    }
}