namespace Turbo.Plugins.Default
{

    public abstract class AbstractMapDecoratorWithRadius
    {

        public bool Enabled { get; set; }
        public WorldLayer Layer { get; private set; }
        public IController Hud { get; private set; }

        public float Radius { get; set; }
        public IRadiusTransformator RadiusTransformator { get; set; }

        public AbstractMapDecoratorWithRadius(IController hud)
        {
            Enabled = true;
            Layer = WorldLayer.Map;
            Hud = hud;

            Radius = 1.0f;
        }

        public void CalculateCoordinateAndRadius(IWorldCoordinate coord, out float mapX, out float mapY, out float radius)
        {
            Hud.Render.GetMinimapCoordinates(coord.X, coord.Y, out mapX, out mapY);

            radius = Radius * Hud.Render.MinimapScale;

            if (RadiusTransformator != null)
            {
                radius = RadiusTransformator.TransformRadius(radius);
            }
        }

    }

}