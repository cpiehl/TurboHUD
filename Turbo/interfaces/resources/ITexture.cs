namespace Turbo.Plugins
{
    public interface ITexture : ITransparent
    {
        float Height { get; }
        float Width { get; }

        void Draw(SharpDX.RectangleF rectangle, float opacity = 1.0f);
        void Draw(float x, float y, float w, float h, float opacityMultiplier = 1.0f);
    }
}