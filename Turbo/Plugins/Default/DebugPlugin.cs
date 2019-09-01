using System;
using System.Globalization;
using System.Linq;

namespace Turbo.Plugins.Default
{
    public class DebugPlugin : BasePlugin, IInGameTopPainter
    {
        public IFont PluginPerfFont { get; set; }
        public TopLabelDecorator RenderTimeDecorator { get; set; }
        public TopLabelDecorator MemoryUsageDecorator { get; set; }
        public bool PluginPerformanceCountersEnabled { get; set; } = false;

        public DebugPlugin()
        {
            Enabled = false;
        }

        public override void Load(IController hud)
        {
            base.Load(hud);

            RenderTimeDecorator = new TopLabelDecorator(Hud)
            {
                TextFont = Hud.Render.CreateFont("courier new", 6, 255, 0, 255, 0, true, false, false),
                TextFunc = () => Hud.Stat.RenderPerfCounter.LastValue.ToString("F0") + " (" + Hud.Stat.RenderPerfCounter.LastCount.ToString("F0") + " FPS)",
            };

            MemoryUsageDecorator = new TopLabelDecorator(Hud)
            {
                TextFont = Hud.Render.CreateFont("courier new", 6, 255, 255, 50, 50, true, false, false),
                TextFunc = () => (GC.GetTotalMemory(false) / 1024.0 / 1024.0).ToString("F0") + " MB",
            };

            PluginPerfFont = Hud.Render.CreateFont("tahoma", 6, 255, 255, 255, 255, false, false, false);
        }

        public void PaintTopInGame(ClipState clipState)
        {
            if (clipState != ClipState.AfterClip)
                return;

            RenderTimeDecorator.Paint(Hud.Window.Size.Width * 0.92f, Hud.Window.Size.Height * 0.0000f, Hud.Window.Size.Width * 0.08f, Hud.Window.Size.Height * 0.01f, HorizontalAlign.Right);
            MemoryUsageDecorator.Paint(Hud.Window.Size.Width * 0.84f, Hud.Window.Size.Height * 0.0000f, Hud.Window.Size.Width * 0.08f, Hud.Window.Size.Height * 0.01f, HorizontalAlign.Right);

            if (PluginPerformanceCountersEnabled)
            {
                PaintPluginPerformanceCounters();
            }
        }

        private void PaintPluginPerformanceCounters()
        {
            var startXcoord = Hud.Game.Me.PortraitUiElement.Rectangle.Right + (Hud.Game.Me.PortraitUiElement.Rectangle.Width * 0.3f);
            var yCoord = Hud.Game.Me.PortraitUiElement.Rectangle.Top;
            var nameColumnWidth = Hud.Game.Me.PortraitUiElement.Rectangle.Width * 4;
            var perfColumnWidth = Hud.Game.Me.PortraitUiElement.Rectangle.Width * 3;

            var plugins = Hud.AllPlugins.Where(p => p.Enabled);

            var pluginNameText = "PLUGIN NAME";
            using (var pluginNameLayout = PluginPerfFont.GetTextLayoutManualDispose(pluginNameText))
            {
                PluginPerfFont.DrawText(pluginNameLayout, startXcoord, yCoord);

                var text = "ACTION =  CPU_TIME/SEC (CALLS/SEC)";
                using (var layout = PluginPerfFont.GetTextLayoutManualDispose(text))
                {
                    PluginPerfFont.DrawText(layout, startXcoord + nameColumnWidth, yCoord);
                }

                yCoord += pluginNameLayout.Metrics.Height;
            }

            foreach (var plugin in plugins)
            {
                var counters = plugin.PerformanceCounters.Where(x => x.Value.LastCount > 0);
                if (!counters.Any())
                    continue;

                pluginNameText = plugin.GetType().FullName;
                using (var pluginNameLayout = PluginPerfFont.GetTextLayoutManualDispose(pluginNameText))
                {
                    PluginPerfFont.DrawText(pluginNameLayout, startXcoord, yCoord);

                    var xCoord = startXcoord + nameColumnWidth;

                    foreach (var perf in counters)
                    {
                        var text = perf.Key + " = " + perf.Value.LastValue.ToString("F1", CultureInfo.InvariantCulture) + " (" + perf.Value.LastCount.ToString("F1", CultureInfo.InvariantCulture) + ")";
                        using (var layout = PluginPerfFont.GetTextLayoutManualDispose(text))
                        {
                            PluginPerfFont.DrawText(layout, xCoord, yCoord);
                        }

                        xCoord += perfColumnWidth;
                    }

                    yCoord += pluginNameLayout.Metrics.Height;
                }
            }
        }
    }
}