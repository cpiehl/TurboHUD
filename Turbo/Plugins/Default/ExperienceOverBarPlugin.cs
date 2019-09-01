using System.Globalization;

namespace Turbo.Plugins.Default
{
    public class ExperienceOverBarPlugin : BasePlugin, IInGameTopPainter
    {
        public TopLabelDecorator BlueThisLevelValueDecorator { get; set; }
        public TopLabelDecorator OrangeThisLevelValueDecorator { get; set; }
        public TopLabelDecorator BlueNextLevelValueDecorator { get; set; }
        public TopLabelDecorator OrangeNextLevelValueDecorator { get; set; }
        public TopLabelDecorator BonusValueDecorator { get; set; }

        public ExperienceOverBarPlugin()
        {
            Enabled = true;
        }

        public override void Load(IController hud)
        {
            base.Load(hud);

            BlueThisLevelValueDecorator = new TopLabelDecorator(Hud)
            {
                TextFont = Hud.Render.CreateFont("tahoma", 5.5f, 255, 140, 140, 180, false, false, 160, 0, 0, 0, true),
                TextFunc = () => Hud.Game.Me.ParagonExpInThisLevel.ToString("#,0", CultureInfo.InvariantCulture),
                HintFunc = () => "experience gained in this level",
            };

            OrangeThisLevelValueDecorator = new TopLabelDecorator(Hud)
            {
                TextFont = Hud.Render.CreateFont("tahoma", 5.5f, 255, 200, 160, 140, false, false, 160, 0, 0, 0, true),
                TextFunc = () => Hud.Game.Me.ParagonExpInThisLevel.ToString("#,0", CultureInfo.InvariantCulture),
                HintFunc = () => "experience gained in this level",
            };

            BlueNextLevelValueDecorator = new TopLabelDecorator(Hud)
            {
                TextFont = Hud.Render.CreateFont("tahoma", 5.5f, 255, 140, 140, 180, false, false, 160, 0, 0, 0, true),
                TextFunc = () => Hud.Game.Me.ParagonExpToNextLevel.ToString("#,0", CultureInfo.InvariantCulture),
                HintFunc = () => "experience to reach next level",
            };

            OrangeNextLevelValueDecorator = new TopLabelDecorator(Hud)
            {
                TextFont = Hud.Render.CreateFont("tahoma", 5.5f, 255, 200, 160, 140, false, false, 160, 0, 0, 0, true),
                TextFunc = () => Hud.Game.Me.ParagonExpToNextLevel.ToString("#,0", CultureInfo.InvariantCulture),
                HintFunc = () => "experience to reach next level",
            };

            BonusValueDecorator = new TopLabelDecorator(Hud)
            {
                TextFont = Hud.Render.CreateFont("tahoma", 5.5f, 255, 200, 160, 140, false, false, 160, 0, 0, 0, true),
                TextFunc = () => (Hud.Game.Me.BonusPoolRemaining * 5).ToString("#,0", CultureInfo.InvariantCulture),
                HintFunc = () => "bonus pool * 5",
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

            var uiRect = Hud.Render.InGameBottomHudUiElement.Rectangle;

            var bonusRemaining = Hud.Game.Me.BonusPoolRemaining;

            (bonusRemaining > 0 ? OrangeThisLevelValueDecorator : BlueThisLevelValueDecorator).Paint(uiRect.Left + (uiRect.Width * 0.42f), uiRect.Top + (uiRect.Height * 0.470f), uiRect.Width * 0.075f, uiRect.Height * 0.07f, HorizontalAlign.Right);
            (bonusRemaining > 0 ? OrangeNextLevelValueDecorator : BlueNextLevelValueDecorator).Paint(uiRect.Left + (uiRect.Width * 0.505f), uiRect.Top + (uiRect.Height * 0.470f), uiRect.Width * 0.075f, uiRect.Height * 0.07f, HorizontalAlign.Left);

            if (bonusRemaining > 0)
            {
                BonusValueDecorator.Paint(uiRect.Left + (uiRect.Width * 0.651f), uiRect.Top + (uiRect.Height * 0.470f), uiRect.Width * 0.075f, uiRect.Height * 0.07f, HorizontalAlign.Right);
            }
        }
    }
}