using System.Globalization;

namespace Turbo.Plugins.Default
{
    public class InventoryFreeSpacePlugin : BasePlugin, IInGameTopPainter
    {
        public TopLabelDecorator RedDecorator { get; set; }
        public TopLabelDecorator YellowDecorator { get; set; }
        public TopLabelDecorator GreenDecorator { get; set; }

        public InventoryFreeSpacePlugin()
        {
            Enabled = true;
        }

        public override void Load(IController hud)
        {
            base.Load(hud);

            RedDecorator = new TopLabelDecorator(Hud)
            {
                TextFont = Hud.Render.CreateFont("tahoma", 8, 255, 255, 100, 100, true, false, 255, 0, 0, 0, true),
                BackgroundTexture1 = Hud.Texture.ButtonTextureGray,
                BackgroundTexture2 = Hud.Texture.BackgroundTextureOrange,
                BackgroundTextureOpacity1 = 1.0f,
                BackgroundTextureOpacity2 = 1.0f,
                TextFunc = () => (Hud.Game.Me.InventorySpaceTotal - Hud.Game.InventorySpaceUsed).ToString("D", CultureInfo.InvariantCulture),
                HintFunc = () => "free space in inventory",
            };

            YellowDecorator = new TopLabelDecorator(Hud)
            {
                TextFont = Hud.Render.CreateFont("tahoma", 7, 255, 200, 205, 50, true, false, false),
                BackgroundTexture1 = Hud.Texture.ButtonTextureGray,
                BackgroundTexture2 = Hud.Texture.BackgroundTextureOrange,
                BackgroundTextureOpacity1 = 1.0f,
                BackgroundTextureOpacity2 = 1.0f,
                TextFunc = () => (Hud.Game.Me.InventorySpaceTotal - Hud.Game.InventorySpaceUsed).ToString("D", CultureInfo.InvariantCulture),
                HintFunc = () => "free space in inventory",
            };

            GreenDecorator = new TopLabelDecorator(Hud)
            {
                TextFont = Hud.Render.CreateFont("tahoma", 7, 255, 100, 130, 100, false, false, false),
                BackgroundTexture1 = Hud.Texture.ButtonTextureGray,
                BackgroundTexture2 = Hud.Texture.BackgroundTextureOrange,
                BackgroundTextureOpacity1 = 1.0f,
                BackgroundTextureOpacity2 = 1.0f,
                TextFunc = () => (Hud.Game.Me.InventorySpaceTotal - Hud.Game.InventorySpaceUsed).ToString("D", CultureInfo.InvariantCulture),
                HintFunc = () => "free space in inventory",
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

            var freeSpace = Hud.Game.Me.InventorySpaceTotal - Hud.Game.InventorySpaceUsed;

            var decorator = freeSpace < 2 ? RedDecorator : freeSpace < 20 ? YellowDecorator : GreenDecorator;
            decorator.Paint(uiRect.Left + (uiRect.Width * 0.645f), uiRect.Top + (uiRect.Height * 0.88f), uiRect.Width * 0.019f, uiRect.Height * 0.12f, HorizontalAlign.Center);
        }
    }
}