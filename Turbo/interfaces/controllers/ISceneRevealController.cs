namespace Turbo.Plugins
{
    public interface ISceneRevealController
    {
        bool MinimapEnabled { get; set; }
        bool MapEnabled { get; set; }
        float MapOpacity { get; set; }
        float MinimapOpacity { get; set; }
        bool MinimapClip { get; set; }
        System.Drawing.SolidBrush BrushKnown { get; set; }
        System.Drawing.Brush BrushUnknown { get; set; }

        bool DisplaySceneBorder { get; set; }
        System.Drawing.Pen SceneBorderPen { get; set; }
    }
}