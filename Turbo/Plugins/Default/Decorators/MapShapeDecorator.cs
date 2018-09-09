using System.Collections.Generic;

namespace Turbo.Plugins.Default
{

    // this is not a plugin, just a helper class to display shapes on the minimap
    public class MapShapeDecorator: AbstractMapDecoratorWithRadius, IWorldDecoratorWithRadius
    {

        public IBrush Brush { get; set; }
        public IBrush ShadowBrush { get; set; }
        public IShapePainter ShapePainter { get; set; }

        public MapShapeDecorator(IController hud)
            : base(hud)
        {
        }

        public void Paint(IActor actor, IWorldCoordinate coord, string text)
        {
            if (!Enabled) return;
            if (Brush == null) return;
            if (Radius <= 0) return;
            if (ShapePainter == null) return;

            float x, y, radius;
            CalculateCoordinateAndRadius(coord, out x, out y, out radius);

            ShapePainter.Paint(x, y, radius, Brush, ShadowBrush);
        }

        public IEnumerable<ITransparent> GetTransparents()
        {
            yield return Brush;
            yield return ShadowBrush;
        }

    }

}