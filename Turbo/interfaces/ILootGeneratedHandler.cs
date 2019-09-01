namespace Turbo.Plugins
{
    public interface ILootGeneratedHandler : IPlugin
    {
        /// <summary>
        /// Called when an item is dropped to the ground OR gambled. This method is called during the data collection phase, which means no rendering is possible!
        /// </summary>
        void OnLootGenerated(IItem item, bool gambled);
    }
}