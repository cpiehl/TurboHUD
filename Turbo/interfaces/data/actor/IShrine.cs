namespace Turbo.Plugins
{
    public interface IShrine: IClickableActor
    {
        ShrineType Type { get; }
    }
}