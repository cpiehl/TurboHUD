using System;
using System.Collections.Generic;
using System.Linq;

namespace Turbo.Plugins.Default
{
    public class WaypointQuestsPlugin : BasePlugin, ITransparentCollection, ITransparent, IInGameTopPainter
    {
        public IFont BountyNameNormalFont { get; set; }
        public IFont BountyNameHighlightedFont { get; set; }

        public List<ISnoQuest> BountiesToHighlight { get; } = new List<ISnoQuest>();

        public float Opacity { get; set; }

        public WaypointQuestsPlugin()
        {
            Enabled = true;
        }

        public override void Load(IController hud)
        {
            base.Load(hud);

            Order = 30000;

            BountiesToHighlight.Add(Hud.Sno.SnoQuests.Bounty_KillTheDataminer_347095);
            BountiesToHighlight.Add(Hud.Sno.SnoQuests.Bounty_KillCaptainClegg_361354);
            BountiesToHighlight.Add(Hud.Sno.SnoQuests.Bounty_KillRadnoj_361352);
            BountiesToHighlight.Add(Hud.Sno.SnoQuests.Bounty_ClearTheCryptOfTheAncients_345517);
            BountiesToHighlight.Add(Hud.Sno.SnoQuests.Bounty_ClearWarriorsRest_345520);
            BountiesToHighlight.Add(Hud.Sno.SnoQuests.Bounty_CarrionFarm_345500);
            BountiesToHighlight.Add(Hud.Sno.SnoQuests.Bounty_EternalWar_345505);
            BountiesToHighlight.Add(Hud.Sno.SnoQuests.Bounty_LastStandOfTheAncients_345502);
            BountiesToHighlight.Add(Hud.Sno.SnoQuests.Bounty_TheCursedCellar_369944);

            BountyNameNormalFont = Hud.Render.CreateFont("tahoma", 7f, 255, 80, 180, 255, true, true, 160, 0, 0, 0, true);
            BountyNameHighlightedFont = Hud.Render.CreateFont("tahoma", 7f, 255, 255, 255, 0, true, true, 160, 0, 0, 0, true);
        }

        public void PaintTopInGame(ClipState clipState)
        {
            if (Hud.Render.UiHidden)
                return;
            if (clipState != ClipState.AfterClip)
                return;
            if (!Hud.Render.WorldMapUiElement.Visible || Hud.Render.ActMapUiElement.Visible)
                return;

            var mapCurrentAct = Hud.Game.ActMapCurrentAct;
            if (mapCurrentAct == BountyAct.None)
                return;

            var w = 220 * Hud.Window.HeightUiRatio;
            var h = 100 * Hud.Window.HeightUiRatio;

            foreach (var waypoint in Hud.Game.ActMapWaypoints.Where(x => x.BountyAct == mapCurrentAct))
            {
                var quest = Hud.Game.Bounties.FirstOrDefault(x => x.SnoQuest.BountySnoArea == waypoint.TargetSnoArea);
                if ((quest != null) && quest.State != QuestState.completed)
                {
                    var x = Hud.Render.WorldMapUiElement.Rectangle.X + (waypoint.CoordinateOnMapUiElement.X * Hud.Window.HeightUiRatio);
                    var y = Hud.Render.WorldMapUiElement.Rectangle.Y + (waypoint.CoordinateOnMapUiElement.Y * Hud.Window.HeightUiRatio);

                    var font = BountiesToHighlight.Contains(quest.SnoQuest) ? BountyNameHighlightedFont : BountyNameNormalFont;

                    var layout = font.GetTextLayout(quest.SnoQuest.NameLocalized);
                    font.DrawText(layout, x + ((w - layout.Metrics.Width) / 2), y + (float)Math.Ceiling(h * 0.32f));
                }
            }
        }

        public IEnumerable<ITransparent> GetTransparents()
        {
            yield return BountyNameNormalFont;
            yield return BountyNameHighlightedFont;
            yield return this;
        }
    }
}