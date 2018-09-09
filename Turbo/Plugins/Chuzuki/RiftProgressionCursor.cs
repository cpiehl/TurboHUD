using Turbo.Plugins.Default;
using SharpDX;
using SharpDX.Direct2D1;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Turbo.Plugins.Chuzuki
{
    public class RiftProgressionCursor : BasePlugin, IInGameWorldPainter
    {
		public Dictionary<string, WorldDecoratorCollection> CursorDecorators { get; set; }
		public Dictionary<string, IBrush> BackgroundBrushes { get; set; }
        public float Radius { get; set; }

		private double progressionMax = 500.0f;
		private float rad = 0.0f;

        public RiftProgressionCursor()
        {
            Enabled = true;
			Radius = 40;
        }

        public override void Load(IController hud)
        {
            base.Load(hud);
			InitDecorators();
			rad = Radius / 1200.0f * Hud.Window.Size.Height;
        }

        public void PaintWorld(WorldLayer layer)
        {
			if ((Hud.Game.SpecialArea != SpecialArea.Rift) && (Hud.Game.SpecialArea != SpecialArea.GreaterRift) && (Hud.Game.SpecialArea != SpecialArea.ChallengeRift)) return;

			var cursorScreenCoord = Hud.Window.CreateScreenCoordinate(Hud.Window.CursorX, Hud.Window.CursorY);
            var visorWorldCoord = cursorScreenCoord.ToWorldCoordinate();

			string color = "red";

			// rift progression per hitpoint
			double nearProgressionSum = CalculateNearMonsterProgression(40, visorWorldCoord); // / Hud.Game.MaxQuestProgress * 100d;
			// rift progression per second = rp/hp * hp/s
			double progressionPerSecond = nearProgressionSum * Hud.Game.Me.Damage.RunDps / Hud.Game.MaxQuestProgress * 1000;
			if (progressionPerSecond < 100)
				color = "red";
			else if (progressionPerSecond < 250)
				color = "yellow";
			else
				color = "green";

			CursorDecorators[color].Paint(layer, null, visorWorldCoord, progressionPerSecond.ToString("0.000"));

            var endAngle = (int)(360 / progressionMax * (progressionMax - progressionPerSecond)) - 90;
            var startAngle = 270;

			using (var pg = Hud.Render.CreateGeometry())
            {
                using (var gs = pg.Open())
                {
                    gs.BeginFigure(new Vector2(cursorScreenCoord.X, cursorScreenCoord.Y), FigureBegin.Filled);
                    for (int angle = startAngle; angle > endAngle; angle--)
                    {
                        var mx = rad * (float)Math.Cos(angle * Math.PI / 180.0f);
                        var my = rad * (float)Math.Sin(angle * Math.PI / 180.0f);
                        var vector = new Vector2(cursorScreenCoord.X + mx, cursorScreenCoord.Y + my);
                        gs.AddLine(vector);
                    }
                    gs.EndFigure(FigureEnd.Closed);
                    gs.Close();
                }
                BackgroundBrushes[color].DrawGeometry(pg);
            }
		}

		private void InitDecorators()
        {
			CursorDecorators = new Dictionary<string, WorldDecoratorCollection>();
			CursorDecorators["red"] = GetCursorDecorator(new Color(128, 255, 64, 64));
			CursorDecorators["yellow"] = GetCursorDecorator(new Color(128, 255, 255, 64));
			CursorDecorators["green"] = GetCursorDecorator(new Color(128, 64, 255, 128));

			BackgroundBrushes = new Dictionary<string, IBrush>();
			BackgroundBrushes["red"] = Hud.Render.CreateBrush(128, 255, 64, 64, 0);
			BackgroundBrushes["yellow"] = Hud.Render.CreateBrush(128, 255, 255, 64, 0);
			BackgroundBrushes["green"] = Hud.Render.CreateBrush(128, 64, 255, 64, 0);
		}

		private WorldDecoratorCollection GetCursorDecorator(Color color)
		{
			int stroke = (Radius == -1) ? 0 : 2;
			return new WorldDecoratorCollection
			(
				new GroundCircleDecorator(Hud)
                {
                    Brush = Hud.Render.CreateBrush(color.A, color.R, color.G, color.B, stroke, SharpDX.Direct2D1.DashStyle.Dash),
                    Radius = Radius,
                }//,
				//~ new GroundLabelDecorator(Hud)
				//~ {
					//~ BackgroundBrush = Hud.Render.CreateBrush(255, color.R, color.G, color.B, 0),
					//~ TextFont = Hud.Render.CreateFont("tahoma", 12.0f, 255, 255, 255, 255, false, false, false),
				//~ }
			);
		}

		// returns rift progression per hitpoint
		private double CalculateNearMonsterProgression(long range, IWorldCoordinate origin)
        {
            var monsters = Hud.Game.AliveMonsters.Where(x => (x.SnoMonster != null) && (x.FloorCoordinate.XYDistanceTo(origin) <= range) && !((x.SummonerAcdDynamicId != 0) && (x.Rarity == ActorRarity.RareMinion)));
            return monsters.Sum(x => x.SnoMonster.RiftProgression / x.CurHealth);
        }

        public IEnumerable<ITransparent> GetTransparents()
        {
            yield return BackgroundBrushes["red"];
            yield return BackgroundBrushes["yellow"];
            yield return BackgroundBrushes["green"];
        }
	}

}
