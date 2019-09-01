using System.Linq;

namespace Turbo.Plugins.Default
{
    public class RackPlugin : BasePlugin, IInGameWorldPainter
    {
        public WorldDecoratorCollection Decorator { get; set; }

        public RackPlugin()
        {
            Enabled = true;
        }

        public override void Load(IController hud)
        {
            base.Load(hud);

            Decorator = new WorldDecoratorCollection(
                new MapShapeDecorator(Hud)
                {
                    Brush = Hud.Render.CreateBrush(255, 64, 255, 64, 1),
                    ShadowBrush = Hud.Render.CreateBrush(96, 0, 0, 0, 1),
                    Radius = 4.0f,
                    ShapePainter = new CircleShapePainter(Hud),
                }
                );
        }

        public void PaintWorld(WorldLayer layer)
        {
            if (Hud.Game.IsInTown)
                return;

            var weaponRacks = Hud.Game.Actors.Where(x => !x.IsDisabled && !x.IsOperated && x.SnoActor.Kind == ActorKind.WeaponRack);
            foreach (var actor in weaponRacks)
            {
                Decorator.Paint(layer, actor, actor.FloorCoordinate, null);
            }

            var armorRacks = Hud.Game.Actors.Where(x => !x.IsDisabled && !x.IsOperated && x.SnoActor.Kind == ActorKind.ArmorRack);
            foreach (var actor in armorRacks)
            {
                Decorator.Paint(layer, actor, actor.FloorCoordinate, null);
            }
        }
    }
}