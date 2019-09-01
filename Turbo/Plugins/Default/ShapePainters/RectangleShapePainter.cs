namespace Turbo.Plugins.Default
{
    public class RectangleShapePainter : IShapePainter
    {
        public IController Hud { get; }

        public RectangleShapePainter(IController hud)
        {
            Hud = hud;
        }

        public void Paint(float x, float y, float radius, IBrush brush, IBrush shadowBrush)
        {
            if (shadowBrush != null)
            {
                shadowBrush.StrokeWidth = brush.StrokeWidth + 1;
                shadowBrush.DrawRectangle(x - radius, y - radius, radius * 2.0f, radius * 2.0f);
            }

            brush.DrawRectangle(x - radius, y - radius, radius * 2.0f, radius * 2.0f);
        }
    }
}