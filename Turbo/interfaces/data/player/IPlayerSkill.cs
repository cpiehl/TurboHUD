namespace Turbo.Plugins
{
    public interface IPlayerSkill
    {
        IPlayer Player { get; }
        ISnoPower SnoPower { get; }
        ISnoPower OverrideSnoPower { get; }
        ISnoPower CurrentSnoPower { get; } // if override is enabled, then returns OverrideSnoPower, otherwise returns SnoPower

        byte Rune { get; }
        string RuneNameLocalized { get; }
        string RuneNameEnglish { get; }
        ActionKey Key { get; }

        bool IsOnCooldown { get; }
        int CooldownFinishTick { get; }
        int CooldownStartTick { get; }

        int ElementalType { get; }
        int WeaponDamageMultiplier { get; }
        float DotSeconds { get; }

        float ResourceCost { get; }
        float GetResourceRequirement();
        float GetResourceRequirement(float baseRequirement);
        float CalculateCooldown(float baseCooldown);

        float DamageBonus { get; }
        float ElementalDamageBonus { get; }

        bool BuffIsActive { get; }
        IBuff Buff { get; }

        int Charges { get; }    }
}