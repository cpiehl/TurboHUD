namespace Turbo.Plugins
{
    public interface IPortalFoundHandler : IPlugin
    {
        /// <summary>
        /// Called when a new portal is detected. This method is called during the data collection phase, which means no rendering is possible!
        /// </summary>
        void OnPortalFound(IPortal portal);
    }
}