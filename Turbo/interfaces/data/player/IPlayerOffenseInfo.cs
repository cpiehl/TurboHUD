using System;
using System.Collections.Generic;

namespace Turbo.Plugins
{
    public interface IPlayerOffenseInfo
    {
        float AttackSpeedPets { get; set; }
        float AttackSpeedPercent { get; set; }
        float WeaponSpeedMainHand { get; set; }
        float WeaponSpeedOffHand { get; set; }
        float AttackSpeedMainHand { get; set; }
        float AttackSpeedOffHand { get; set; }
        float AttackSpeedBonus { get; set; }
        float CritBase { get; set; }
        float CritDamage { get; set; }
        float AttackSpeed { get; set; }
        float CriticalHitChance { get; set; }

        float WeaponDamageIncreasedBySkills { get; set; }
        float BonusToElitesBase { get; set; }

        List<Tuple<ISnoPower, float>> PlainDamageBonuses { get; }

        float WeaponDamageMinPhysicalMainHand { get; set; }
        float WeaponDamageAddPhysicalMainHand { get; set; }
        float WeaponDamageMinElementalMainHand { get; set; }
        float WeaponDamageAddElementalMainHand { get; set; }
        float WeaponDamageMinPhysicalOffHand { get; set; }
        float WeaponDamageAddPhysicalOffHand { get; set; }
        float WeaponDamageMinElementalOffHand { get; set; }
        float WeaponDamageAddElementalOffHand { get; set; }

        float WeaponBaseDamageMinAmainHand { get; set; }
        float WeaponBaseDamageMinAoffHand { get; set; } // weapon's base damage without any increase
        float WeaponBaseDamageMaxAmainHand { get; set; }
        float WeaponBaseDamageMaxAoffHand { get; set; }
        float WeaponBaseDamageMinBmainHand { get; set; }
        float WeaponBaseDamageMinBoffHand { get; set; } // weapon's base damage + armor DMG + legacy bonus
        float WeaponBaseDamageMaxBmainHand { get; set; }
        float WeaponBaseDamageMaxBoffHand { get; set; }
        float DamageMin { get; set; }
        float DamageMax { get; set; }
        float WeaponDamageMainHand { get; set; }
        float WeaponDamageSecondHand { get; set; }
        float SheetDpsMainHand { get; set; }
        float SheetDpsOffHand { get; set; }
        float SheetDps { get; set; }

        bool MainHandIsActive { get; set; }

        float AreaDamageBonus { get; set; }

        float[] ElementalDamageBonus { get; }
        float HighestElementalDamageBonus { get; set; }

        float BonusToPhysical { get; }
        float BonusToFire { get; }
        float BonusToLightning { get; }
        float BonusToCold { get; }
        float BonusToPoison { get; }
        float BonusToArcane { get; }
        float BonusToHoly { get; }
        float BonusToElites { get; }
    }
}