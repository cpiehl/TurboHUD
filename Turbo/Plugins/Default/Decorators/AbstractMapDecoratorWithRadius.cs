namespace Turbo.Plugins.Default
{
    public abstract class AbstractMapDecoratorWithRadius
    {
        public bool Enabled { get; set; }
        public WorldLayer Layer { get; } = WorldLayer.Map;
        public IController Hud { get; }

        public float Radius { get; set; } = 1.0f;
        public IRadiusTransformator RadiusTransformator { get; set; }

        protected AbstractMapDecoratorWithRadius(IController hud)
        {
            Enabled = true;
            Hud = hud;
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