using System;
using System.Collections.Generic;
using SharpDX;
using SharpDX.Direct2D1;

namespace Turbo.Plugins.Default
{
    // this is not a plugin, just a helper class to display timers on the ground
    public class GroundTimerDecorator : IWorldDecorator
    {
        public bool Enabled { get; set; }
        public WorldLayer Layer { get; } = WorldLayer.Ground;
        public IController Hud { get; }

        public IBrush BackgroundBrushEmpty { get; set; }
        public IBrush BackgroundBrushFill { get; set; }

        public float Radius { get; set; }
        public float CountDownFrom { get; set; }

        public GroundTimerDecorator(IController hud)
        {
            Enabled = true;
            Hud = hud;
        }

        public void Paint(IActor actor, IWorldCoordinate coord, string text)
        {
            if (!Enabled)
                return;
            if (actor == null)
                return;

            var rad = Radius / 1200.0f * Hud.Window.Size.Height;
            var max = CountDownFrom;
            var elapsed = (Hud.Game.CurrentGameTick - actor.CreatedAtInGameTick) / 60.0f;
            if (elapsed < 0)
                return;
            if (elapsed > max)
                elapsed = max;

            var screenCoord = coord.ToScreenCoordinate();
            var startAngle = Convert.ToInt32(360 / max * elapsed) - 90;
            var endAngle = 360 - 90;

            using (var pg = Hud.Render.CreateGeometry())
            {
                using (var gs = pg.Open())
                {
                    gs.BeginFigure(new Vector2(screenCoord.X, screenCoord.Y), FigureBegin.Filled);
                    for (var angle = startAngle; angle <= endAngle; angle++)
                    {
                        var mx = rad * (float)Math.Cos(angle * Math.PI / 180.0f);
                        var my = rad * (float)Math.Sin(angle * Math.PI / 180.0f);
                        var vector = new Vector2(screenCoord.X + mx, screenCoord.Y + my);
                        gs.AddLine(vector);
                    }

                    gs.EndFigure(FigureEnd.Closed);
                    gs.Close();
                }

                BackgroundBrushFill.DrawGeometry(pg);
            }

            using (var pg = Hud.Render.CreateGeometry())
            {
                using (var gs = pg.Open())
                {
                    gs.BeginFigure(new Vector2(screenCoord.X, screenCoord.Y), FigureBegin.Filled);
                    for (var angle = endAngle; angle <= startAngle + 360; angle++)
                    {
                        var mx = rad * (float)Math.Cos(angle * Math.PI / 180.0f);
                        var my = rad * (float)Math.Sin(angle * Math.PI / 180.0f);
                        var vector = new Vector2(screenCoord.X + mx, screenCoord.Y + my);
                        gs.AddLine(vector);
                    }

                    gs.EndFigure(FigureEnd.Closed);
                    gs.Close();
                }

                BackgroundBrushEmpty.DrawGeometry(pg);
            }
        }

        public IEnumerable<ITransparent> GetTransparents()
        {
            yield return BackgroundBrushEmpty;
            yield return BackgroundBrushFill;
        }
    }
}