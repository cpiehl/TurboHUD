using SharpDX;
using System;

namespace Turbo.Plugins.Default
{

    public class OriginalHealthPotionSkillPlugin : BasePlugin, IInGameTopPainter
    {

        public SkillPainter Decorator { get; set; }

        public OriginalHealthPotionSkillPlugin()
        {
            Enabled = true;
        }

        public override void Load(IController hud)
        {
            base.Load(hud);

            Decorator = new SkillPainter(Hud, true)
            {
                TextureOpacity = 0.0f,
                EnableSkillDpsBar = true,
                EnableDetailedDpsHint = true,
            };
        }

        public void PaintTopInGame(ClipState clipState)
        {
            if (Hud.Render.UiHidden) return;
            if (clipState != ClipState.BeforeClip) return;

            var ui = Hud.Render.GetPlayerSkillUiElement(ActionKey.Heal);
            var rect = new RectangleF((float)Math.Round(ui.Rectangle.X) + 0.5f, (float)Math.Round(ui.Rectangle.Y) + 0.5f, (float)Math.Round(ui.Rectangle.Width), (float)Math.Round(ui.Rectangle.Height));

            Decorator.Paint(Hud.Game.Me.Powers.HealthPotionSkill, rect);
        }

    }

}