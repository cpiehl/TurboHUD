namespace Turbo.Plugins
{
    public interface IWorldCoordinate
    {
        IWindow Window { get; }
        float X { get; }
        float Y { get; }
        float Z { get; }

        bool Equals(IWorldCoordinate otherWorldCoordinate);
        bool IsOnScreen(double r = 1);
        bool IsValid { get; }
        void Add(IWorldCoordinate otherCoordinate);
        IWorldCoordinate Offset(float x, float y, float z);
        void Set(IWorldCoordinate otherCoordinate);
        void Set(float x, float y, float z);
        IScreenCoordinate ToScreenCoordinate(bool raw = false, bool precise = false);
        void SetScreenCoordinate(IScreenCoordinate sc, bool raw = false, bool precise = false);
        float XYDistanceTo(IWorldCoordinate otherCoordinate);
        float XYDistanceTo(float x, float y);
        float XYZDistanceTo(IWorldCoordinate otherWorldCoordinate);
        float XYZDistanceTo(float x, float y, float z);
        float ZDiffTo(IWorldCoordinate wc);

        string ToString();
        string ToStringCompact();
        string ToStringCompactPrecise();
    }
}