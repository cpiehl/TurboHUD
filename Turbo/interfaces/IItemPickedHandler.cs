namespace Turbo.Plugins
{
    public interface IItemPickedHandler : IPlugin
    {
        /// <summary>
        /// Called when an item is picked up from the ground. This method is called during the data collection phase, which means no rendering is possible!
        /// </summary>
        void OnItemPicked(IItem item);
    }
}