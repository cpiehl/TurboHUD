using System.Collections.Generic;
using SharpDX.Direct2D1;

namespace Turbo.Plugins
{
    public interface IRenderController
    {
        // Setting this to false disables HUD's rendering. IBeforeRenderHandler.BeforeRender implementations are called regardless of rendering is enabled.
        bool IsRenderEnabled { get; set; }

        bool UiHidden { get; }
        IBrush CreateBrush(int a, int r, int g, int b, float strokeWidth, DashStyle dash = DashStyle.Solid, CapStyle startCap = CapStyle.Flat, CapStyle endCap = CapStyle.Flat);
        IFont CreateFont(string fontFamily, float size, int a, int r, int g, int b, bool bold, bool italic, bool standardShadow);
        IFont CreateFont(string fontFamily, float size, int a, int r, int g, int b, bool bold, bool italic, int shadowA, int shadowR, int shadowG, int shadowB, bool shadowIsHeavy);

        IUiElement GetUiElement(string path);
        IUiElement RegisterUiElement(string path, IUiElement collectOnlyWhenThisIsVisible, IUiElement collectOnlyWhenThisIsInvisible, float inflateXby = 0, float inflateYby = 0);

        IUiElement InGameBottomHudUiElement { get; }
        IUiElement MonsterHpBarUiElement { get; }
        IUiElement GetPlayerSkillUiElement(ActionKey key);
        IUiElement MinimapUiElement { get; }
        IUiElement NephalemRiftBarUiElement { get; }
        IUiElement GreaterRiftBarUiElement { get; }
        IUiElement ChallengeRiftBarUiElement { get; }
        IUiElement ChallengeRiftAheadPanelUiElement { get; }

        IUiElement ParagonLevelUpSplashTextUiElement { get; }

        IUiElement BuffBarExtendedBuffsUiElement { get; }
        IEnumerable<IUiElement> BuffBarUiElements { get; }

        IUiElement WorldMapUiElement { get; }
        IUiElement ActMapUiElement { get; }

        PathGeometry CreateGeometry();

        void GetMinimapCoordinates(float x, float y, out float mapX, out float mapY);
        float MinimapScale { get; }

        void SetHint(string text, string specialLocation = null);

        void TurnOnAliasing();
        // ALWAYS turn off aliasing after turning it on!
        void TurnOffAliasing();

        void CaptureScreenToFile(string subFolder, string fileName);
    }
}