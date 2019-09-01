namespace Turbo.Plugins
{
    public interface IItemLocationChangedHandler : IPlugin
    {
        /// <summary>
        /// Called when an item's location is changed. This method is called during the data collection phase, which means no rendering is possible!
        /// </summary>
        void OnItemLocationChanged(IItem item, ItemLocation from, ItemLocation to);
    }
}