using System.Linq;

namespace Turbo.Plugins.Default
{
    public class MonsterRiftProgressionColoringPlugin : BasePlugin, IInGameWorldPainter
    {
        public WorldDecoratorCollection Decorator1 { get; set; }
        public WorldDecoratorCollection Decorator2 { get; set; }
        public WorldDecoratorCollection Decorator3 { get; set; }
        public WorldDecoratorCollection Decorator4 { get; set; }
        public WorldDecoratorCollection Decorator5 { get; set; }

        public MonsterRiftProgressionColoringPlugin()
        {
            Enabled = true;
        }

        public override void Load(IController hud)
        {
            base.Load(hud);

            var shadowBrush = Hud.Render.CreateBrush(96, 0, 0, 0, 1);

            Decorator1 = new WorldDecoratorCollection(
                new MapShapeDecorator(Hud)
                {
                    Brush = Hud.Render.CreateBrush(128, 200, 200, 200, 0),
                    ShadowBrush = shadowBrush,
                    ShapePainter = new CircleShapePainter(Hud),
                    Radius = 2,
                }
                );
            Decorator2 = new WorldDecoratorCollection(
                new MapShapeDecorator(Hud)
                {
                    Brush = Hud.Render.CreateBrush(180, 0, 200, 0, 0),
                    ShadowBrush = shadowBrush,
                    ShapePainter = new CircleShapePainter(Hud),
                    Radius = 4,
                }
                );
            Decorator3 = new WorldDecoratorCollection(
                new MapShapeDecorator(Hud)
                {
                    Brush = Hud.Render.CreateBrush(180, 0, 125, 0, 0),
                    ShadowBrush = shadowBrush,
                    ShapePainter = new CircleShapePainter(Hud),
                    Radius = 5,
                }
                );
            Decorator4 = new WorldDecoratorCollection(
                new MapShapeDecorator(Hud)
                {
                    Brush = Hud.Render.CreateBrush(180, 0, 200, 0, 0),
                    ShadowBrush = shadowBrush,
                    ShapePainter = new CircleShapePainter(Hud),
                    Radius = 6,
                },
                new MapShapeDecorator(Hud)
                {
                    Brush = Hud.Render.CreateBrush(180, 0, 55, 0, 2),
                    ShadowBrush = shadowBrush,
                    ShapePainter = new CircleShapePainter(Hud),
                    Radius = 6,
                }
                );
            Decorator5 = new WorldDecoratorCollection(
                new MapShapeDecorator(Hud)
                {
                    Brush = Hud.Render.CreateBrush(180, 0, 125, 0, 0),
                    ShadowBrush = shadowBrush,
                    ShapePainter = new CircleShapePainter(Hud),
                    Radius = 7,
                },
                new MapShapeDecorator(Hud)
                {
                    Brush = Hud.Render.CreateBrush(180, 0, 55, 0, 2),
                    ShadowBrush = shadowBrush,
                    ShapePainter = new CircleShapePainter(Hud),
                    Radius = 7,
                }
                );
        }

        public WorldDecoratorCollection GetDecoratorByProgression(float progression)
        {
            if (progression <= 1.0)
                return Decorator1;
            if (progression <= 2.0)
                return Decorator2;
            if (progression <= 3.0)
                return Decorator3;
            if (progression <= 4.0)
                return Decorator4;
            return Decorator5; // in theory there is no monster with >10 progression
        }

        public void PaintWorld(WorldLayer layer)
        {
            if ((Hud.Game.SpecialArea != SpecialArea.Rift) && (Hud.Game.SpecialArea != SpecialArea.GreaterRift))
                return;

            var monsters = Hud.Game.AliveMonsters.Where(x => !x.Invisible && !x.IsElite);
            foreach (var monster in monsters)
            {
                var decorator = GetDecoratorByProgression(monster.SnoMonster.RiftProgression);
                decorator.Paint(layer, monster, monster.FloorCoordinate, monster.SnoMonster.NameLocalized);
            }
        }
    }
}