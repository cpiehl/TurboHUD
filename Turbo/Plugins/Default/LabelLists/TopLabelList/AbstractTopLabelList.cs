using System;
using System.Collections.Generic;

namespace Turbo.Plugins.Default
{
    public abstract class AbstractTopLabelList
    {
        public IController Hud { get; }
        public List<TopLabelDecorator> LabelDecorators { get; set; } = new List<TopLabelDecorator>();
        public float SpacingAdjustmentInPixels { get; set; } = -1;

        public Func<float> LeftFunc { get; set; }
        public Func<float> TopFunc { get; set; }
        public Func<float> WidthFunc { get; set; }
        public Func<float> HeightFunc { get; set; }

        protected AbstractTopLabelList(IController hud)
        {
            Hud = hud;
        }

        public void Paint()
        {
            if (LeftFunc == null || TopFunc == null)
                return;
            if (WidthFunc == null || HeightFunc == null)
                return;

            var left = LeftFunc();
            var top = TopFunc();
            var labelWidth = WidthFunc();
            var labelHeight = HeightFunc();
            Paint(left, top, labelWidth, labelHeight);
        }

        protected abstract void Paint(float x, float y, float labelWidth, float labelHeight);
    }
}