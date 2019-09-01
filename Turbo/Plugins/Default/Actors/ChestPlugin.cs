using System.Linq;

namespace Turbo.Plugins.Default
{
    public class ChestPlugin : BasePlugin, IInGameWorldPainter
    {
        public WorldDecoratorCollection LoreChestDecorator { get; set; }
        public WorldDecoratorCollection NormalChestDecorator { get; set; }
        public WorldDecoratorCollection ResplendentChestDecorator { get; set; }

        public ChestPlugin()
        {
            Enabled = true;
        }

        public override void Load(IController hud)
        {
            base.Load(hud);

            LoreChestDecorator = new WorldDecoratorCollection(
                new GroundCircleDecorator(Hud)
                {
                    Brush = Hud.Render.CreateBrush(255, 0, 255, 0, -2),
                    Radius = 1.2f,
                }
                );

            NormalChestDecorator = new WorldDecoratorCollection(
                new MapShapeDecorator(Hud)
                {
                    Brush = Hud.Render.CreateBrush(255, 200, 255, 200, 1),
                    ShadowBrush = Hud.Render.CreateBrush(96, 0, 0, 0, 1),
                    Radius = 6.0f,
                    ShapePainter = new CircleShapePainter(Hud),
                }
                );

            ResplendentChestDecorator = new WorldDecoratorCollection(
                new MapShapeDecorator(Hud)
                {
                    Brush = Hud.Render.CreateBrush(255, 64, 255, 64, 2),
                    ShadowBrush = Hud.Render.CreateBrush(96, 0, 0, 0, 1),
                    Radius = 6.0f,
                    ShapePainter = new CircleShapePainter(Hud),
                }
                );
        }

        public void PaintWorld(WorldLayer layer)
        {
            if (Hud.Game.IsInTown)
                return;

            var loreChests = Hud.Game.Actors.Where(x => !x.IsDisabled && !x.IsOperated && x.GizmoType == GizmoType.LoreChest);
            foreach (var actor in loreChests)
            {
                LoreChestDecorator.Paint(layer, actor, actor.FloorCoordinate, actor.SnoActor.NameLocalized);
            }

            var normalChests = Hud.Game.Actors.Where(x => !x.IsDisabled && !x.IsOperated && x.SnoActor.Kind == ActorKind.ChestNormal);
            foreach (var actor in normalChests)
            {
                NormalChestDecorator.Paint(layer, actor, actor.FloorCoordinate, actor.SnoActor.NameLocalized);
            }

            var resplendentChests = Hud.Game.Actors.Where(x => !x.IsDisabled && !x.IsOperated && x.SnoActor.Kind == ActorKind.Chest);
            foreach (var actor in resplendentChests)
            {
                ResplendentChestDecorator.Paint(layer, actor, actor.FloorCoordinate, actor.SnoActor.NameLocalized);
            }
        }
    }
}