using System.Collections.Generic;

namespace Turbo.Plugins.Default
{

    // this is not a plugin, just a helper class to display textures on the minimap
    public class MapTextureDecorator : AbstractMapDecoratorWithRadius, IWorldDecoratorWithRadius
    {

        public ITexture Texture { get; set; }
        public ISnoItem SnoItem { get; set; }

        public MapTextureDecorator(IController hud)
            : base(hud)
        {
        }

        /// <summary>
        /// Paints the decorator.
        /// </summary>
        /// <param name="coord">The coordinate of the texture.</param>
        /// <param name="text">Unused.</param>
        public void Paint(IActor actor, IWorldCoordinate coord, string text)
        {
            if (!Enabled) return;

            if (SnoItem != null) Texture = Hud.Texture.GetItemTexture(SnoItem);

            if (Texture == null) return;
            if (Radius <= 0) return;

            float mapX, mapY, radius;
            CalculateCoordinateAndRadius(coord, out mapX, out mapY, out radius);

            var width = Texture.Width * radius;
            var height = Texture.Height * radius;

            Texture.Draw(mapX - width / 2, mapY - height / 2, width, height);
        }

        public IEnumerable<ITransparent> GetTransparents()
        {
            yield return Texture;
        }

    }

}