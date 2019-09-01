namespace Turbo.Plugins.Default
{
    public interface IWorldDecoratorWithRadius : IWorldDecorator
    {
        float Radius { get; set; }
    }
}