namespace Turbo.Plugins
{
    public interface IKeyEventHandler : IPlugin
    {
        /// <summary>
        /// Called when the player pressed or released a key. This method is called during the data collection phase, which means no rendering is possible!
        /// </summary>
        void OnKeyEvent(IKeyEvent keyEvent);
    }
}