namespace Turbo.Plugins
{
    public interface IInGameWorldPainter : IPlugin
    {
        /// <summary>
        /// Called once for each layer while HUD is rendering the UI: ground and (mini)map rendering.
        /// </summary>
        void PaintWorld(WorldLayer layer);
    }
}