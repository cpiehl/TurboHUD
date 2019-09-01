namespace Turbo.Plugins
{
    public interface IPortal : IActor
    {
        ISnoArea TargetArea { get; }
        uint TargetWorldId { get; }
        bool ActorAvailable { get; }
        IWatch ActorLastAvailable { get; }
    }
}