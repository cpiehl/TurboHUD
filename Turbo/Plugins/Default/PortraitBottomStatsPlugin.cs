namespace Turbo.Plugins.Default
{
    public class PortraitBottomStatsPlugin : BasePlugin, IInGameTopPainter
    {
        public TopLabelDecorator MonsterHpDecreaseDecorator { get; set; }

        public PortraitBottomStatsPlugin()
        {
            Enabled = true;
        }

        public override void Load(IController hud)
        {
            base.Load(hud);

            MonsterHpDecreaseDecorator = new TopLabelDecorator(Hud)
            {
                BackgroundTexture1 = Hud.Texture.BackgroundTextureOrange,
                BackgroundTextureOpacity1 = 1.0f,
                TextFont = Hud.Render.CreateFont("tahoma", 6, 255, 200, 180, 100, true, false, 255, 0, 0, 0, true),
                TextFunc = () => ValueToString(Hud.Stat.MonsterHitpointDecreasePerfCounter.LastValue, ValueFormat.LongNumber),
                HintFunc = () => "DPS dealt to monsters",
            };
        }

        public void PaintTopInGame(ClipState clipState)
        {
            if (Hud.Render.UiHidden)
                return;
            if (clipState != ClipState.BeforeClip)
                return;
            if (Hud.Game.NumberOfPlayersInGame <= 1)
                return;
            if ((Hud.Game.MapMode == MapMode.WaypointMap) || (Hud.Game.MapMode == MapMode.ActMap) || (Hud.Game.MapMode == MapMode.Map))
                return;

            var uiRect = Hud.Render.GetUiElement("*portrait-bottom").Rectangle;

            MonsterHpDecreaseDecorator.Paint(uiRect.Left + (uiRect.Width * 0.14f), uiRect.Top + (uiRect.Height * 1.03f), uiRect.Width * 0.72f, uiRect.Height * 0.1f, HorizontalAlign.Center);
        }
    }
}