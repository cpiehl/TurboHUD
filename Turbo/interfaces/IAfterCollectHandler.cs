namespace Turbo.Plugins
{
    public interface IAfterCollectHandler : IPlugin
    {
        /// <summary>
        /// Called after data is collected from the game, but render is not started yet.
        /// It is not guaranteed that there is a collection before every frame.
        /// </summary>
        void AfterCollect();
    }
}