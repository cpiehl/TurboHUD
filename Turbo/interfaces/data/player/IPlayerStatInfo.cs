namespace Turbo.Plugins
{
    public interface IPlayerStatInfo
    {
        float ResourceMaxArcane { get; set; }
        float ResourceMaxSpirit { get; set; }
        float ResourceMaxFury { get; set; }
        float ResourceMaxMana { get; set; }
        float ResourceMaxHatred { get; set; }
        float ResourceMaxDiscipline { get; set; }
        float ResourceMaxWrath { get; set; }
        float ResourceMaxEssence { get; set; }
        float ResourceCurArcane { get; set; }
        float ResourceCurSpirit { get; set; }
        float ResourceCurFury { get; set; }
        float ResourceCurMana { get; set; }
        float ResourceCurHatred { get; set; }
        float ResourceCurDiscipline { get; set; }
        float ResourceCurWrath { get; set; }
        float ResourceCurEssence { get; set; }
        float ResourcePctArcane { get; set; }
        float ResourcePctSpirit { get; set; }
        float ResourcePctFury { get; set; }
        float ResourcePctMana { get; set; }
        float ResourcePctHatred { get; set; }
        float ResourcePctDiscipline { get; set; }
        float ResourcePctWrath { get; set; }
        float ResourcePctEssence { get; set; }
        float ResourceRegArcane { get; set; }
        float ResourceRegSpirit { get; set; }
        float ResourceRegFury { get; set; }
        float ResourceRegMana { get; set; }
        float ResourceRegHatred { get; set; }
        float ResourceRegDiscipline { get; set; }
        float ResourceRegWrath { get; set; }
        float ResourceRegEssence { get; set; }

        float CooldownReduction { get; set; }
        float ResourceCostReduction { get; set; }

        float ResourceCurPri { get; set; }
        float ResourceCurSec { get; set; }
        float ResourceMaxPri { get; set; }
        float ResourceMaxSec { get; set; }
        float ResourcePctPri { get; set; }
        float ResourcePctSec { get; set; }
        float ResourceRegPri { get; set; }
        float ResourceRegSec { get; set; }

        float MainStat { get; set; }
        float Strength { get; set; }
        float Dexterity { get; set; }
        float Intelligence { get; set; }
        float Vitality { get; set; }

        float MoveSpeed { get; set; }
        float MoveSpeedBonus { get; set; }

        float MagicFind { get; set; }
        float GoldFind { get; set; }
        float ExperiencePercentBonus { get; set; }
        float ExpOnKill { get; set; }
        float ExpOnKillNoPenalty { get; set; }
        float ExperienceOnKillBonus { get; }
        float PickupRange { get; set; }
    }
}