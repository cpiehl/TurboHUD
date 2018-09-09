namespace Turbo.Plugins.Default
{

    public class TopLeftBuffListPlugin : BasePlugin, IInGameTopPainter
    {

        public BuffPainter BuffPainter { get; set; }
        public BuffRuleCalculator RuleCalculator { get; private set; }
        public float PositionX { get; set; }
        public float PositionY { get; set; }

        public TopLeftBuffListPlugin()
        {
            Enabled = true;
            PositionX = 0.25f;
            PositionY = 0.001f;
        }

        public override void Load(IController hud)
        {
            base.Load(hud);

            BuffPainter = new BuffPainter(Hud, true)
            {
                Opacity = 0.75f,
                ShowTimeLeftNumbers = false,
                ShowTooltips = false,
                TimeLeftFont = Hud.Render.CreateFont("tahoma", 7, 255, 255, 255, 255, false, false, 255, 0, 0, 0, true),
                StackFont = Hud.Render.CreateFont("tahoma", 6, 255, 255, 255, 255, false, false, 255, 0, 0, 0, true),
            };

            RuleCalculator = new BuffRuleCalculator(Hud);
            RuleCalculator.SizeMultiplier = 0.75f;
        }

        public void PaintTopInGame(ClipState clipState)
        {
            if (Hud.Render.UiHidden) return;
            if (clipState != ClipState.BeforeClip) return;

            RuleCalculator.CalculatePaintInfo(Hud.Game.Me);
            if (RuleCalculator.PaintInfoList.Count == 0) return;

            var w = Hud.Window.Size.Width * 0.5f;
            var x = Hud.Window.Size.Width * PositionX - w / 2;
            var y = Hud.Window.Size.Height * PositionY;
            BuffPainter.PaintHorizontalCenter(RuleCalculator.PaintInfoList, x, y, w, RuleCalculator.StandardIconSize, RuleCalculator.StandardIconSpacing);
        }

    }

}