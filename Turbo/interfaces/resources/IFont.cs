using SharpDX.DirectWrite;

namespace Turbo.Plugins
{

    public interface IFont: ITransparent
    {
        int MaxHeight { get; set; }
        int MaxWidth { get; set; }
        bool WordWrap { get; set; }
        bool HeavyShadow { get; set; }

        void DrawText(string text, IScreenCoordinate coordinate, bool enableLayoutCache = true);
        void DrawText(string text, float x, float y, bool enableLayoutCache = true);

        // Cached by the overlay engine, and disposed automatically. Best for rarely changing texts.
        TextLayout GetTextLayout(string text);

        // Not cached or disposed by the overlay engine. The caller has to dispose to prevent memory leaks! Best for regularly changing texts.
        // If the layout is not disposed, then the resulted memory leak will crash TurboHUD in a few seconds/minutes.
        TextLayout GetTextLayoutManualDispose(string text);

        void DrawText(TextLayout textLayout, float x, float y);
        void DrawText(TextLayout textLayout, IScreenCoordinate coordinate);

        void SetMaxSize(int maxWidth, int maxHeight);
        void SetShadowBrush(int a, int r, int g, int b, bool heavy);
    }

}