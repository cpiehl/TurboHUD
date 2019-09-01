using System;
using System.Collections.Generic;
using System.Globalization;

namespace Turbo.Plugins.Default
{
    public class TopExperienceStatistics : BasePlugin, IInGameTopPainter
    {
        public HorizontalTopLabelList LabelList { get; private set; }

        public TopExperienceStatistics()
        {
            Enabled = true;
        }

        public override void Load(IController hud)
        {
            base.Load(hud);

            var expandedHintFont = Hud.Render.CreateFont("tahoma", 6, 255, 200, 200, 200, false, false, true);

            LabelList = new HorizontalTopLabelList(hud)
            {
                LeftFunc = () => (Hud.Window.Size.Width / 2) - (Hud.Window.Size.Height * 0.08f),
                TopFunc = () => Hud.Window.Size.Height * 0.001f,
                WidthFunc = () => Hud.Window.Size.Height * 0.08f,
                HeightFunc = () => Hud.Window.Size.Height * 0.018f,
            };

            var currentLevelDecorator = new TopLabelDecorator(Hud)
            {
                TextFont = Hud.Render.CreateFont("tahoma", 6, 255, 57, 137, 205, true, false, true),
                BackgroundTexture1 = Hud.Texture.Button2TextureOrange,
                BackgroundTexture2 = Hud.Texture.BackgroundTextureBlue,
                BackgroundTextureOpacity1 = 1.0f,
                BackgroundTextureOpacity2 = 0.5f,
                TextFunc = () => (Hud.Game.Me.CurrentLevelNormal < Hud.Game.Me.CurrentLevelNormalCap) ? Hud.Game.Me.CurrentLevelNormal.ToString("0") : "p" + Hud.Game.Me.CurrentLevelParagonDouble.ToString("0.##", CultureInfo.InvariantCulture),
                ExpandDownLabels = new List<TopLabelDecorator>(),
            };

            foreach (var levelIncrement in new uint[] { 1, 2, 5, 10, 20, 50, 100, 250, 500, 1000 })
            {
                currentLevelDecorator.ExpandDownLabels.Add(
                    new TopLabelDecorator(Hud)
                    {
                        TextFont = Hud.Render.CreateFont("tahoma", 6, 180, 255, 255, 255, true, false, true),
                        ExpandedHintFont = expandedHintFont,
                        ExpandedHintWidthMultiplier = 2,
                        BackgroundTexture1 = Hud.Texture.Button2TextureOrange,
                        BackgroundTexture2 = Hud.Texture.BackgroundTextureBlue,
                        BackgroundTextureOpacity1 = 1.0f,
                        BackgroundTextureOpacity2 = 0.5f,
                        HideBackgroundWhenTextIsEmpty = true,
                        TextFunc = () => Hud.Game.Me.CurrentLevelNormal >= Hud.Game.Me.CurrentLevelNormalCap ? ("p" + (Hud.Game.Me.CurrentLevelParagon + levelIncrement).ToString("D", CultureInfo.InvariantCulture)) : null,
                        HintFunc = () => ExpToParagonLevel(Hud.Game.Me.CurrentLevelParagon + levelIncrement) + " = " + TimeToParagonLevel(Hud.Game.Me.CurrentLevelParagon + levelIncrement, false),
                    });
            }

            LabelList.LabelDecorators.Add(currentLevelDecorator);

            LabelList.LabelDecorators.Add(new TopLabelDecorator(Hud)
            {
                TextFont = Hud.Render.CreateFont("tahoma", 6, 255, 57, 137, 205, true, false, true),
                BackgroundTexture1 = Hud.Texture.Button2TextureOrange,
                BackgroundTexture2 = Hud.Texture.BackgroundTextureBlue,
                BackgroundTextureOpacity1 = 1.0f,
                BackgroundTextureOpacity2 = 0.5f,
                TextFunc = () => ValueToString(Hud.Game.CurrentHeroToday.GainedExperiencePerHourPlay, ValueFormat.ShortNumber) + "/h",
            });
        }

        public void PaintTopInGame(ClipState clipState)
        {
            if (clipState != ClipState.BeforeClip)
                return;

            LabelList.Paint();
        }

        public string ExpToParagonLevel(uint paragonLevel)
        {
            if (paragonLevel > Hud.Game.Me.CurrentLevelParagon)
            {
                var xpRequired = Hud.Sno.TotalParagonExperienceRequired(paragonLevel);
                var xpRemaining = xpRequired - Hud.Game.Me.ParagonTotalExp;
                return ValueToString(xpRemaining, ValueFormat.LongNumber);
            }

            return null;
        }

        public string TimeToParagonLevel(uint paragonLevel, bool includetext)
        {
            var tracker = Hud.Game.CurrentHeroToday;
            if (tracker != null)
            {
                if (paragonLevel > Hud.Game.Me.CurrentLevelParagon)
                {
                    var text = includetext ? "p" + paragonLevel.ToString("D", CultureInfo.InvariantCulture) + ": " : "";
                    var xph = tracker.GainedExperiencePerHourPlay;
                    if (xph > 0)
                    {
                        var xpRequired = Hud.Sno.TotalParagonExperienceRequired(paragonLevel);
                        var xpRemaining = xpRequired - Hud.Game.Me.ParagonTotalExp;
                        var hours = xpRemaining / xph;
                        var ticks = Convert.ToInt64(Math.Ceiling(hours * 60.0d * 60.0d * 1000.0d * TimeSpan.TicksPerMillisecond));
                        text += ValueToString(ticks, ValueFormat.LongTimeNoSeconds);
                    }
                    else
                    {
                        text += "-";
                    }

                    return text;
                }
            }

            return null;
        }
    }
}