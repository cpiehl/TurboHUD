using System.Collections.Generic;

namespace Turbo.Plugins.Default
{
    // this is not a plugin, just a helper class to display shapes on the minimap
    public class MapLabelDecorator : IWorldDecorator
    {
        public bool Enabled { get; set; }
        public WorldLayer Layer { get; } = WorldLayer.Map;
        public IController Hud { get; }

        public IFont LabelFont { get; set; }
        public bool Up { get; set; }
        public float RadiusOffset { get; set; }

        public MapLabelDecorator(IController hud)
        {
            Enabled = true;
            Hud = hud;
        }

        public void Paint(IActor actor, IWorldCoordinate coord, string text)
        {
            if (!Enabled)
                return;
            if (LabelFont == null)
                return;
            if (string.IsNullOrEmpty(text))
                return;
            Hud.Render.GetMinimapCoordinates(coord.X, coord.Y, out var mapx, out var mapy);

            var layout = LabelFont.GetTextLayout(text);
            if (!Up)
            {
                LabelFont.DrawText(layout, mapx - (layout.Metrics.Width / 2), mapy + RadiusOffset);
            }
            else
            {
                LabelFont.DrawText(layout, mapx - (layout.Metrics.Width / 2), mapy - RadiusOffset - layout.Metrics.Height);
            }
        }

        public IEnumerable<ITransparent> GetTransparents()
        {
            yield break;
        }
    }
}