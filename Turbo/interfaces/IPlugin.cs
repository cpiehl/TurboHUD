using System.Collections.Generic;

namespace Turbo.Plugins
{
    public enum WorldLayer { Ground, Map }
    public enum ClipState { BeforeClip, Inventory, AfterClip }

    public interface IPlugin
    {
        /// <summary>
        /// The HUD instance given when Load called.
        /// </summary>
        IController Hud { get; }

        /// <summary>
        /// The state of the plugin.
        /// </summary>
        bool Enabled { get; set; }

        /// <summary>
        /// The order of the plugin. Smaller number means the plugin will be called earlier, larger number means the plugin will be called later.
        /// </summary>
        int Order { get; set; }

        /// <summary>
        /// Performance counters for the plugin.
        /// </summary>
        Dictionary<string, IPerfCounter> PerformanceCounters { get; }

        /// <summary>
        /// Called when the plugin has to initialize itself and set the Hud property to the given parameter.
        /// </summary>
        /// <param name="hud">The HUD instance.</param>
        void Load(IController hud);
    }
}