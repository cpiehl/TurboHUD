namespace Turbo.Plugins
{
    public interface ISceneHint
    {
        string Hint { get; }
        IWorldCoordinate FloorCoordinate { get; }
    }
}