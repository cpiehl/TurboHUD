namespace Turbo.Plugins
{
    public interface IScreenCoordinate
    {
        IWindow Window { get; }
        float X { get; set; }
        float Y { get; set; }

        void Set(float x, float y);

        float DistanceToCursor();
        IScreenCoordinate Offset(float ox, float oy);
        IWorldCoordinate ToWorldCoordinate();

        string ToString();
    }
}