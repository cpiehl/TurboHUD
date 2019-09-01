using System.Linq;

namespace Turbo.Plugins.Default
{
    public class OculusPlugin : BasePlugin, IInGameWorldPainter
    {
        public WorldDecoratorCollection Decorator { get; set; }

        public OculusPlugin()
        {
            Enabled = true;
        }

        public override void Load(IController hud)
        {
            base.Load(hud);

            Decorator = new WorldDecoratorCollection(
                new GroundCircleDecorator(Hud)
                {
                    Brush = Hud.Render.CreateBrush(255, 128, 255, 0, -2),
                    Radius = 10.0f,
                },
                new GroundLabelDecorator(Hud)
                {
                    CountDownFrom = 7,
                    TextFont = Hud.Render.CreateFont("tahoma", 11, 255, 96, 255, 96, true, false, 128, 0, 0, 0, true),
                },
                new GroundTimerDecorator(Hud)
                {
                    CountDownFrom = 7,
                    BackgroundBrushEmpty = Hud.Render.CreateBrush(128, 0, 0, 0, 0),
                    BackgroundBrushFill = Hud.Render.CreateBrush(200, 0, 192, 0, 0),
                    Radius = 30,
                }
                );
        }

        public void PaintWorld(WorldLayer layer)
        {
            if (Hud.Game.IsInTown)
                return;

            var actors = Hud.Game.Actors.Where(x => x.SnoActor.Sno == ActorSnoEnum._generic_proxy && x.GetAttributeValueAsInt(Hud.Sno.Attributes.Power_Buff_1_Visual_Effect_None, Hud.Sno.SnoPowers.OculusRing.Sno) == 1);
            foreach (var actor in actors)
            {
                Decorator.Paint(layer, actor, actor.FloorCoordinate, null);
            }
        }
    }
}