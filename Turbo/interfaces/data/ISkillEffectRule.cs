namespace Turbo.Plugins
{
    public interface ISkillEffectRule
    {
        SkillEffectType Type { get; }
        ISnoActor SnoActor { get; }
        float Radius { get; }
    }
}