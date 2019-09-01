namespace Turbo.Plugins
{
    public interface ISkillCooldownHandler : IPlugin
    {
        /// <summary>
        /// Called when a used skill's cooldown is started or expired. This method is called during the data collection phase, which means no rendering is possible!
        /// </summary>
        void OnCooldown(IPlayerSkill playerSkill, bool expired);
    }
}