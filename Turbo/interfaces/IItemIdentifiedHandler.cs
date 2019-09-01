namespace Turbo.Plugins
{
    public interface IItemIdentifiedHandler : IPlugin
    {
        /// <summary>
        /// Called when an item is identified. This method is called during the data collection phase, which means no rendering is possible!
        /// </summary>
        void OnItemIdentified(IItem item);
    }
}