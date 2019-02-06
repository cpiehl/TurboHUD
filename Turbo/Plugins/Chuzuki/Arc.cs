using SharpDX;
using SharpDX.Direct2D1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Turbo.Plugins.Default;

namespace Turbo.Plugins.Chuzuki
{
	public class Arc
	{
		public IController Hud { get; set; }
		public IBrush Brush { get; set; }
		public static float pixelsPerYardX { get; set; }
		public static float pixelsPerYardY { get; set; }

		public Arc(IController Hud, IBrush Brush)
		{
			this.Hud = Hud;
			this.Brush = Brush;
		}

		public void Paint(WorldLayer layer, IScreenCoordinate coord, float radiusYards, int thickness, double maxValue, double value, int startDeg = 0, int maxDeg = 360)
		{
			int valueDeg = (int)Math.Round(Math.Abs(maxDeg / maxValue * (maxValue - value)));
			bool ccw = maxDeg < 0;
			float outerMajor = radiusYards * pixelsPerYardX;
			float innerMajor = outerMajor - thickness;
			float outerMinor = radiusYards * pixelsPerYardY;
			float innerMinor = outerMinor - thickness;

			using (var pg = Hud.Render.CreateGeometry())
			{
				using (var gs = pg.Open())
				{
					for (int a = 0; a < valueDeg; a++)
					{
						int angle = startDeg + (ccw ? a : -a);
						var radians = angle * Math.PI / 180.0f;
						var cos = (float)Math.Cos(radians);
						var sin = (float)Math.Sin(radians);
						var innerX = innerMajor * cos;
						var innerY = innerMinor * sin;
						var outerX = outerMajor * cos;
						var outerY = outerMinor * sin;

						gs.BeginFigure(new Vector2(coord.X + innerX, coord.Y + innerY), FigureBegin.Filled);
						var vector = new Vector2(coord.X + outerX, coord.Y + outerY);
						gs.AddLine(vector);
						gs.EndFigure(FigureEnd.Closed);
					}
					gs.Close();
				}
				Brush.DrawGeometry(pg);
			}
		}
	}
}
