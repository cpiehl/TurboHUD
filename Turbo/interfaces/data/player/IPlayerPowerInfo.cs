using System.Collections.Generic;

namespace Turbo.Plugins
{

    public interface IPlayerPowerInfo
    {

        IPlayerSkill HealthPotionSkill { get; }
        IEnumerable<IPlayerSkill> UsedSkills { get; } // Returns all used skills, in no particular order. No elements can be null.
        IEnumerable<IPlayerSkill> CurrentSkills { get; } // Returns all used skills, in no particular order. No elements can be null. If override is enabled (archon) then only the overridden skills are returned.
        IEnumerable<ISnoPower> UsedPassives { get; } // Returns all used passives, in no particular order. No elements can be null.
        IPlayerSkill[] SkillSlots { get; }
        ISnoPower[] PassiveSlots { get; }

        bool SkillOverrideActive { get; }

        IPlayerSkill GetUsedSkill(ISnoPower snoPower);

        IBarbarianPowerInfo UsedBarbarianPowers { get; }
        ICrusaderPowerInfo UsedCrusaderPowers { get; }
        IDemonHunterPowerInfo UsedDemonHunterPowers { get; }
        IMonkPowerInfo UsedMonkPowers { get; }
        INecromancerPowerInfo UsedNecromancerPowers { get; }
        IWitchDoctorPowerInfo UsedWitchDoctorPowers { get; }
        IWizardPowerInfo UsedWizardPowers { get; }

        ILegendaryPowerInfo UsedLegendaryPowers { get; }
        ILegendaryGemInfo UsedLegendaryGems { get; }

        IEnumerable<IBuff> AllBuffs { get; }
        IBuff GetBuff(string code);
        IBuff GetBuff(uint sno);
        bool BuffIsActive(uint sno);
        bool BuffIsActive(uint sno, int iconIndex);

        bool Frozen { get; set; }
        bool Rooted { get; set; }
        bool Stunned { get; set; }
        bool CantMove { get; } // = Frozen || Rooted || Stunned
    }

}