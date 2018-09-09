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
        float LifeBonus { get; set; }
        float ResPhysical { get; set; }
        float ResCold { get; set; }
        float ResFire { get; set; }
        float ResLightning { get; set; }
        float ResPoison { get; set; }
        float ResArcane { get; set; }
        float ResLowest { get; set; }
        float ResAverage { get; set; }
        float LifeRegen { get; set; }

        float DRClass { get; set; }
        float drArmor { get; set; }
        float drResist { get; set; }
        float drCombined { get; set; }
        float[] DamageReductionFromType { get; }
        float AverageDamageReductionFromType { get; set; }
        float CCReduction { get; set; }
        float DamageReduction { get; set; }
        float DRRanged { get; set; }
        float DRMelee { get; set; }
        float DRElite { get; set; }

        long CurrentDamageTakenPerSecond { get; set; }
        long CurrentHealingPerSecond { get; set; }
        double CurrentEffectiveHealingPercent { get; }
    }

}