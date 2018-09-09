namespace Turbo.Plugins.Default
{

    public class HorizontalTopLabelList : List
    {

        public HorizontalTopLabelList(IController hud)
            : base(hud)
        {
        }

        protected override void Paint(float x, float y, float labelWidth, float labelHeight)
        {
            TopLabelDecorator selectedLabel = null;
            var selectedX = 0f;
            foreach (var label in LabelDecorators)
            {
                if (!Hud.Window.CursorInsideRect(x, y, labelWidth, labelHeight))
                {
                    label.Paint(x, y, labelWidth, labelHeight, HorizontalAlign.Center);
                }
                else
                {
                    selectedLabel = label;
                    selectedX = x;
                }
                x += labelWidth + SpacingAdjustmentInPixels;
            }

            if (selectedLabel != null)
            {
                selectedLabel.Paint(selectedX, y, labelWidth, labelHeight, HorizontalAlign.Center);
            }
        }

    }

}