using System.Globalization;

namespace Turbo.Plugins.Default
{
    public class ResourceOverGlobePlugin : BasePlugin, IInGameTopPainter
    {
        public TopLabelDecorator HealthDecorator { get; set; }
        public TopLabelDecorator HealingPosDecorator { get; set; }
        public TopLabelDecorator HealingNegDecorator { get; set; }
        public TopLabelDecorator ShieldDecorator { get; set; }

        public TopLabelDecorator ArcaneValueDecorator { get; set; }
        public TopLabelDecorator ArcaneRegenDecorator { get; set; }
        public TopLabelDecorator ManaValueDecorator { get; set; }
        public TopLabelDecorator ManaRegenDecorator { get; set; }
        public TopLabelDecorator SpiritValueDecorator { get; set; }
        public TopLabelDecorator SpiritRegenDecorator { get; set; }
        public TopLabelDecorator FuryValueDecorator { get; set; }
        public TopLabelDecorator FuryRegenDecorator { get; set; }
        public TopLabelDecorator HatredValueDecorator { get; set; }
        public TopLabelDecorator HatredRegenDecorator { get; set; }
        public TopLabelDecorator DisciplineValueDecorator { get; set; }
        public TopLabelDecorator DisciplineRegenDecorator { get; set; }
        public TopLabelDecorator WrathValueDecorator { get; set; }
        public TopLabelDecorator WrathRegenDecorator { get; set; }
        public TopLabelDecorator EssenceValueDecorator { get; set; }
        public TopLabelDecorator EssenceRegenDecorator { get; set; }

        public ResourceOverGlobePlugin()
        {
            Enabled = true; // .NET Framework does not contains CompilerServices for C# 6
        }

        public override void Load(IController hud)
        {
            base.Load(hud);

            HealthDecorator = new TopLabelDecorator(Hud)
            {
                TextFont = Hud.Render.CreateFont("tahoma", 10, 255, 255, 180, 144, true, false, 96, 255, 0, 0, true),
                TextFunc = () => Hud.Game.Me.Defense.HealthCur.ToString("#,0", CultureInfo.InvariantCulture),
            };

            HealingPosDecorator = new TopLabelDecorator(Hud)
            {
                TextFont = Hud.Render.CreateFont("tahoma", 8, 255, 110, 205, 110, true, false, 255, 100, 0, 0, true),
                TextFunc = () => (Hud.Game.Me.Defense.CurrentEffectiveHealingPercent > 0 ? "+" : "") + Hud.Game.Me.Defense.CurrentEffectiveHealingPercent.ToString("#,0", CultureInfo.InvariantCulture) + "%",
            };

            HealingNegDecorator = new TopLabelDecorator(Hud)
            {
                TextFont = Hud.Render.CreateFont("tahoma", 8, 255, 255, 130, 130, true, false, 255, 100, 0, 0, true),
                TextFunc = () => (Hud.Game.Me.Defense.CurrentEffectiveHealingPercent > 0 ? "+" : "") + Hud.Game.Me.Defense.CurrentEffectiveHealingPercent.ToString("#,0", CultureInfo.InvariantCulture) + "%",
            };

            ShieldDecorator = new TopLabelDecorator(Hud)
            {
                TextFont = Hud.Render.CreateFont("tahoma", 8, 255, 160, 160, 215, true, false, 255, 100, 0, 0, true),
                TextFunc = () => Hud.Game.Me.Defense.CurShield.ToString("#,0", CultureInfo.InvariantCulture),
            };

            ArcaneValueDecorator = new TopLabelDecorator(Hud)
            {
                TextFont = Hud.Render.CreateFont("tahoma", 12, 255, 200, 120, 255, true, false, 128, 0, 0, 0, true),
                TextFunc = () => Hud.Game.Me.Stats.ResourceCurArcane.ToString("F0", CultureInfo.InvariantCulture),
            };

            ArcaneRegenDecorator = new TopLabelDecorator(Hud)
            {
                TextFont = Hud.Render.CreateFont("tahoma", 8, 255, 200, 120, 255, true, false, 128, 0, 0, 0, true),
                TextFunc = () => Hud.Game.Me.Stats.ResourceRegArcane <= 0 ? null : "+" + Hud.Game.Me.Stats.ResourceRegArcane.ToString("F0", CultureInfo.InvariantCulture) + "/s",
            };

            ManaValueDecorator = new TopLabelDecorator(Hud)
            {
                TextFont = Hud.Render.CreateFont("tahoma", 12, 255, 160, 160, 255, true, false, 128, 0, 0, 0, true),
                TextFunc = () => Hud.Game.Me.Stats.ResourceCurMana.ToString("F0", CultureInfo.InvariantCulture),
            };

            ManaRegenDecorator = new TopLabelDecorator(Hud)
            {
                TextFont = Hud.Render.CreateFont("tahoma", 8, 255, 160, 160, 255, true, false, 128, 0, 0, 0, true),
                TextFunc = () => Hud.Game.Me.Stats.ResourceRegMana <= 0 ? null : "+" + Hud.Game.Me.Stats.ResourceRegMana.ToString("F0", CultureInfo.InvariantCulture) + "/s",
            };

            SpiritValueDecorator = new TopLabelDecorator(Hud)
            {
                TextFont = Hud.Render.CreateFont("tahoma", 12, 255, 255, 255, 160, true, false, 128, 0, 0, 0, true),
                TextFunc = () => Hud.Game.Me.Stats.ResourceCurSpirit.ToString("F0", CultureInfo.InvariantCulture),
            };

            SpiritRegenDecorator = new TopLabelDecorator(Hud)
            {
                TextFont = Hud.Render.CreateFont("tahoma", 8, 255, 255, 255, 160, true, false, 128, 0, 0, 0, true),
                TextFunc = () => Hud.Game.Me.Stats.ResourceRegSpirit <= 0 ? null : "+" + Hud.Game.Me.Stats.ResourceRegSpirit.ToString("F0", CultureInfo.InvariantCulture) + "/s",
            };

            FuryValueDecorator = new TopLabelDecorator(Hud)
            {
                TextFont = Hud.Render.CreateFont("tahoma", 12, 255, 255, 220, 60, true, false, 128, 0, 0, 0, true),
                TextFunc = () => Hud.Game.Me.Stats.ResourceCurFury.ToString("F0", CultureInfo.InvariantCulture),
            };

            FuryRegenDecorator = new TopLabelDecorator(Hud)
            {
                TextFont = Hud.Render.CreateFont("tahoma", 8, 255, 255, 220, 60, true, false, 128, 0, 0, 0, true),
                TextFunc = () => Hud.Game.Me.Stats.ResourceRegFury <= 0 ? null : "+" + Hud.Game.Me.Stats.ResourceRegFury.ToString("F0", CultureInfo.InvariantCulture) + "/s",
            };

            HatredValueDecorator = new TopLabelDecorator(Hud)
            {
                TextFont = Hud.Render.CreateFont("tahoma", 12, 255, 255, 160, 160, true, false, 128, 0, 0, 0, true),
                TextFunc = () => Hud.Game.Me.Stats.ResourceCurHatred.ToString("F0", CultureInfo.InvariantCulture),
            };

            HatredRegenDecorator = new TopLabelDecorator(Hud)
            {
                TextFont = Hud.Render.CreateFont("tahoma", 8, 255, 255, 160, 160, true, false, 128, 0, 0, 0, true),
                TextFunc = () => Hud.Game.Me.Stats.ResourceRegHatred <= 0 ? null : "+" + Hud.Game.Me.Stats.ResourceRegHatred.ToString("F0", CultureInfo.InvariantCulture) + "/s",
            };

            DisciplineValueDecorator = new TopLabelDecorator(Hud)
            {
                TextFont = Hud.Render.CreateFont("tahoma", 12, 255, 160, 160, 255, true, false, 128, 0, 0, 0, true),
                TextFunc = () => Hud.Game.Me.Stats.ResourceCurDiscipline.ToString("F0", CultureInfo.InvariantCulture),
            };

            DisciplineRegenDecorator = new TopLabelDecorator(Hud)
            {
                TextFont = Hud.Render.CreateFont("tahoma", 8, 255, 160, 160, 255, true, false, 128, 0, 0, 0, true),
                TextFunc = () => Hud.Game.Me.Stats.ResourceRegDiscipline <= 0 ? null : "+" + Hud.Game.Me.Stats.ResourceRegDiscipline.ToString("F0", CultureInfo.InvariantCulture) + "/s",
            };

            WrathValueDecorator = new TopLabelDecorator(Hud)
            {
                TextFont = Hud.Render.CreateFont("tahoma", 12, 255, 255, 220, 60, true, false, 128, 0, 0, 0, true),
                TextFunc = () => Hud.Game.Me.Stats.ResourceCurWrath.ToString("F0", CultureInfo.InvariantCulture),
            };

            WrathRegenDecorator = new TopLabelDecorator(Hud)
            {
                TextFont = Hud.Render.CreateFont("tahoma", 8, 255, 255, 220, 60, true, false, 128, 0, 0, 0, true),
                TextFunc = () => Hud.Game.Me.Stats.ResourceRegWrath <= 0 ? null : "+" + Hud.Game.Me.Stats.ResourceRegWrath.ToString("F0", CultureInfo.InvariantCulture) + "/s",
            };

            EssenceValueDecorator = new TopLabelDecorator(Hud)
            {
                TextFont = Hud.Render.CreateFont("tahoma", 12, 255, 175, 238, 238, true, false, 128, 0, 0, 0, true),
                TextFunc = () => Hud.Game.Me.Stats.ResourceCurEssence.ToString("F0", CultureInfo.InvariantCulture),
            };

            EssenceRegenDecorator = new TopLabelDecorator(Hud)
            {
                TextFont = Hud.Render.CreateFont("tahoma", 8, 255, 175, 238, 238, true, false, 128, 0, 0, 0, true),
                TextFunc = () => Hud.Game.Me.Stats.ResourceRegEssence <= 0 ? null : "+" + Hud.Game.Me.Stats.ResourceRegEssence.ToString("F0", CultureInfo.InvariantCulture) + "/s",
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

            var uiRect = Hud.Render.GetUiElement("Root.NormalLayer.game_dialog_backgroundScreenPC.game_progressBar_healthBall").Rectangle;

            var glowTexture = Hud.Texture.GetTexture(1981524232);
            glowTexture.Draw(uiRect.Left + (uiRect.Width * 0.6f) - (uiRect.Height * 0.4f), uiRect.Top + (uiRect.Height * 0.32f) - (uiRect.Height * 0.4f), uiRect.Height * 0.8f, uiRect.Height * 0.8f, opacityMultiplier: 0.7f);

            HealthDecorator.Paint(uiRect.Left + (uiRect.Width * 0.2f), uiRect.Top + (uiRect.Height * 0.26f), uiRect.Width * 0.8f, uiRect.Height * 0.15f, HorizontalAlign.Center);

            if (Hud.Game.Me.Defense.CurrentEffectiveHealingPercent != 0)
            {
                (Hud.Game.Me.Defense.CurrentEffectiveHealingPercent > 0 ? HealingPosDecorator : HealingNegDecorator).Paint(uiRect.Left + (uiRect.Width * 0.2f), uiRect.Top + (uiRect.Height * 0.42f), uiRect.Width * 0.8f, uiRect.Height * 0.1f, HorizontalAlign.Center);
            }

            if (Hud.Game.Me.Defense.CurShield > 0)
            {
                ShieldDecorator.Paint(uiRect.Left + (uiRect.Width * 0.2f), uiRect.Top + (uiRect.Height * 0.66f), uiRect.Width * 0.63f, uiRect.Height * 0.12f, HorizontalAlign.Right);
            }

            uiRect = Hud.Render.GetUiElement("Root.NormalLayer.game_dialog_backgroundScreenPC.game_progressBar_manaBall").Rectangle;

            switch (Hud.Game.Me.HeroClassDefinition.HeroClass)
            {
                case HeroClass.Barbarian:
                    FuryValueDecorator.Paint(uiRect.Left, uiRect.Top + (uiRect.Height * 0.30f), uiRect.Width, uiRect.Height * 0.15f, HorizontalAlign.Center);
                    FuryRegenDecorator.Paint(uiRect.Left, uiRect.Top + (uiRect.Height * 0.43f), uiRect.Width, uiRect.Height * 0.15f, HorizontalAlign.Center);
                    break;
                case HeroClass.Crusader:
                    WrathValueDecorator.Paint(uiRect.Left, uiRect.Top + (uiRect.Height * 0.30f), uiRect.Width, uiRect.Height * 0.15f, HorizontalAlign.Center);
                    WrathRegenDecorator.Paint(uiRect.Left, uiRect.Top + (uiRect.Height * 0.43f), uiRect.Width, uiRect.Height * 0.15f, HorizontalAlign.Center);
                    break;
                case HeroClass.DemonHunter:
                    HatredValueDecorator.Paint(uiRect.Left, uiRect.Top + (uiRect.Height * 0.30f), uiRect.Width * 0.5f, uiRect.Height * 0.15f, HorizontalAlign.Center);
                    DisciplineValueDecorator.Paint(uiRect.Left + (uiRect.Width * 0.5f), uiRect.Top + (uiRect.Height * 0.30f), uiRect.Width * 0.5f, uiRect.Height * 0.15f, HorizontalAlign.Center);
                    HatredRegenDecorator.Paint(uiRect.Left, uiRect.Top + (uiRect.Height * 0.43f), uiRect.Width * 0.5f, uiRect.Height * 0.15f, HorizontalAlign.Center);
                    DisciplineRegenDecorator.Paint(uiRect.Left + (uiRect.Width * 0.5f), uiRect.Top + (uiRect.Height * 0.43f), uiRect.Width * 0.5f, uiRect.Height * 0.15f, HorizontalAlign.Center);
                    break;
                case HeroClass.Monk:
                    SpiritValueDecorator.Paint(uiRect.Left, uiRect.Top + (uiRect.Height * 0.30f), uiRect.Width, uiRect.Height * 0.15f, HorizontalAlign.Center);
                    SpiritRegenDecorator.Paint(uiRect.Left, uiRect.Top + (uiRect.Height * 0.43f), uiRect.Width, uiRect.Height * 0.15f, HorizontalAlign.Center);
                    break;
                case HeroClass.Necromancer:
                    EssenceValueDecorator.Paint(uiRect.Left, uiRect.Top + (uiRect.Height * 0.30f), uiRect.Width, uiRect.Height * 0.15f, HorizontalAlign.Center);
                    EssenceRegenDecorator.Paint(uiRect.Left, uiRect.Top + (uiRect.Height * 0.43f), uiRect.Width, uiRect.Height * 0.15f, HorizontalAlign.Center);
                    break;
                case HeroClass.WitchDoctor:
                    ManaValueDecorator.Paint(uiRect.Left, uiRect.Top + (uiRect.Height * 0.30f), uiRect.Width, uiRect.Height * 0.15f, HorizontalAlign.Center);
                    ManaRegenDecorator.Paint(uiRect.Left, uiRect.Top + (uiRect.Height * 0.43f), uiRect.Width, uiRect.Height * 0.15f, HorizontalAlign.Center);
                    break;
                case HeroClass.Wizard:
                    ArcaneValueDecorator.Paint(uiRect.Left, uiRect.Top + (uiRect.Height * 0.30f), uiRect.Width, uiRect.Height * 0.15f, HorizontalAlign.Center);
                    ArcaneRegenDecorator.Paint(uiRect.Left, uiRect.Top + (uiRect.Height * 0.43f), uiRect.Width, uiRect.Height * 0.15f, HorizontalAlign.Center);
                    break;
            }
        }
    }
}