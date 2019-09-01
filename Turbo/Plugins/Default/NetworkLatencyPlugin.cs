using System.Globalization;

namespace Turbo.Plugins.Default
{
    public class NetworkLatencyPlugin : BasePlugin, IInGameTopPainter
    {
        public TopLabelDecorator AverageDecoratorNormal { get; set; }
        public TopLabelDecorator CurrentDecoratorNormal { get; set; }
        public TopLabelDecorator AverageDecoratorHigh { get; set; }
        public TopLabelDecorator CurrentDecoratorHigh { get; set; }
        public int HighLimit { get; set; } = 50;

        public NetworkLatencyPlugin()
        {
            Enabled = true;
        }

        public override void Load(IController hud)
        {
            base.Load(hud);

            AverageDecoratorNormal = new TopLabelDecorator(Hud)
            {
                TextFont = Hud.Render.CreateFont("tahoma", 6, 160, 150, 200, 150, false, false, false),
                TextFunc = () => Hud.Game.AverageLatency.ToString("F0", CultureInfo.InvariantCulture),
                HintFunc = () => "average latency"
            };

            CurrentDecoratorNormal = new TopLabelDecorator(Hud)
            {
                TextFont = Hud.Render.CreateFont("tahoma", 6, 160, 150, 200, 150, false, false, false),
                TextFunc = () => Hud.Game.CurrentLatency.ToString("F0", CultureInfo.InvariantCulture),
                HintFunc = () => "current latency"
            };

            AverageDecoratorHigh = new TopLabelDecorator(Hud)
            {
                TextFont = Hud.Render.CreateFont("tahoma", 6, 255, 200, 175, 150, false, false, false),
                TextFunc = () => Hud.Game.AverageLatency.ToString("F0", CultureInfo.InvariantCulture),
                HintFunc = () => "average latency"
            };

            CurrentDecoratorHigh = new TopLabelDecorator(Hud)
            {
                TextFont = Hud.Render.CreateFont("tahoma", 6, 255, 200, 175, 150, false, false, false),
                TextFunc = () => Hud.Game.CurrentLatency.ToString("F0", CultureInfo.InvariantCulture),
                HintFunc = () => "current latency"
            };
        }

        public void PaintTopInGame(ClipState clipState)
        {
            if (Hud.Render.UiHidden)
                return;
            if (clipState != ClipState.BeforeClip)
                return;
            if ((Hud.Game.MapMode == MapMode.WaypointMap) || (Hud.Game.MapMode == MapMode.ActMap) || (Hud.Game.MapMode == MapMode.Map))
                return;

            var uiRect = Hud.Render.GetUiElement("Root.NormalLayer.game_dialog_backgroundScreenPC.latency_meter").Rectangle;

            var avg = Hud.Game.AverageLatency;
            var cur = Hud.Game.CurrentLatency;

            (avg >= HighLimit ? AverageDecoratorHigh : AverageDecoratorNormal).Paint(uiRect.Left + (uiRect.Width * 0.5f), uiRect.Top + (uiRect.Height * 0.62f), uiRect.Width, uiRect.Height * 0.15f, HorizontalAlign.Left);
            (cur >= HighLimit ? CurrentDecoratorHigh : CurrentDecoratorNormal).Paint(uiRect.Left + (uiRect.Width * 0.5f), uiRect.Top + (uiRect.Height * 0.80f), uiRect.Width, uiRect.Height * 0.15f, HorizontalAlign.Left);
        }
    }
}