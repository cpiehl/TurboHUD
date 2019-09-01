namespace Turbo.Plugins
{
    public interface ISkillEffect : IActor
    {
        ISkillEffectRule SkillEffectRule { get; }
    }
}