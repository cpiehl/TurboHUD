using System.Collections.Generic;

namespace Turbo.Plugins.Default
{
    public delegate string StringGeneratorFunc();

    public enum HorizontalAlign { Left, Center, Right }

    // this is not a plugin, just a helper class to display fixed sized labels on the screen
    public class TopLabelDecorator : ITransparentCollection
    {
        public bool Enabled { get; set; }
        public IController Hud { get; }

        public IFont TextFont { get; set; }
        public IFont ExpandedHintFont { get; set; }
        public float ExpandedHintWidthMultiplier { get; set; } = 1;

        // Option #1: Use brushes for background and border
        public IBrush BackgroundBrush { get; set; }
        public IBrush BorderBrush { get; set; }

        // Option #2: Use textures for background
        public ITexture BackgroundTexture1 { get; set; }
        public ITexture BackgroundTexture2 { get; set; }
        public float BackgroundTextureOpacity1 { get; set; } = 1.0f;
        public float BackgroundTextureOpacity2 { get; set; } = 1.0f;

        public StringGeneratorFunc TextFunc { get; set; }
        public StringGeneratorFunc HintFunc { get; set; }

        public bool HideBackgroundWhenTextIsEmpty { get; set; } = false;

        public List<TopLabelDecorator> ExpandUpLabels { get; set; }
        public List<TopLabelDecorator> ExpandDownLabels { get; set; }
        public List<TopLabelDecorator> ExpandRightLabels { get; set; }
        public List<TopLabelDecorator> ExpandLeftLabels { get; set; }

        public TopLabelDecorator(IController hud)
        {
            Enabled = true;
            Hud = hud;
        }

        public void Paint(float x, float y, float w, float h, HorizontalAlign align)
        {
            if (!Enabled)
                return;
            if (TextFont == null)
                return;

            var text = TextFunc?.Invoke();
            var hint = HintFunc?.Invoke();

            if (string.IsNullOrEmpty(text) && HideBackgroundWhenTextIsEmpty)
                return;

            if (Hud.Window.CursorInsideRect(x, y, w, h))
            {
                var expanded = false;
                if (ExpandUpLabels?.Count > 0)
                {
                    var ly = y - h;
                    foreach (var label in ExpandUpLabels)
                    {
                        label.Paint(x, ly, w, h, align);
                        label.PaintExpandedHint(x + w, ly, w * label.ExpandedHintWidthMultiplier, h, HorizontalAlign.Center);
                        ly -= h;
                        expanded = true;
                    }

                    PaintExpandedHint(x + w, y, w * 3, h, HorizontalAlign.Center);
                }

                if (ExpandDownLabels?.Count > 0)
                {
                    var ly = y + h;
                    foreach (var label in ExpandDownLabels)
                    {
                        label.Paint(x, ly, w, h, align);
                        label.PaintExpandedHint(x + w, ly, w * label.ExpandedHintWidthMultiplier, h, HorizontalAlign.Center);
                        ly += h;
                        expanded = true;
                    }

                    PaintExpandedHint(x + w, y, w * 3, h, HorizontalAlign.Center);
                }

                if (ExpandRightLabels?.Count > 0)
                {
                    var lx = x + w;
                    foreach (var label in ExpandRightLabels)
                    {
                        label.Paint(lx, y, w, h, align);
                        lx += h;
                        expanded = true;
                    }
                }

                if (ExpandLeftLabels?.Count > 0)
                {
                    var lx = x - w;
                    foreach (var label in ExpandLeftLabels)
                    {
                        label.Paint(lx, y, w, h, align);
                        lx -= h;
                        expanded = true;
                    }
                }

                if (!expanded)
                {
                    if (!string.IsNullOrEmpty(hint))
                    {
                        Hud.Render.SetHint(hint);
                    }
                }
            }

            BackgroundTexture1?.Draw(x, y, w, h, BackgroundTextureOpacity1);

            BackgroundTexture2?.Draw(x, y, w, h, BackgroundTextureOpacity2);

            BackgroundBrush?.DrawRectangle(x, y, w, h);

            if (!string.IsNullOrEmpty(text))
            {
                var layout = TextFont.GetTextLayout(text);
                switch (align)
                {
                    case HorizontalAlign.Left:
                        TextFont.DrawText(layout, x, y + ((h - layout.Metrics.Height) / 2));
                        break;
                    case HorizontalAlign.Center:
                        TextFont.DrawText(layout, x + ((w - layout.Metrics.Width) / 2), y + ((h - layout.Metrics.Height) / 2));
                        break;
                    case HorizontalAlign.Right:
                        TextFont.DrawText(layout, x + w - layout.Metrics.Width, y + ((h - layout.Metrics.Height) / 2));
                        break;
                }
            }

            BorderBrush?.DrawRectangle(x, y, w, h);
        }

        public void PaintExpandedHint(float x, float y, float w, float h, HorizontalAlign align)
        {
            if (!Enabled)
                return;
            if (ExpandedHintFont == null)
                return;

            var hint = HintFunc?.Invoke();
            if (string.IsNullOrEmpty(hint))
                return;

            BackgroundTexture1?.Draw(x, y, w, h, BackgroundTextureOpacity1);

            BackgroundTexture2?.Draw(x, y, w, h, BackgroundTextureOpacity2);

            BackgroundBrush?.DrawRectangle(x, y, w, h);

            var layout = ExpandedHintFont.GetTextLayout(hint);
            switch (align)
            {
                case HorizontalAlign.Left:
                    ExpandedHintFont.DrawText(layout, x, y + ((h - layout.Metrics.Height) / 2));
                    break;
                case HorizontalAlign.Center:
                    ExpandedHintFont.DrawText(layout, x + ((w - layout.Metrics.Width) / 2), y + ((h - layout.Metrics.Height) / 2));
                    break;
                case HorizontalAlign.Right:
                    ExpandedHintFont.DrawText(layout, x + w - layout.Metrics.Width, y + ((h - layout.Metrics.Height) / 2));
                    break;
            }

            BorderBrush?.DrawRectangle(x, y, w, h);
        }

        public IEnumerable<ITransparent> GetTransparents()
        {
            yield return TextFont;
            yield return ExpandedHintFont;
            yield return BackgroundTexture1;
            yield return BackgroundTexture2;
        }
    }
}