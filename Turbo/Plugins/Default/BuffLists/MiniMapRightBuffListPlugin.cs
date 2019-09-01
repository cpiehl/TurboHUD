namespace Turbo.Plugins.Default
{
    public class MiniMapRightBuffListPlugin : BasePlugin, IInGameTopPainter
    {
        public BuffPainter BuffPainter { get; set; }
        public BuffRuleCalculator RuleCalculator { get; private set; }

        public MiniMapRightBuffListPlugin()
        {
            Enabled = true;
        }

        public override void Load(IController hud)
        {
            base.Load(hud);

            BuffPainter = new BuffPainter(Hud, true)
            {
                Opacity = 0.50f,
                TimeLeftFont = Hud.Render.CreateFont("tahoma", 5, 160, 255, 255, 255, true, false, 160, 0, 0, 0, true),
            };

            RuleCalculator = new BuffRuleCalculator(Hud)
            {
                SizeMultiplier = 0.75f
            };

            RuleCalculator.Rules.Add(new BuffRule(156484) { IconIndex = 1, MinimumIconCount = 1, IconSizeMultiplier = 1.25f, }); // Near Death Experience
            RuleCalculator.Rules.Add(new BuffRule(208474) { IconIndex = 1, MinimumIconCount = 1, IconSizeMultiplier = 1.25f, }); // Unstable Anomaly
            RuleCalculator.Rules.Add(new BuffRule(359580) { IconIndex = 1, MinimumIconCount = 1, IconSizeMultiplier = 1.25f, }); // Firebird's Finery
            RuleCalculator.Rules.Add(new BuffRule(324770) { IconIndex = 1, MinimumIconCount = 1, IconSizeMultiplier = 1.25f, }); // Awareness
            RuleCalculator.Rules.Add(new BuffRule(218501) { IconIndex = 1, MinimumIconCount = 1, IconSizeMultiplier = 1.25f, }); // Spirit Vessel
            RuleCalculator.Rules.Add(new BuffRule(309830) { IconIndex = 1, MinimumIconCount = 1, IconSizeMultiplier = 1.25f, }); // Indestructible
            RuleCalculator.Rules.Add(new BuffRule(217819) { IconIndex = 1, MinimumIconCount = 1, IconSizeMultiplier = 1.25f, }); // Nerves of Steel
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

            var uiMinimapRect = Hud.Render.MinimapUiElement.Rectangle;

            var x = uiMinimapRect.Right - (RuleCalculator.StandardIconSize / 2);
            BuffPainter.PaintVerticalCenter(RuleCalculator.PaintInfoList, x, uiMinimapRect.Top, uiMinimapRect.Height, RuleCalculator.StandardIconSize, RuleCalculator.StandardIconSpacing);
        }
    }
}