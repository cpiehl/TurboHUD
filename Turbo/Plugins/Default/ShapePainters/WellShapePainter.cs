namespace Turbo.Plugins.Default
{
    public class WellShapePainter : IShapePainter
    {
        public IController Hud { get; }

        public WellShapePainter(IController hud)
        {
            Hud = hud;
        }

        public void Paint(float x, float y, float radius, IBrush brush, IBrush shadowBrush)
        {
            if (shadowBrush != null)
            {
                shadowBrush.StrokeWidth = brush.StrokeWidth + 1;
                shadowBrush.DrawRectangle(x - radius, y - radius, radius * 2.0f, radius * 2.0f);
                shadowBrush.DrawLine(x - radius, y, x + radius, y);
                shadowBrush.DrawLine(x, y - radius, x, y + radius);
            }

            brush.DrawRectangle(x - radius, y - radius, radius * 2.0f, radius * 2.0f);
            brush.DrawLine(x - radius, y, x + radius, y);
            brush.DrawLine(x, y - radius, x, y + radius);
        }
    }
}