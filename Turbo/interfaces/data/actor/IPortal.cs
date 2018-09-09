namespace Turbo.Plugins
{

    public interface IPortal: IActor
    {

        ISnoArea TargetArea { get; }
        bool ActorAvailable { get; }
        IWatch ActorLastAvailable { get; }

    }

}