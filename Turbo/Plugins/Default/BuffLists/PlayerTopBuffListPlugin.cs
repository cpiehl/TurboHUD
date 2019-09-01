namespace Turbo.Plugins.Default
{
    public class PlayerTopBuffListPlugin : BasePlugin, IInGameTopPainter
    {
        public BuffPainter BuffPainter { get; set; }
        public BuffRuleCalculator RuleCalculator { get; private set; }
        public float PositionOffset { get; set; } = -0.20f;

        public PlayerTopBuffListPlugin()
        {
            Enabled = true;
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

            RuleCalculator = new BuffRuleCalculator(Hud)
            {
                SizeMultiplier = 0.75f
            };
        }

        public void PaintTopInGame(ClipState clipState)
        {
            if (Hud.Render.UiHidden)
                return;
            if (clipState != ClipState.BeforeClip)
                return;

            RuleCalculator.CalculatePaintInfo(Hud.Game.Me);
            if (RuleCalculator.PaintInfoList.Count == 0)
                return;

            var y = (Hud.Window.Size.Height * 0.5f) + (Hud.Window.Size.Height * PositionOffset);
            BuffPainter.PaintHorizontalCenter(RuleCalculator.PaintInfoList, 0, y, Hud.Window.Size.Width, RuleCalculator.StandardIconSize, RuleCalculator.StandardIconSpacing);
        }
    }
}