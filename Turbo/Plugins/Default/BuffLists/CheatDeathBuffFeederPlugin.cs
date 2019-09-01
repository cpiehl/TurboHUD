namespace Turbo.Plugins.Default
{
    public class CheatDeathBuffFeederPlugin : BasePlugin, IInGameTopPainter
    {
        public IBrush BorderBrush { get; set; }
        public IBrush FillBrush { get; set; }

        public BuffRuleCalculator RuleList { get; private set; }

        public CheatDeathBuffFeederPlugin()
        {
            Enabled = true;
            Order = -10000;
        }

        public override void Load(IController hud)
        {
            base.Load(hud);

            BorderBrush = Hud.Render.CreateBrush(50, 255, 0, 0, -2);
            FillBrush = Hud.Render.CreateBrush(25, 255, 0, 0, 0);

            RuleList = new BuffRuleCalculator(Hud);
            RuleList.Rules.Add(new BuffRule(156484) { IconIndex = 1, MinimumIconCount = 1, IconSizeMultiplier = 1.25f, }); // Near Death Experience
            RuleList.Rules.Add(new BuffRule(208474) { IconIndex = 1, MinimumIconCount = 1, IconSizeMultiplier = 1.25f, }); // Unstable Anomaly
            RuleList.Rules.Add(new BuffRule(359580) { IconIndex = 1, MinimumIconCount = 1, IconSizeMultiplier = 1.25f, }); // Firebird's Finery
            RuleList.Rules.Add(new BuffRule(324770) { IconIndex = 1, MinimumIconCount = 1, IconSizeMultiplier = 1.25f, }); // Awareness
            RuleList.Rules.Add(new BuffRule(218501) { IconIndex = 1, MinimumIconCount = 1, IconSizeMultiplier = 1.25f, }); // Spirit Vessel
            RuleList.Rules.Add(new BuffRule(309830) { IconIndex = 1, MinimumIconCount = 1, IconSizeMultiplier = 1.25f, }); // Indestructible
            RuleList.Rules.Add(new BuffRule(217819) { IconIndex = 1, MinimumIconCount = 1, IconSizeMultiplier = 1.25f, }); // Nerves of Steel
            RuleList.Rules.Add(new BuffRule(465952) { IconIndex = 1, MinimumIconCount = 1, IconSizeMultiplier = 1.25f, }); // Final Service 
        }

        public void PaintTopInGame(ClipState clipState)
        {
            if (Hud.Render.UiHidden)
                return;
            if (clipState != ClipState.BeforeClip)
                return;

            RuleList.CalculatePaintInfo(Hud.Game.Me);
            if (RuleList.PaintInfoList.Count > 0)
            {
                var uiMinimapRect = Hud.Render.MinimapUiElement.Rectangle;

                FillBrush.DrawRectangleGridFit(uiMinimapRect.X, uiMinimapRect.Y, uiMinimapRect.Width, uiMinimapRect.Height);
                BorderBrush.DrawRectangleGridFit(uiMinimapRect.X, uiMinimapRect.Y, uiMinimapRect.Width, uiMinimapRect.Height);
            }
        }
    }
}