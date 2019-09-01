namespace Turbo.Plugins.Default
{
    public class TopMonsterHealthBarPlugin : BasePlugin, IInGameTopPainter
    {
        public IFont MonsterHitpointsFont { get; set; }
        public IFont MonsterEffectsFont { get; set; }

        public TopMonsterHealthBarPlugin()
        {
            Enabled = true;
        }

        public override void Load(IController hud)
        {
            base.Load(hud);

            MonsterHitpointsFont = Hud.Render.CreateFont("tahoma", 6, 255, 255, 255, 255, true, false, true);
            MonsterEffectsFont = Hud.Render.CreateFont("tahoma", 6, 255, 40, 200, 40, true, false, 128, 0, 0, 0, true);
        }

        public void PaintTopInGame(ClipState clipState)
        {
            if (clipState != ClipState.BeforeClip)
                return;

            var uiBar = Hud.Render.MonsterHpBarUiElement;

            var monster = Hud.Game.SelectedMonster2 ?? Hud.Game.SelectedMonster1;
            if ((monster == null) || (uiBar == null))
                return;

            var hpText = ValueToString(monster.CurHealth, ValueFormat.LongNumber) + " / " + ValueToString(monster.MaxHealth, ValueFormat.LongNumber);
            hpText += " - " + ValueToString(monster.CurHealth / (monster.MaxHealth / 100.0f), ValueFormat.LongNumber) + "%";

            var textLayout = MonsterHitpointsFont.GetTextLayout(hpText);
            MonsterHitpointsFont.DrawText(textLayout, uiBar.Rectangle.Left + ((uiBar.Rectangle.Width - textLayout.Metrics.Width) / 2), uiBar.Rectangle.Top + ((uiBar.Rectangle.Height - textLayout.Metrics.Height) / 2));

            string textCC = null;
            if (monster.Frozen)
                textCC += (textCC == null ? "" : ", ") + "frozen";
            if (monster.Chilled)
                textCC += (textCC == null ? "" : ", ") + "chill";
            if (monster.Slow)
                textCC += (textCC == null ? "" : ", ") + "slow";
            if (monster.Stunned)
                textCC += (textCC == null ? "" : ", ") + "stun";
            if (monster.Invulnerable)
                textCC += (textCC == null ? "" : ", ") + "invulnerable";
            if (monster.Blind)
                textCC += (textCC == null ? "" : ", ") + "blind";

            string textDebuff = null;
            if (monster.Locust)
                textDebuff += (textDebuff == null ? "" : ", ") + "locust";
            if (monster.Palmed)
                textDebuff += (textDebuff == null ? "" : ", ") + "palm";
            if (monster.Haunted)
                textDebuff += (textDebuff == null ? "" : ", ") + "haunt";
            if (monster.MarkedForDeath)
                textDebuff += (textDebuff == null ? "" : ", ") + "mark";
            if (monster.Strongarmed)
                textDebuff += (textDebuff == null ? "" : ", ") + "strongarm";
            if (monster.Phoenixed)
                textDebuff += (textDebuff == null ? "" : ", ") + "firebird";

            var text = textCC + (textCC != null && textDebuff != null ? " | " : "") + textDebuff;

            if (monster.DotDpsApplied > 0)
                text += (string.IsNullOrEmpty(text) ? "" : " | ") + "DOT: " + ValueToString(monster.DotDpsApplied, ValueFormat.LongNumber);
            if (text != null)
            {
                textLayout = MonsterEffectsFont.GetTextLayout(text);
                MonsterEffectsFont.DrawText(textLayout, uiBar.Rectangle.Left + ((uiBar.Rectangle.Width - textLayout.Metrics.Width) / 2), uiBar.Rectangle.Top - (uiBar.Rectangle.Height * 0.38f) - textLayout.Metrics.Height);
            }
        }
    }
}