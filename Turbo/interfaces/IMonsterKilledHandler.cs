namespace Turbo.Plugins
{
    public interface IMonsterKilledHandler : IPlugin
    {
        /// <summary>
        /// Called when a monster is killed. This method is called during the data collection phase, which means no rendering is possible!
        /// </summary>
        void OnMonsterKilled(IMonster monster);
    }
}