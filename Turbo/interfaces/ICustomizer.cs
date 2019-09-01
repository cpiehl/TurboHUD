namespace Turbo.Plugins
{
    public interface ICustomizer : IPlugin
    {
        /// <summary>
        /// Called after all plugins are loaded, so the plugin can be sure about all plugins are already loaded so it can change other plugins' properties.
        /// </summary>
        void Customize();
    }
}