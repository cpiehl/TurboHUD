namespace Turbo.Plugins.Default
{
    public class SceneHintPlugin : BasePlugin, IInGameWorldPainter
    {
        public WorldDecoratorCollection Decorator { get; set; }

        public SceneHintPlugin()
        {
            Enabled = true;
        }

        public override void Load(IController hud)
        {
            base.Load(hud);

            Decorator = new WorldDecoratorCollection(
                new MapLabelDecorator(Hud)
                {
                    LabelFont = Hud.Render.CreateFont("tahoma", 6f, 200, 255, 200, 0, false, false, 128, 0, 0, 0, true),
                    RadiusOffset = 0,
                }
                );
        }

        public void PaintWorld(WorldLayer layer)
        {
            var sceneHints = Hud.Game.SceneHints;
            foreach (var sceneHint in sceneHints)
            {
                Decorator.ToggleDecorators<GroundLabelDecorator>(!sceneHint.FloorCoordinate.IsOnScreen()); // do not display ground labels when the actor is on the screen
                Decorator.Paint(layer, null, sceneHint.FloorCoordinate, sceneHint.Hint);
            }
        }
    }
}