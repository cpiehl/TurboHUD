namespace Turbo.Plugins.Default
{
    public interface IShapePainter
    {
        void Paint(float x, float y, float radius, IBrush brush, IBrush shadowBrush);
    }
}