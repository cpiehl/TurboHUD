namespace Turbo.Plugins
{

    public interface IDemonHunterPowerInfo
    {

        IPlayerSkill Bolas { get; } // 77552 - Bolas
        IPlayerSkill Caltrops { get; } // 129216 - Caltrops
        IPlayerSkill Chakram { get; } // 129213 - Chakram
        IPlayerSkill ClusterArrow { get; } // 129214 - Cluster Arrow
        IPlayerSkill Companion { get; } // 365311 - Companion
        IPlayerSkill ElementalArrow { get; } // 131325 - Elemental Arrow
        IPlayerSkill EntanglingShot { get; } // 361936 - Entangling Shot
        IPlayerSkill EvasiveFire { get; } // 377450 - Evasive Fire
        IPlayerSkill FanOfKnives { get; } // 77546 - Fan of Knives
        IPlayerSkill Grenades { get; } // 86610 - Grenade
        IPlayerSkill HungeringArrow { get; } // 129215 - Hungering Arrow
        IPlayerSkill Impale { get; } // 131366 - Impale
        IPlayerSkill MarkedForDeath { get; } // 130738 - Marked for Death
        IPlayerSkill Multishot { get; } // 77649 - Multishot
        IPlayerSkill Preparation { get; } // 129212 - Preparation
        IPlayerSkill RainOfVengeance { get; } // 130831 - Rain of Vengeance
        IPlayerSkill RapidFire { get; } // 131192 - Rapid Fire
        IPlayerSkill Sentry { get; } // 129217 - Sentry
        IPlayerSkill ShadowPower { get; } // 130830 - Shadow Power
        IPlayerSkill SmokeScreen { get; } // 130695 - Smoke Screen
        IPlayerSkill SpikeTrap { get; } // 75301 - Spike Trap
        IPlayerSkill Strafe { get; } // 134030 - Strafe
        IPlayerSkill Vault { get; } // 111215 - Vault
        IPlayerSkill Vengeance { get; } // 302846 - Vengeance
        ISnoPower Ambush { get; } // 352920 - Ambush
        ISnoPower Archery { get; } // 209734 - Archery
        ISnoPower Awareness { get; } // 324770 - Awareness
        ISnoPower Ballistics { get; } // 155723 - Ballistics
        ISnoPower Brooding { get; } // 210801 - Brooding
        ISnoPower CullTheWeak { get; } // 155721 - Cull the Weak
        ISnoPower CustomEngineering { get; } // 208610 - Custom Engineering
        ISnoPower Grenadier { get; } // 208779 - Grenadier
        ISnoPower HotPursuit { get; } // 155725 - Hot Pursuit
        ISnoPower Leech { get; } // 439525 - Leech
        ISnoPower NightStalker { get; } // 218350 - Night Stalker
        ISnoPower NumbingTraps { get; } // 218398 - Numbing Traps
        ISnoPower Perfectionist { get; } // 155722 - Perfectionist
        ISnoPower Sharpshooter { get; } // 155715 - Sharpshooter
        ISnoPower SingleOut { get; } // 338859 - Single Out
        ISnoPower SteadyAim { get; } // 164363 - Steady Aim
        ISnoPower TacticalAdvantage { get; } // 218385 - Tactical Advantage
        ISnoPower ThrillOfTheHunt { get; } // 211225 - Thrill of the Hunt
    }

}
