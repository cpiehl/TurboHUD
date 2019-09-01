namespace Turbo.Plugins.Default
{
    public interface IWorldShapePainter
    {
        void Paint(float x, float y, float z, float radius, float rotation, IBrush brush, IBrush shadowBrush);
    }
}