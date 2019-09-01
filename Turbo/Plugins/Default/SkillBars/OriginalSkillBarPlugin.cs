using System;
using SharpDX;

namespace Turbo.Plugins.Default
{
    public class OriginalSkillBarPlugin : BasePlugin, IInGameTopPainter
    {
        public SkillPainter SkillPainter { get; set; }

        public OriginalSkillBarPlugin()
        {
            Enabled = true;
        }

        public override void Load(IController hud)
        {
            base.Load(hud);

            SkillPainter = new SkillPainter(Hud, true)
            {
                TextureOpacity = 0.0f,
                EnableSkillDpsBar = true,
                EnableDetailedDpsHint = true,
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

            var uiSkill1 = Hud.Render.GetPlayerSkillUiElement(ActionKey.Skill1);

            foreach (var skill in Hud.Game.Me.Powers.CurrentSkills)
            {
                var ui = Hud.Render.GetPlayerSkillUiElement(skill.Key);

                var rect = new RectangleF((float)Math.Round(ui.Rectangle.X) + 0.5f, (float)Math.Round(uiSkill1.Rectangle.Y) + 0.5f, (float)Math.Round(ui.Rectangle.Width), (float)Math.Round(uiSkill1.Rectangle.Height));

                SkillPainter.Paint(skill, rect);
            }
        }
    }
}