using System;

namespace Turbo.Plugins.Default
{
    public class RotatingTriangleShapePainter : IShapePainter
    {
        public IController Hud { get; }

        public RotatingTriangleShapePainter(IController hud)
        {
            Hud = hud;
        }

        public void Paint(float x, float y, float radius, IBrush brush, IBrush shadowBrush)
        {
            var tickRotationAngle = Hud.Game.CurrentRealTimeMilliseconds / 2 % 360;
            if (shadowBrush != null)
            {
                shadowBrush.StrokeWidth = brush.StrokeWidth + 1;
                shadowBrush.DrawLine(x + (radius * (float)Math.Cos((0.0f + tickRotationAngle) * Math.PI / 180.0f)), y + (radius * (float)Math.Sin((0.0f + tickRotationAngle) * Math.PI / 180.0f)), x + (radius * (float)Math.Cos((120.0f + tickRotationAngle) * Math.PI / 180.0f)), y + (radius * (float)Math.Sin((120.0f + tickRotationAngle) * Math.PI / 180.0f)));
                shadowBrush.DrawLine(x + (radius * (float)Math.Cos((120.0f + tickRotationAngle) * Math.PI / 180.0f)), y + (radius * (float)Math.Sin((120.0f + tickRotationAngle) * Math.PI / 180.0f)), x + (radius * (float)Math.Cos((240.0f + tickRotationAngle) * Math.PI / 180.0f)), y + (radius * (float)Math.Sin((240.0f + tickRotationAngle) * Math.PI / 180.0f)));
                shadowBrush.DrawLine(x + (radius * (float)Math.Cos((240.0f + tickRotationAngle) * Math.PI / 180.0f)), y + (radius * (float)Math.Sin((240.0f + tickRotationAngle) * Math.PI / 180.0f)), x + (radius * (float)Math.Cos((360.0f + tickRotationAngle) * Math.PI / 180.0f)), y + (radius * (float)Math.Sin((360.0f + tickRotationAngle) * Math.PI / 180.0f)));
            }

            brush.DrawLine(x + (radius * (float)Math.Cos((0.0f + tickRotationAngle) * Math.PI / 180.0f)), y + (radius * (float)Math.Sin((0.0f + tickRotationAngle) * Math.PI / 180.0f)), x + (radius * (float)Math.Cos((120.0f + tickRotationAngle) * Math.PI / 180.0f)), y + (radius * (float)Math.Sin((120.0f + tickRotationAngle) * Math.PI / 180.0f)));
            brush.DrawLine(x + (radius * (float)Math.Cos((120.0f + tickRotationAngle) * Math.PI / 180.0f)), y + (radius * (float)Math.Sin((120.0f + tickRotationAngle) * Math.PI / 180.0f)), x + (radius * (float)Math.Cos((240.0f + tickRotationAngle) * Math.PI / 180.0f)), y + (radius * (float)Math.Sin((240.0f + tickRotationAngle) * Math.PI / 180.0f)));
            brush.DrawLine(x + (radius * (float)Math.Cos((240.0f + tickRotationAngle) * Math.PI / 180.0f)), y + (radius * (float)Math.Sin((240.0f + tickRotationAngle) * Math.PI / 180.0f)), x + (radius * (float)Math.Cos((360.0f + tickRotationAngle) * Math.PI / 180.0f)), y + (radius * (float)Math.Sin((360.0f + tickRotationAngle) * Math.PI / 180.0f)));
        }
    }
}