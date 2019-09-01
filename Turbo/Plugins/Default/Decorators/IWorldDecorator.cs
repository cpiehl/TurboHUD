namespace Turbo.Plugins.Default
{
    public interface IWorldDecorator : ITransparentCollection
    {
        bool Enabled { get; set; }
        WorldLayer Layer { get; }
        void Paint(IActor actor, IWorldCoordinate coord, string text);
    }
}