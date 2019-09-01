using System.Globalization;

namespace Turbo.Plugins.Default
{
    public class NotifyAtRiftPercentagePlugin : BasePlugin, IInGameTopPainter
    {
        public TopLabelWithTitleDecorator LabelDecorator { get; set; }
        public int DisplayLimit { get; set; } = 90;

        public NotifyAtRiftPercentagePlugin()
        {
            Enabled = true;
        }

        public override void Load(IController hud)
        {
            base.Load(hud);

            LabelDecorator = new TopLabelWithTitleDecorator(Hud)
            {
                BorderBrush = Hud.Render.CreateBrush(255, 180, 147, 109, -1),
                BackgroundBrush = Hud.Render.CreateBrush(128, 0, 0, 0, 0),
                TextFont = Hud.Render.CreateFont("tahoma", 9, 255, 255, 210, 150, true, false, false),
                TitleFont = Hud.Render.CreateFont("tahoma", 6, 255, 180, 147, 109, true, false, false),
            };
        }

        public void PaintTopInGame(ClipState clipState)
        {
            if (clipState != ClipState.AfterClip)
                return;
            if ((Hud.Game.SpecialArea != SpecialArea.Rift) && (Hud.Game.SpecialArea != SpecialArea.GreaterRift))
                return;
            if (Hud.Game.RiftPercentage < DisplayLimit)
                return;
            if (Hud.Game.RiftPercentage >= 100)
                return;

            var text = Hud.Game.RiftPercentage.ToString("F1", CultureInfo.InvariantCulture) + "%";
            var title = "Rift Completion";

            var w = Hud.Window.Size.Height * 0.1f;
            var h = Hud.Window.Size.Height * 0.04f;

            LabelDecorator.Paint((Hud.Window.Size.Width * 0.5f) - (w / 2), (Hud.Window.Size.Height * 0.18f) - (h / 2), w, h, text, title);
        }
    }
}