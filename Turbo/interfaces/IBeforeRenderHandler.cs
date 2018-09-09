namespace Turbo.Plugins
{
    public interface IBeforeRenderHandler : IPlugin
    {
        /// <summary>
        /// Called before rendering starts, which means no rendering is possible!
        /// </summary>
        void BeforeRender();
    }
}