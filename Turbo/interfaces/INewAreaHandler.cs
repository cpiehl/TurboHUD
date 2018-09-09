namespace Turbo.Plugins
{
    public interface INewAreaHandler : IPlugin
    {
        /// <summary>
        /// Called when the player enters a new game or area. This method is called during the data collection phase, which means no rendering is possible!
        /// </summary>
        void OnNewArea(bool newGame, ISnoArea area);
    }
}