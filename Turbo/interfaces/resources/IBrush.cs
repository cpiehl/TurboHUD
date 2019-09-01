namespace Turbo.Plugins
{
    public interface IBrush : ITransparent
    {
        void DrawEllipse(float x1, float y1, float r1, float r2, float strokeWidthCorrection = 0);
        void DrawLine(float x1, float y1, float x2, float y2, float strokeWidthCorrection = 0);
        void DrawLineGridFit(float x1, float y1, float x2, float y2, float strokeWidthCorrection = 0);
        void DrawLineWorld(float x1, float y1, float z1, float x2, float y2, float z2, float strokeWidthCorrection = 0);
        void DrawLineWorld(IWorldCoordinate wc1, IWorldCoordinate wc2, float strokeWidthCorrection = 0);
        void DrawRectangle(System.Drawing.RectangleF rectangle);
        void DrawRectangle(SharpDX.RectangleF rectangle);
        void DrawRectangle(float x, float y, float w, float h);
        void DrawRectangleGridFit(float x, float y, float w, float h);
        void DrawWorldEllipse(float radius, int sectionCount, IWorldCoordinate coordinate);
        void DrawWorldEllipse(float radius, int sectionCount, float x, float y, float z);
        void DrawWorldPlus(float radius, float x, float y, float z);
        void DrawGeometry(SharpDX.Direct2D1.Geometry geometry);

        float StrokeWidth { get; set; }
        SharpDX.Direct2D1.StrokeStyle StrokeStyle { get; }
        float RealStrokeWidth { get; }
    }
}