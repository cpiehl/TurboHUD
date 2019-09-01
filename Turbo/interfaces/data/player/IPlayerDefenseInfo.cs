namespace Turbo.Plugins
{
    public interface IPlayerDefenseInfo
    {
        float EhpCur { get; set; }
        float EhpMax { get; set; }
        float HealthCur { get; set; }
        float HealthMax { get; set; }
        float HealthPct { get; set; }
        float CurShield { get; set; }

        float Armor { get; set; }
        float ResPhysical { get; set; }
        float ResCold { get; set; }
        float ResFire { get; set; }
        float ResLightning { get; set; }
        float ResPoison { get; set; }
        float ResArcane { get; set; }
        float ResLowest { get; set; }
        float ResAverage { get; set; }

        float LifeBonus { get; set; }
        float LifeRegen { get; set; }
        float LifeOnHit { get; set; }
        float LifeOnKill { get; set; }
        float Thorns { get; set; }
        float GlobeBonusHealth { get; set; }

#pragma warning disable IDE1006 // Naming Styles
        float DRClass { get; set; }
        float drArmor { get; set; }
        float drResist { get; set; }
        float drCombined { get; set; }
        float DRRanged { get; set; }
        float DRMelee { get; set; }
        float DRElite { get; set; }
#pragma warning restore IDE1006 // Naming Styles

        float[] DamageReductionFromType { get; }
        float AverageDamageReductionFromType { get; set; }
        float CCReduction { get; set; }
        float DamageReduction { get; set; }

        double CurrentDamageTakenPerSecond { get; set; }
        double CurrentHealingPerSecond { get; set; }

        double CurrentEffectiveHealingPercent { get; }
    }
}