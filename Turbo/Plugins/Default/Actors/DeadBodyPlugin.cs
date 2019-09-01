using System.Linq;

namespace Turbo.Plugins.Default
{
    public class DeadBodyPlugin : BasePlugin, IInGameWorldPainter
    {
        public WorldDecoratorCollection Decorator { get; set; }

        public DeadBodyPlugin()
        {
            Enabled = false;
        }

        public override void Load(IController hud)
        {
            base.Load(hud);

            Decorator = new WorldDecoratorCollection(
                new MapShapeDecorator(Hud)
                {
                    Brush = Hud.Render.CreateBrush(255, 230, 200, 230, 1),
                    ShadowBrush = Hud.Render.CreateBrush(96, 0, 0, 0, 1),
                    Radius = 2f,
                    ShapePainter = new RectangleShapePainter(Hud),
                }
                );
        }

        public void PaintWorld(WorldLayer layer)
        {
            var actors = Hud.Game.Actors.Where(x => !x.IsDisabled && !x.IsOperated && x.SnoActor.Kind == ActorKind.DeadBody);
            foreach (var actor in actors)
            {
                Decorator.Paint(layer, actor, actor.FloorCoordinate, null);
            }
        }
    }
}