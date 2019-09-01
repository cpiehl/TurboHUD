namespace Turbo.Plugins
{
    public interface IClickableActor : IActor
    {
        IWatch FirstClicked { get; }
        IWatch LastClicked { get; }

        bool? ShouldBeClicked { get; set; }

        void RegisterClick();
    }
}