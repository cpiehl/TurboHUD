using System;

namespace Turbo.Plugins.Default
{
    public class CrossShapePainter : IShapePainter
    {
        public IController Hud { get; }

        public CrossShapePainter(IController hud)
        {
            Hud = hud;
        }

        public void Paint(float x, float y, float radius, IBrush brush, IBrush shadowBrush)
        {
            if (shadowBrush != null)
            {
                shadowBrush.StrokeWidth = brush.StrokeWidth + 1;
                shadowBrush.DrawLine(x + ((float)Math.Cos(45 * Math.PI / 180.0f) * radius), y + ((float)Math.Sin(45 * Math.PI / 180.0f) * radius), x + ((float)Math.Cos(225 * Math.PI / 180.0f) * radius), y + ((float)Math.Sin(225 * Math.PI / 180.0f) * radius));
                shadowBrush.DrawLine(x + ((float)Math.Cos(135 * Math.PI / 180.0f) * radius), y + ((float)Math.Sin(135 * Math.PI / 180.0f) * radius), x + ((float)Math.Cos(315 * Math.PI / 180.0f) * radius), y + ((float)Math.Sin(315 * Math.PI / 180.0f) * radius));
            }

            brush.DrawLine(x + ((float)Math.Cos(45 * Math.PI / 180.0f) * radius), y + ((float)Math.Sin(45 * Math.PI / 180.0f) * radius), x + ((float)Math.Cos(225 * Math.PI / 180.0f) * radius), y + ((float)Math.Sin(225 * Math.PI / 180.0f) * radius));
            brush.DrawLine(x + ((float)Math.Cos(135 * Math.PI / 180.0f) * radius), y + ((float)Math.Sin(135 * Math.PI / 180.0f) * radius), x + ((float)Math.Cos(315 * Math.PI / 180.0f) * radius), y + ((float)Math.Sin(315 * Math.PI / 180.0f) * radius));
        }
    }
}