using System.Linq;

namespace Turbo.Plugins.Default
{
    public class GlobePlugin : BasePlugin, IInGameWorldPainter
    {
        public WorldDecoratorCollection PowerGlobeDecorator { get; set; }
        public WorldDecoratorCollection RiftOrbDecorator { get; set; }

        public GlobePlugin()
        {
            Enabled = true;
        }

        public override void Load(IController hud)
        {
            base.Load(hud);

            PowerGlobeDecorator = new WorldDecoratorCollection(
                new MapShapeDecorator(Hud)
                {
                    Brush = Hud.Render.CreateBrush(255, 240, 240, 120, 0),
                    ShadowBrush = Hud.Render.CreateBrush(96, 0, 0, 0, 1),
                    Radius = 6.0f,
                    ShapePainter = new CircleShapePainter(Hud),
                },
                new GroundLabelDecorator(Hud)
                {
                    BackgroundBrush = Hud.Render.CreateBrush(255, 240, 240, 120, 0),
                    TextFont = Hud.Render.CreateFont("tahoma", 6.5f, 255, 0, 0, 0, false, false, false),
                }
                );

            RiftOrbDecorator = new WorldDecoratorCollection(
                new MapShapeDecorator(Hud)
                {
                    Brush = Hud.Render.CreateBrush(255, 240, 120, 240, 0),
                    ShadowBrush = Hud.Render.CreateBrush(96, 0, 0, 0, 1),
                    Radius = 6.0f,
                    ShapePainter = new CircleShapePainter(Hud),
                },
                new GroundLabelDecorator(Hud)
                {
                    BackgroundBrush = Hud.Render.CreateBrush(255, 240, 120, 240, 0),
                    TextFont = Hud.Render.CreateFont("tahoma", 6.5f, 255, 0, 0, 0, false, false, false),
                }
                );
        }

        public void PaintWorld(WorldLayer layer)
        {
            var actors = Hud.Game.Actors.Where(x => x.SnoActor.Kind == ActorKind.PowerGlobe);
            foreach (var actor in actors)
            {
                PowerGlobeDecorator.ToggleDecorators<GroundLabelDecorator>(!actor.IsOnScreen); // do not display ground labels when the actor is on the screen
                PowerGlobeDecorator.Paint(layer, actor, actor.FloorCoordinate, "power globe");
            }

            actors = Hud.Game.Actors.Where(x => x.SnoActor.Kind == ActorKind.RiftOrb);
            foreach (var actor in actors)
            {
                RiftOrbDecorator.ToggleDecorators<GroundLabelDecorator>(!actor.IsOnScreen); // do not display ground labels when the actor is on the screen
                RiftOrbDecorator.Paint(layer, actor, actor.FloorCoordinate, "rift orb");
            }
        }
    }
}