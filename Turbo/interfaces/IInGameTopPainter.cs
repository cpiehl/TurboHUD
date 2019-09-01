namespace Turbo.Plugins
{
    public interface IInGameTopPainter : IPlugin
    {
        /// <summary>
        /// Called when HUD renders the 2D UI (which is not related to world coordinate system).
        /// </summary>
        void PaintTopInGame(ClipState clipState);
    }
}