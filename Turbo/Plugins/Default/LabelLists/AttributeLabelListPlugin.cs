using System.Collections.Generic;
using System.Globalization;

namespace Turbo.Plugins.Default
{
    public class AttributeLabelListPlugin : BasePlugin, IInGameTopPainter
    {
        public HorizontalTopLabelList LabelList { get; private set; }

        public AttributeLabelListPlugin()
        {
            Enabled = true;
        }

        public override void Load(IController hud)
        {
            base.Load(hud);

            var expandedHintFont = Hud.Render.CreateFont("tahoma", 7, 255, 200, 200, 200, false, false, true);
            var expandedHintWidthMultiplier = 3;

            LabelList = new HorizontalTopLabelList(hud)
            {
                LeftFunc = () =>
                {
                    var ui = Hud.Render.InGameBottomHudUiElement;
                    return ui.Rectangle.Left + (ui.Rectangle.Width * 0.267f);
                },
                TopFunc = () =>
                {
                    var ui = Hud.Render.InGameBottomHudUiElement;
                    return ui.Rectangle.Top + (ui.Rectangle.Height * 0.318f);
                },
                WidthFunc = () =>
                {
                    var ui = Hud.Render.InGameBottomHudUiElement;
                    return Hud.Window.Size.Height * 0.0621f;
                },
                HeightFunc = () =>
                {
                    var ui = Hud.Render.InGameBottomHudUiElement;
                    return Hud.Window.Size.Height * 0.025f;
                }
            };

            LabelList.LabelDecorators.Add(new TopLabelDecorator(Hud)
            {
                TextFont = Hud.Render.CreateFont("tahoma", 7, 180, 255, 255, 255, true, false, true),
                BackgroundTexture1 = Hud.Texture.ButtonTextureGray,
                BackgroundTexture2 = Hud.Texture.BackgroundTextureGreen,
                BackgroundTextureOpacity2 = 0.75f,
                TextFunc = () => ValueToString(Hud.Game.Me.Defense.EhpCur, ValueFormat.ShortNumber),
                HintFunc = () => "EHP current",
            });

            LabelList.LabelDecorators.Add(new TopLabelDecorator(Hud)
            {
                TextFont = Hud.Render.CreateFont("tahoma", 7, 180, 255, 255, 255, true, false, true),
                ExpandedHintFont = expandedHintFont,
                ExpandedHintWidthMultiplier = expandedHintWidthMultiplier,
                BackgroundTexture1 = Hud.Texture.ButtonTextureGray,
                BackgroundTexture2 = Hud.Texture.BackgroundTextureGreen,
                BackgroundTextureOpacity2 = 0.70f,
                TextFunc = () => ValueToString(Hud.Game.Me.Defense.EhpMax, ValueFormat.ShortNumber),
                HintFunc = () => "EHP max",
                ExpandUpLabels = new List<TopLabelDecorator>()
                {
                    new TopLabelDecorator(Hud)
                    {
                        TextFont = Hud.Render.CreateFont("tahoma", 7, 180, 255, 255, 255, true, false, true),
                        ExpandedHintFont = expandedHintFont,
                        ExpandedHintWidthMultiplier = expandedHintWidthMultiplier,
                        BackgroundTexture1 = Hud.Texture.ButtonTextureGray,
                        BackgroundTexture2 = Hud.Texture.BackgroundTextureGreen,
                        BackgroundTextureOpacity2 = 1.0f,
                        TextFunc = () => (Hud.Game.Me.Defense.drCombined * 100).ToString("F1", CultureInfo.InvariantCulture),
                        HintFunc = () => "damage reduction",
                    },
                    new TopLabelDecorator(Hud)
                    {
                        TextFont = Hud.Render.CreateFont("tahoma", 7, 180, 255, 255, 255, true, false, true),
                        ExpandedHintFont = expandedHintFont,
                        ExpandedHintWidthMultiplier = expandedHintWidthMultiplier,
                        BackgroundTexture1 = Hud.Texture.ButtonTextureGray,
                        BackgroundTexture2 = Hud.Texture.BackgroundTextureGreen,
                        BackgroundTextureOpacity2 = 1.0f,
                        TextFunc = () => Hud.Game.Me.Defense.Armor.ToString("#,0", CultureInfo.InvariantCulture),
                        HintFunc = () => "armor",
                    },
                    new TopLabelDecorator(Hud)
                    {
                        TextFont = Hud.Render.CreateFont("tahoma", 7, 180, 255, 255, 255, true, false, true),
                        ExpandedHintFont = expandedHintFont,
                        ExpandedHintWidthMultiplier = expandedHintWidthMultiplier,
                        BackgroundTexture1 = Hud.Texture.ButtonTextureGray,
                        BackgroundTexture2 = Hud.Texture.BackgroundTextureGreen,
                        BackgroundTextureOpacity2 = 1.0f,
                        TextFunc = () => Hud.Game.Me.Defense.ResAverage.ToString("F0", CultureInfo.InvariantCulture),
                        HintFunc = () => "average resist",
                    },
                }
            });

            LabelList.LabelDecorators.Add(new TopLabelDecorator(Hud)
            {
                TextFont = Hud.Render.CreateFont("tahoma", 7, 180, 255, 255, 255, true, false, true),
                ExpandedHintFont = expandedHintFont,
                ExpandedHintWidthMultiplier = expandedHintWidthMultiplier,
                BackgroundTexture1 = Hud.Texture.ButtonTextureOrange,
                BackgroundTexture2 = Hud.Texture.BackgroundTextureOrange,
                BackgroundTextureOpacity2 = 0.5f,
                TextFunc = () => ValueToString(Hud.Game.Me.Offense.SheetDps, ValueFormat.ShortNumber),
                HintFunc = () => "sheet DPS",
                ExpandUpLabels = new List<TopLabelDecorator>()
                {
                    new TopLabelDecorator(Hud)
                    {
                        TextFont = Hud.Render.CreateFont("tahoma", 7, 180, 255, 255, 255, false, false, true),
                        ExpandedHintFont = expandedHintFont,
                        ExpandedHintWidthMultiplier = expandedHintWidthMultiplier,
                        BackgroundTexture1 = Hud.Texture.ButtonTextureOrange,
                        BackgroundTexture2 = Hud.Texture.BackgroundTextureYellow,
                        BackgroundTextureOpacity2 = 0.75f,
                        TextFunc = () => ValueToString(Hud.Game.Me.Offense.MainHandIsActive ? Hud.Game.Me.Offense.WeaponDamageMainHand : Hud.Game.Me.Offense.WeaponDamageSecondHand, ValueFormat.ShortNumber),
                        HintFunc = () => "weapon damage",
                    },
                    new TopLabelDecorator(Hud)
                    {
                        TextFont = Hud.Render.CreateFont("tahoma", 7, 180, 255, 255, 255, false, false, true),
                        ExpandedHintFont = expandedHintFont,
                        ExpandedHintWidthMultiplier = expandedHintWidthMultiplier,
                        BackgroundTexture1 = Hud.Texture.ButtonTextureOrange,
                        BackgroundTexture2 = Hud.Texture.BackgroundTextureYellow,
                        BackgroundTextureOpacity2 = 0.75f,
                        TextFunc = () => Hud.Game.Me.Offense.AttackSpeed.ToString("F2", CultureInfo.InvariantCulture) + "/s",
                        HintFunc = () => "attack speed",
                    },
                    new TopLabelDecorator(Hud)
                    {
                        TextFont = Hud.Render.CreateFont("tahoma", 7, 180, 255, 255, 255, false, false, true),
                        ExpandedHintFont = expandedHintFont,
                        ExpandedHintWidthMultiplier = expandedHintWidthMultiplier,
                        BackgroundTexture1 = Hud.Texture.ButtonTextureOrange,
                        BackgroundTexture2 = Hud.Texture.BackgroundTextureYellow,
                        BackgroundTextureOpacity2 = 0.75f,
                        TextFunc = () => Hud.Game.Me.Offense.CriticalHitChance.ToString("F1", CultureInfo.InvariantCulture) + "%",
                        HintFunc = () => "critical hit chance",
                    },
                    new TopLabelDecorator(Hud)
                    {
                        TextFont = Hud.Render.CreateFont("tahoma", 7, 180, 255, 255, 255, false, false, true),
                        ExpandedHintFont = expandedHintFont,
                        ExpandedHintWidthMultiplier = expandedHintWidthMultiplier,
                        BackgroundTexture1 = Hud.Texture.ButtonTextureOrange,
                        BackgroundTexture2 = Hud.Texture.BackgroundTextureYellow,
                        BackgroundTextureOpacity2 = 0.75f,
                        TextFunc = () => Hud.Game.Me.Offense.CritDamage.ToString("F0", CultureInfo.InvariantCulture) + "%",
                        HintFunc = () => "critical hit damage",
                    }
                }
            });

            LabelList.LabelDecorators.Add(new TopLabelDecorator(Hud)
            {
                TextFont = Hud.Render.CreateFont("tahoma", 7, 120, 255, 255, 255, false, false, true),
                BackgroundTexture1 = Hud.Texture.ButtonTextureOrange,
                BackgroundTexture2 = Hud.Texture.BackgroundTextureGreen,
                BackgroundTextureOpacity2 = 0.3f,
                TextFunc = () => Hud.Game.Me.Offense.AttackSpeed.ToString("F2", CultureInfo.InvariantCulture) + "/s",
                HintFunc = () => "attack speed",
            });

            LabelList.LabelDecorators.Add(new TopLabelDecorator(Hud)
            {
                TextFont = Hud.Render.CreateFont("tahoma", 7, 120, 255, 255, 255, false, false, true),
                BackgroundTexture1 = Hud.Texture.ButtonTextureOrange,
                BackgroundTexture2 = Hud.Texture.BackgroundTextureGreen,
                BackgroundTextureOpacity2 = 0.3f,
                TextFunc = () => Hud.Game.Me.Offense.CriticalHitChance.ToString("F1", CultureInfo.InvariantCulture) + "%",
                HintFunc = () => "critical hit chance",
            });

            LabelList.LabelDecorators.Add(new TopLabelDecorator(Hud)
            {
                TextFont = Hud.Render.CreateFont("tahoma", 7, 120, 255, 255, 255, false, false, true),
                BackgroundTexture1 = Hud.Texture.ButtonTextureOrange,
                BackgroundTexture2 = Hud.Texture.BackgroundTextureGreen,
                BackgroundTextureOpacity2 = 0.3f,
                TextFunc = () => Hud.Game.Me.Offense.CritDamage.ToString("F0", CultureInfo.InvariantCulture) + "%",
                HintFunc = () => "critical hit damage",
            });

            LabelList.LabelDecorators.Add(new TopLabelDecorator(Hud)
            {
                TextFont = Hud.Render.CreateFont("tahoma", 7, 120, 200, 200, 255, false, false, true),
                BackgroundTexture1 = Hud.Texture.ButtonTextureOrange,
                BackgroundTexture2 = Hud.Texture.BackgroundTextureBlue,
                BackgroundTextureOpacity2 = 0.75f,
                TextFunc = () => Hud.Game.Me.Offense.AreaDamageBonus.ToString("F0", CultureInfo.InvariantCulture) + "%",
                HintFunc = () => "area damage bonus %",
            });

            LabelList.LabelDecorators.Add(new TopLabelDecorator(Hud)
            {
                TextFont = Hud.Render.CreateFont("tahoma", 7, 120, 200, 200, 255, false, false, true),
                BackgroundTexture1 = Hud.Texture.ButtonTextureBlue,
                BackgroundTexture2 = Hud.Texture.BackgroundTextureBlue,
                BackgroundTextureOpacity2 = 0.75f,
                TextFunc = () => (Hud.Game.Me.Stats.CooldownReduction * 100).ToString("F0", CultureInfo.InvariantCulture) + "%",
                HintFunc = () => "cooldown reduction %",
            });

            LabelList.LabelDecorators.Add(new TopLabelDecorator(Hud)
            {
                TextFont = Hud.Render.CreateFont("tahoma", 7, 120, 255, 200, 200, false, false, true),
                BackgroundTexture1 = Hud.Texture.ButtonTextureBlue,
                BackgroundTexture2 = Hud.Texture.BackgroundTextureBlue,
                BackgroundTextureOpacity2 = 0.75f,
                TextFunc = () => (Hud.Game.Me.Stats.ResourceCostReduction * 100).ToString("F0", CultureInfo.InvariantCulture) + "%",
                HintFunc = () => "resource cost reduction",
            });

            LabelList.LabelDecorators.Add(new TopLabelDecorator(Hud)
            {
                TextFont = Hud.Render.CreateFont("tahoma", 7, 120, 255, 200, 200, false, false, true),
                BackgroundTexture1 = Hud.Texture.ButtonTextureOrange,
                BackgroundTexture2 = Hud.Texture.BackgroundTextureBlue,
                BackgroundTextureOpacity2 = 0.75f,
                TextFunc = () => ValueToString(Hud.Game.ExperiencePerHourToday, ValueFormat.ShortNumber) + "/h",
                HintFunc = () => "experience per hour today",
            });
        }

        public void PaintTopInGame(ClipState clipState)
        {
            if (clipState != ClipState.BeforeClip)
                return;
            if ((Hud.Game.MapMode == MapMode.WaypointMap) || (Hud.Game.MapMode == MapMode.ActMap) || (Hud.Game.MapMode == MapMode.Map))
                return;

            LabelList.Paint();
        }
    }
}