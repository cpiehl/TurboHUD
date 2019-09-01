using System.Linq;

namespace Turbo.Plugins.Default
{
    public class CursedEventPlugin : BasePlugin, IInGameWorldPainter
    {
        public WorldDecoratorCollection Decorator { get; set; }

        public CursedEventPlugin()
        {
            Enabled = true;
        }

        public override void Load(IController hud)
        {
            base.Load(hud);

            Decorator = new WorldDecoratorCollection(
                new MapShapeDecorator(Hud)
                {
                    Brush = Hud.Render.CreateBrush(255, 255, 64, 64, 2),
                    ShadowBrush = Hud.Render.CreateBrush(96, 0, 0, 0, 1),
                    Radius = 8.0f,
                    ShapePainter = new CircleShapePainter(Hud),
                },
                new GroundLabelDecorator(Hud)
                {
                    BackgroundBrush = Hud.Render.CreateBrush(255, 255, 64, 64, 0),
                    TextFont = Hud.Render.CreateFont("tahoma", 6.5f, 255, 255, 255, 255, false, false, false),
                }
                );
        }

        public void PaintWorld(WorldLayer layer)
        {
            var actors = Hud.Game.Actors.Where(x => x.SnoActor.Kind == ActorKind.CursedEvent && !x.IsDisabled && !x.IsOperated);
            foreach (var actor in actors)
            {
                Decorator.ToggleDecorators<GroundLabelDecorator>(!actor.IsOnScreen);

                Decorator.Paint(layer, actor, actor.FloorCoordinate, actor.SnoActor.NameLocalized);
            }
        }
    }
}