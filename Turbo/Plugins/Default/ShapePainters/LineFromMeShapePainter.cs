namespace Turbo.Plugins.Default
{
    public class LineFromMeShapePainter : IShapePainter
    {
        public IController Hud { get; }

        public LineFromMeShapePainter(IController hud)
        {
            Hud = hud;
        }

        public void Paint(float x, float y, float radius, IBrush brush, IBrush shadowBrush)
        {
            Hud.Render.GetMinimapCoordinates(Hud.Game.Me.FloorCoordinate.X, Hud.Game.Me.FloorCoordinate.Y, out var meOnMapX, out var meOnMapY);

            // looks best with triangle-capped ('arrow') brushes:
            // Hud.Render.CreateBrush(255, 255, 255, 255, 3, SharpDX.Direct2D1.DashStyle.Dash, SharpDX.Direct2D1.CapStyle.Flat, SharpDX.Direct2D1.CapStyle.Triangle);

            brush.DrawLine(x, y, meOnMapX, meOnMapY);
        }
    }
}