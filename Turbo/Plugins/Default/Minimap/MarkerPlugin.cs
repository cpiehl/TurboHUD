namespace Turbo.Plugins.Default
{

    public class MarkerPlugin : BasePlugin, IInGameWorldPainter
	{

        public WorldDecoratorCollection Decorator { get; set; }

        public MarkerPlugin()
		{
            Enabled = true;
		}

        public override void Load(IController hud)
        {
            base.Load(hud);

            Decorator = new WorldDecoratorCollection(
                new MapShapeDecorator(Hud)
                {
                    Brush = Hud.Render.CreateBrush(192, 255, 255, 55, -1),
                    ShadowBrush = Hud.Render.CreateBrush(96, 0, 0, 0, 1),
                    Radius = 10.0f,
                    ShapePainter = new CircleShapePainter(Hud),
                },
                new MapLabelDecorator(Hud)
                {
                    LabelFont = Hud.Render.CreateFont("tahoma", 6f, 200, 255, 255, 0, false, false, 128, 0, 0, 0, true),
                    RadiusOffset = 10,
                    Up = true,
                }
                );
        }

		public void PaintWorld(WorldLayer layer)
		{
            var markers = Hud.Game.Markers;
            foreach (var marker in markers)
			{
                Decorator.ToggleDecorators<GroundLabelDecorator>(!marker.FloorCoordinate.IsOnScreen()); // do not display ground labels when the marker is on the screen
                Decorator.Paint(layer, null, marker.FloorCoordinate, marker.Name);
			}
        }

    }

}