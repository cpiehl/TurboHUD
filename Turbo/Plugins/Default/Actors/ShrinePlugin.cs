using System.Linq;

namespace Turbo.Plugins.Default
{
    public class ShrinePlugin : BasePlugin, IInGameWorldPainter
    {
        public WorldDecoratorCollection AllShrineDecorator { get; set; }
        public WorldDecoratorCollection HealingWellDecorator { get; set; }
        public WorldDecoratorCollection PoolOfReflectionDecorator { get; set; }
        public WorldDecoratorCollection PossibleRiftPylonDecorator { get; set; }

        public ShrinePlugin()
        {
            Enabled = true;
        }

        public override void Load(IController hud)
        {
            base.Load(hud);

            var shadowBrush = Hud.Render.CreateBrush(96, 0, 0, 0, 1);

            AllShrineDecorator = new WorldDecoratorCollection(
                new MapShapeDecorator(Hud)
                {
                    Brush = Hud.Render.CreateBrush(255, 255, 255, 64, 2),
                    ShadowBrush = shadowBrush,
                    Radius = 4.0f,
                    ShapePainter = new RectangleShapePainter(Hud),
                },
                new GroundLabelDecorator(Hud)
                {
                    BackgroundBrush = Hud.Render.CreateBrush(255, 255, 255, 64, 0),
                    TextFont = Hud.Render.CreateFont("tahoma", 6.5f, 255, 0, 0, 0, false, false, false),
                }
                );

            HealingWellDecorator = new WorldDecoratorCollection(
                new MapShapeDecorator(Hud)
                {
                    Brush = Hud.Render.CreateBrush(255, 255, 128, 128, 1),
                    ShadowBrush = shadowBrush,
                    Radius = 6.0f,
                    ShapePainter = new WellShapePainter(Hud),
                },
                new GroundLabelDecorator(Hud)
                {
                    BackgroundBrush = Hud.Render.CreateBrush(255, 255, 255, 64, 0),
                    TextFont = Hud.Render.CreateFont("tahoma", 6.5f, 255, 0, 0, 0, false, false, false),
                }
                );

            PoolOfReflectionDecorator = new WorldDecoratorCollection(
                new MapShapeDecorator(Hud)
                {
                    Brush = Hud.Render.CreateBrush(255, 255, 255, 64, 2),
                    ShadowBrush = shadowBrush,
                    Radius = 4.0f,
                    ShapePainter = new CircleShapePainter(Hud),
                },
                new GroundLabelDecorator(Hud)
                {
                    BackgroundBrush = Hud.Render.CreateBrush(255, 255, 255, 64, 0),
                    TextFont = Hud.Render.CreateFont("tahoma", 6.5f, 255, 0, 0, 0, false, false, false),
                }
                );

            PossibleRiftPylonDecorator = new WorldDecoratorCollection(
                new MapShapeDecorator(Hud)
                {
                    Brush = Hud.Render.CreateBrush(192, 255, 255, 55, 3),
                    ShadowBrush = shadowBrush,
                    Radius = 5.0f,
                    ShapePainter = new CircleShapePainter(Hud),
                },
                new MapLabelDecorator(Hud)
                {
                    LabelFont = Hud.Render.CreateFont("tahoma", 6f, 192, 255, 255, 55, false, false, 128, 0, 0, 0, true),
                    RadiusOffset = 5.0f,
                }
                );
        }

        public void PaintWorld(WorldLayer layer)
        {
            var shrines = Hud.Game.Shrines.Where(x => !x.IsDisabled && !x.IsOperated && (x.Type != ShrineType.HealingWell) && (x.Type != ShrineType.PoolOfReflection));
            foreach (var actor in shrines)
            {
                AllShrineDecorator.ToggleDecorators<GroundLabelDecorator>(!actor.IsOnScreen); // do not display ground labels when the actor is on the screen
                AllShrineDecorator.Paint(layer, actor, actor.FloorCoordinate, actor.SnoActor.NameLocalized);
            }

            var healingWells = Hud.Game.Shrines.Where(x => !x.IsDisabled && !x.IsOperated && (x.Type == ShrineType.HealingWell));
            foreach (var actor in healingWells)
            {
                HealingWellDecorator.ToggleDecorators<GroundLabelDecorator>(!actor.IsOnScreen); // do not display ground labels when the actor is on the screen
                HealingWellDecorator.Paint(layer, actor, actor.FloorCoordinate, actor.SnoActor.NameLocalized);
            }

            var poolOfReflections = Hud.Game.Shrines.Where(x => !x.IsDisabled && !x.IsOperated && (x.Type == ShrineType.PoolOfReflection));
            foreach (var actor in poolOfReflections)
            {
                PoolOfReflectionDecorator.ToggleDecorators<GroundLabelDecorator>(!actor.IsOnScreen); // do not display ground labels when the actor is on the screen
                PoolOfReflectionDecorator.Paint(layer, actor, actor.FloorCoordinate, actor.SnoActor.NameLocalized);
            }

            var riftPylonSpawnPoints = Hud.Game.Actors.Where(x => x.SnoActor.Sno == ActorSnoEnum._markerlocation_tieredriftpylon);
            foreach (var actor in riftPylonSpawnPoints)
            {
                PossibleRiftPylonDecorator.Paint(layer, actor, actor.FloorCoordinate, "pylon?");
            }
        }
    }
}