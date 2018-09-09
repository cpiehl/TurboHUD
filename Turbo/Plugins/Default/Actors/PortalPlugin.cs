namespace Turbo.Plugins.Default
{

    public class PortalPlugin : BasePlugin, IInGameWorldPainter
	{

        public WorldDecoratorCollection Decorator { get; set; }

        public PortalPlugin()
		{
            Enabled = true;
		}

        public override void Load(IController hud)
        {
            base.Load(hud);

            Decorator = new WorldDecoratorCollection(
                new MapShapeDecorator(Hud)
                {
                    Brush = Hud.Render.CreateBrush(200, 255, 255, 0, 2),
                    ShadowBrush = Hud.Render.CreateBrush(96, 0, 0, 0, 1),
                    Radius = 6.0f,
                    ShapePainter = new DoorShapePainter(Hud),
                },
                new MapLabelDecorator(Hud)
                {
                    LabelFont = Hud.Render.CreateFont("tahoma", 6f, 200, 255, 255, 0, false, false, 128, 0, 0, 0, true),
                    RadiusOffset = 6.0f,
                }
                );
        }

		public void PaintWorld(WorldLayer layer)
		{
            var portals = Hud.Game.Portals;
            foreach (var portal in portals)
			{
                Decorator.Paint(layer, null, portal.FloorCoordinate, portal.TargetArea.NameLocalized ?? "unknown portal");
			}
        }

    }

}