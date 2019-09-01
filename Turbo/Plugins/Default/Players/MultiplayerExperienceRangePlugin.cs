namespace Turbo.Plugins.Default
{
    // range for "Strength in Numbers" buff in multiplayer games
    public class MultiplayerExperienceRangePlugin : BasePlugin, IInGameWorldPainter
    {
        public WorldDecoratorCollection Decorator { get; set; }

        public MultiplayerExperienceRangePlugin()
        {
            Enabled = false;
        }

        public override void Load(IController hud)
        {
            base.Load(hud);

            Decorator = new WorldDecoratorCollection(
                new MapShapeDecorator(Hud)
                {
                    Brush = Hud.Render.CreateBrush(7, 255, 255, 255, 0),
                    ShapePainter = new CircleShapePainter(Hud),
                    Radius = 200,
                }
            );
        }

        public void PaintWorld(WorldLayer layer)
        {
            if (Hud.Game.IsInTown)
                return;
            if (Hud.Render.UiHidden)
                return;
            if (Hud.Game.NumberOfPlayersInGame <= 1)
                return;

            Decorator.Paint(layer, null, Hud.Game.Me.FloorCoordinate, null);
        }
    }
}