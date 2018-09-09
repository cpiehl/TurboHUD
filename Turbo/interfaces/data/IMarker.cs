namespace Turbo.Plugins
{
    public interface IMarker
    {
        ISnoActor SnoActor { get; }
        ISnoQuest SnoQuest { get; }
        uint WorldId { get; }
        IWorldCoordinate FloorCoordinate { get; }
        string Name { get; }
    }
}