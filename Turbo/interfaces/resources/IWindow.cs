using System.Drawing;

namespace Turbo.Plugins
{
    public interface IWindow
    {
        IWorldCoordinate Center { get; }

        Size Size { get; }
        Point Offset { get; }
        float Aspect { get; }
        float HeightUiRatio { get; }

        bool IsForeground { get; }

        Rectangle GroundRectangle { get; }

        int CursorX { get; set; }
        int CursorY { get; set; }

        bool CursorInsideRect(float x, float y, float w, float h);

        IScreenCoordinate CreateScreenCoordinate(float x, float y);

        IWorldCoordinate CreateWorldCoordinate(IWorldCoordinate source);
        IWorldCoordinate CreateWorldCoordinate(float worldX, float worldY, float worldZ);

        void WorldToScreenCoordinate(float worldX, float worldY, float worldZ, out float screenX, out float screenY);
        IScreenCoordinate WorldToScreenCoordinate(float worldX, float worldY, float worldZ, bool raw = false, bool ultraPrecise = false);
        void SetScreenCoordinate(IScreenCoordinate screenCoordinate, float worldX, float worldY, float worldZ, bool raw = false, bool ultraPrecise = false);

        bool CalculateMidPointByDistance(IWorldCoordinate targetCoordinate, float targetDistance, IWorldCoordinate midPoint);
        IWorldCoordinate CalculateMidPointByRatio(IWorldCoordinate targetCoordinate, float ratio);
    }
}