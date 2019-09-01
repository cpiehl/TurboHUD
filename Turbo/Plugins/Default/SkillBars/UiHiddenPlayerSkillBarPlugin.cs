using SharpDX;

namespace Turbo.Plugins.Default
{
    public class UiHiddenPlayerSkillBarPlugin : BasePlugin, IInGameTopPainter
    {
        public SkillPainter SkillPainter { get; set; }
        public float Ratio { get; set; } = 0.55f;

        public UiHiddenPlayerSkillBarPlugin()
        {
            Enabled = true;
        }

        public override void Load(IController hud)
        {
            base.Load(hud);

            SkillPainter = new SkillPainter(Hud, true)
            {
                TextureOpacity = 1.0f,
                EnableSkillDpsBar = false,
                EnableDetailedDpsHint = false,
                CooldownFont = Hud.Render.CreateFont("arial", 8, 255, 255, 255, 255, true, false, 255, 0, 0, 0, true),
            };
        }

        public void PaintTopInGame(ClipState clipState)
        {
            if (!Hud.Render.UiHidden)
                return;
            if (clipState != ClipState.BeforeClip)
                return;

            var size = 55f / 1200.0f * Hud.Window.Size.Height * Ratio;

            foreach (var player in Hud.Game.Players)
            {
                var portraitRect = player.PortraitUiElement.Rectangle;
                foreach (var skill in player.Powers.CurrentSkills)
                {
                    var index = skill.Key <= ActionKey.RightSkill ? (int)skill.Key + 4 : (int)skill.Key - 2;

                    var x = portraitRect.Right + (size * index);
                    var y = portraitRect.Top + (portraitRect.Height * 0.21f);

                    var rect = new RectangleF(x, y, size, size);

                    SkillPainter.Paint(skill, rect);
                }
            }
        }
    }
}