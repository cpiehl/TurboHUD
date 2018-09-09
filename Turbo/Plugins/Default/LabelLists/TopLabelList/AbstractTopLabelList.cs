using System;
using System.Collections.Generic;

namespace Turbo.Plugins.Default
{

    public abstract class List
    {

        public IController Hud { get; private set; }
        public List<TopLabelDecorator> LabelDecorators { get; private set; }
        public float SpacingAdjustmentInPixels { get; set; }

        public Func<float> LeftFunc { get; set; }
        public Func<float> TopFunc { get; set; }
        public Func<float> WidthFunc { get; set; }
        public Func<float> HeightFunc { get; set; }

        public List(IController hud)
        {
            Hud = hud;
            LabelDecorators = new List<TopLabelDecorator>();
            SpacingAdjustmentInPixels = -1;
        }

        public void Paint()
        {
            if (LeftFunc == null || TopFunc == null) return;
            if (WidthFunc == null || HeightFunc == null) return;

            var left = LeftFunc();
            var top = TopFunc();
            var labelWidth = WidthFunc();
            var labelHeight = HeightFunc();
            Paint(left, top, labelWidth, labelHeight);
        }

        protected abstract void Paint(float x, float y, float labelWidth, float labelHeight);

    }

}