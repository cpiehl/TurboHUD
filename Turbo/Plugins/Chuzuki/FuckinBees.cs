using Turbo.Plugins.Default;
using SharpDX;
using SharpDX.Direct2D1;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Turbo.Plugins.Chuzuki
{
    public class FuckinBees : BasePlugin, IInGameWorldPainter
    {
		public WorldDecoratorCollection BeeDecorator { get; set; }
        public float Radius { get; set; }

        public FuckinBees()
        {
            Enabled = true;
			Radius = 2;
        }

        public override void Load(IController hud)
        {
            base.Load(hud);
			InitDecorators();
        }

        public void PaintWorld(WorldLayer layer)
        {
			var gcd = (GroundCircleDecorator)BeeDecorator.Decorators.FirstOrDefault();
			gcd.Radius -= 0.1f;
			if (gcd.Radius <= 0.1f)
				gcd.Radius = Radius;
			foreach (var bee in Hud.Game.Actors.Where(a => a.SnoActor.Sno == 5212))
			{
				BeeDecorator.Paint(layer, bee, bee.FloorCoordinate, "O");
			}
		}

		private void InitDecorators()
        {
			BeeDecorator = GetDecorator(new Color(255, 255, 255, 64), Radius);
		}

		private WorldDecoratorCollection GetDecorator(Color color1, float radius)
		{
			int stroke = (radius == -1) ? 0 : 2;
			return new WorldDecoratorCollection
			(
				new GroundCircleDecorator(Hud)
                {
                    Brush = Hud.Render.CreateBrush(color1.A, color1.R, color1.G, color1.B, stroke, SharpDX.Direct2D1.DashStyle.Dash),
                    Radius = radius,
                }
			);
		}
	}
}
