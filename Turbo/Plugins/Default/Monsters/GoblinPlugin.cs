using System.Collections.Generic;
using System.Linq;

namespace Turbo.Plugins.Default
{

    public class GoblinPlugin : BasePlugin, IInGameWorldPainter
    {

        public WorldDecoratorCollection PortalDecorator { get; set; }
        public WorldDecoratorCollection DefaultGoblinDecorator { get; set; }

        public WorldDecoratorCollection MalevolentTormentorDecorator { get; set; }
        public WorldDecoratorCollection BloodThiefDecorator { get; set; }
        public WorldDecoratorCollection OdiousCollectorDecorator { get; set; }
        public WorldDecoratorCollection GemHoarderDecorator { get; set; }
        public WorldDecoratorCollection GelatinousDecorator { get; set; }
        public WorldDecoratorCollection GildedBaronDecorator { get; set; }
        public WorldDecoratorCollection InsufferableMiscreantDecorator { get; set; }
        public WorldDecoratorCollection RainbowGoblinDecorator { get; set; }
        public WorldDecoratorCollection MenageristGoblinDecorator { get; set; }
        public WorldDecoratorCollection TreasureFiendGoblinDecorator { get; set; }

        public Dictionary<uint, WorldDecoratorCollection> SnoMapping { get; private set; }

        public bool EnableSpeak { get; set; }

        public GoblinPlugin()
        {
            Enabled = true;
            SnoMapping = new Dictionary<uint, WorldDecoratorCollection>();
            EnableSpeak = false;
        }

        public override void Load(IController hud)
        {
            base.Load(hud);

            CreateDecorators();
            SnoMapping.Add(413289, MalevolentTormentorDecorator);
            SnoMapping.Add(408989, BloodThiefDecorator);
            SnoMapping.Add(5985, OdiousCollectorDecorator);
            SnoMapping.Add(5987, GemHoarderDecorator);
            SnoMapping.Add(408354, GelatinousDecorator); // Gelatinous Sire
            SnoMapping.Add(410572, GelatinousDecorator); // Gelatinous Spawn
            SnoMapping.Add(410574, GelatinousDecorator); // Gelatinous Spawn
            SnoMapping.Add(429161, GildedBaronDecorator);
            SnoMapping.Add(408655, InsufferableMiscreantDecorator);
            SnoMapping.Add(450993, MenageristGoblinDecorator);
            SnoMapping.Add(405186, RainbowGoblinDecorator);
            SnoMapping.Add(380657, TreasureFiendGoblinDecorator);
        }

        private void CreateDecorators()
        {

            PortalDecorator = new WorldDecoratorCollection(
                new MapShapeDecorator(Hud)
                {
                    Brush = Hud.Render.CreateBrush(180, 255, 255, 255, 0),
                    ShadowBrush = Hud.Render.CreateBrush(96, 0, 0, 0, 1),
                    Radius = 8.0f,
                    ShapePainter = new CircleShapePainter(Hud),
                },
                new MapShapeDecorator(Hud)
                {
                    Brush = Hud.Render.CreateBrush(180, 120, 0, 0, 0),
                    ShadowBrush = Hud.Render.CreateBrush(96, 0, 0, 0, 1),
                    Radius = 2.5f,
                    ShapePainter = new CircleShapePainter(Hud),
                }
                );

            TreasureFiendGoblinDecorator = new WorldDecoratorCollection(
                new GroundCircleDecorator(Hud)
                {
                    Brush = Hud.Render.CreateBrush(180, 255, 163, 15, -2),
                    Radius = 1.5f,
                },
                new GroundLabelDecorator(Hud)
                {
                    BackgroundBrush = Hud.Render.CreateBrush(180, 255, 163, 15, 0),
                    BorderBrush = Hud.Render.CreateBrush(0, 0, 0, 0, 0),
                    TextFont = Hud.Render.CreateFont("tahoma", 7, 255, 0, 0, 0, true, false, false)
                },
                new MapShapeDecorator(Hud)
                {
                    ShapePainter = new CircleShapePainter(Hud),
                    Brush = Hud.Render.CreateBrush(180, 255, 198, 107, 3),
                    ShadowBrush = Hud.Render.CreateBrush(96, 0, 0, 0, 1),
                    Radius = 8f,
                },
                new MapShapeDecorator(Hud)
                {
                    ShapePainter = new CircleShapePainter(Hud),
                    Brush = Hud.Render.CreateBrush(180, 255, 163, 15, 3),
                    ShadowBrush = Hud.Render.CreateBrush(96, 0, 0, 0, 1),
                    Radius = 4.5f,
                }
                );

            MenageristGoblinDecorator = new WorldDecoratorCollection(
                new GroundCircleDecorator(Hud)
                {
                    Brush = Hud.Render.CreateBrush(180, 255, 255, 0, -2),
                    Radius = 1.5f,
                },
                new GroundLabelDecorator(Hud)
                {
                    BackgroundBrush = Hud.Render.CreateBrush(180, 255, 255, 0, 0),
                    BorderBrush = Hud.Render.CreateBrush(0, 0, 0, 0, 0),
                    TextFont = Hud.Render.CreateFont("tahoma", 7, 255, 0, 0, 0, true, false, false)
                },
                new MapShapeDecorator(Hud)
                {
                    ShapePainter = new CircleShapePainter(Hud),
                    Brush = Hud.Render.CreateBrush(180, 150, 150, 225, 3),
                    ShadowBrush = Hud.Render.CreateBrush(96, 0, 0, 0, 1),
                    Radius = 8f,
                },
                new MapShapeDecorator(Hud)
                {
                    ShapePainter = new CircleShapePainter(Hud),
                    Brush = Hud.Render.CreateBrush(180, 200, 0, 0, 3),
                    ShadowBrush = Hud.Render.CreateBrush(96, 0, 0, 0, 1),
                    Radius = 4.5f,
                }
                );

            RainbowGoblinDecorator = new WorldDecoratorCollection(
                new GroundCircleDecorator(Hud)
                {
                    Brush = Hud.Render.CreateBrush(180, 255, 255, 0, -2),
                    Radius = 1.5f,
                },
                new GroundLabelDecorator(Hud)
                {
                    BackgroundBrush = Hud.Render.CreateBrush(180, 255, 255, 0, 0),
                    BorderBrush = Hud.Render.CreateBrush(0, 0, 0, 0, 0),
                    TextFont = Hud.Render.CreateFont("tahoma", 7, 255, 0, 0, 0, true, false, false)
                },
                new MapShapeDecorator(Hud)
                {
                    ShapePainter = new CircleShapePainter(Hud),
                    Brush = Hud.Render.CreateBrush(180, 150, 150, 225, 3),
                    ShadowBrush = Hud.Render.CreateBrush(96, 0, 0, 0, 1),
                    Radius = 8f,
                },
                new MapShapeDecorator(Hud)
                {
                    ShapePainter = new CircleShapePainter(Hud),
                    Brush = Hud.Render.CreateBrush(180, 200, 0, 0, 3),
                    ShadowBrush = Hud.Render.CreateBrush(96, 0, 0, 0, 1),
                    Radius = 4.5f,
                }
                );

            DefaultGoblinDecorator = new WorldDecoratorCollection(
                new GroundCircleDecorator(Hud)
                {
                    Brush = Hud.Render.CreateBrush(200, 150, 150, 150, -2),
                    Radius = 1.5f,
                },
                new GroundLabelDecorator(Hud)
                {
                    BackgroundBrush = Hud.Render.CreateBrush(200, 150, 150, 150, 0),
                    BorderBrush = Hud.Render.CreateBrush(0, 0, 0, 0, 0),
                    TextFont = Hud.Render.CreateFont("tahoma", 7, 255, 0, 0, 0, true, false, false)
                },
                new MapShapeDecorator(Hud)
                {
                    ShapePainter = new CircleShapePainter(Hud),
                    Brush = Hud.Render.CreateBrush(180, 210, 180, 140, 3),
                    ShadowBrush = Hud.Render.CreateBrush(96, 0, 0, 0, 1),
                    Radius = 8f,
                },
                new MapShapeDecorator(Hud)
                {
                    ShapePainter = new CircleShapePainter(Hud),
                    Brush = Hud.Render.CreateBrush(180, 180, 180, 180, 3),
                    ShadowBrush = Hud.Render.CreateBrush(96, 0, 0, 0, 1),
                    Radius = 4.5f,
                }
                );

            InsufferableMiscreantDecorator = new WorldDecoratorCollection(
                new GroundCircleDecorator(Hud)
                {
                    Brush = Hud.Render.CreateBrush(180, 255, 50, 50, -2),
                    Radius = 1.5f,
                },
                new GroundLabelDecorator(Hud)
                {
                    BackgroundBrush = Hud.Render.CreateBrush(180, 255, 50, 50, 0),
                    BorderBrush = Hud.Render.CreateBrush(0, 0, 0, 0, 0),
                    TextFont = Hud.Render.CreateFont("tahoma", 7, 255, 255, 255, 255, true, false, false)
                },
                new MapShapeDecorator(Hud)
                {
                    ShapePainter = new CircleShapePainter(Hud),
                    Brush = Hud.Render.CreateBrush(180, 255, 255, 255, 3),
                    ShadowBrush = Hud.Render.CreateBrush(96, 0, 0, 0, 1),
                    Radius = 8f,
                },
                new MapShapeDecorator(Hud)
                {
                    ShapePainter = new CircleShapePainter(Hud),
                    Brush = Hud.Render.CreateBrush(180, 255, 0, 0, 3),
                    ShadowBrush = Hud.Render.CreateBrush(96, 0, 0, 0, 1),
                    Radius = 4.5f,
                }
                );

            GildedBaronDecorator = new WorldDecoratorCollection(
                new GroundCircleDecorator(Hud)
                {
                    Brush = Hud.Render.CreateBrush(180, 255, 240, 0, -2),
                    Radius = 1.5f,
                },
                new GroundLabelDecorator(Hud)
                {
                    BackgroundBrush = Hud.Render.CreateBrush(180, 255, 240, 0, 0),
                    BorderBrush = Hud.Render.CreateBrush(0, 0, 0, 0, 0),
                    TextFont = Hud.Render.CreateFont("tahoma", 7, 220, 0, 0, 0, true, false, false)
                },
                new MapShapeDecorator(Hud)
                {
                    ShapePainter = new CircleShapePainter(Hud),
                    Brush = Hud.Render.CreateBrush(200, 255, 255, 0, 3),
                    ShadowBrush = Hud.Render.CreateBrush(96, 0, 0, 0, 1),
                    Radius = 8f,
                },
                new MapShapeDecorator(Hud)
                {
                    ShapePainter = new CircleShapePainter(Hud),
                    Brush = Hud.Render.CreateBrush(200, 255, 255, 0, 3),
                    ShadowBrush = Hud.Render.CreateBrush(96, 0, 0, 0, 1),
                    Radius = 4.5f,
                }
                );

            GelatinousDecorator = new WorldDecoratorCollection(
                new GroundCircleDecorator(Hud)
                {
                    Brush = Hud.Render.CreateBrush(180, 0, 0, 255, -2),
                    Radius = 1.5f,
                },
                new GroundLabelDecorator(Hud)
                {
                    BackgroundBrush = Hud.Render.CreateBrush(180, 0, 0, 255, 0),
                    BorderBrush = Hud.Render.CreateBrush(0, 0, 0, 0, 0),
                    TextFont = Hud.Render.CreateFont("tahoma", 7, 255, 255, 255, 255, true, false, false)
                },
                new MapShapeDecorator(Hud)
                {
                    ShapePainter = new CircleShapePainter(Hud),
                    Brush = Hud.Render.CreateBrush(180, 255, 255, 255, 3),
                    ShadowBrush = Hud.Render.CreateBrush(96, 0, 0, 0, 1),
                    Radius = 8f,
                },
                new MapShapeDecorator(Hud)
                {
                    ShapePainter = new CircleShapePainter(Hud),
                    Brush = Hud.Render.CreateBrush(180, 50, 50, 200, 3),
                    ShadowBrush = Hud.Render.CreateBrush(96, 0, 0, 0, 1),
                    Radius = 4.5f,
                }
                );

            GemHoarderDecorator = new WorldDecoratorCollection(
                new GroundCircleDecorator(Hud)
                {
                    Brush = Hud.Render.CreateBrush(180, 255, 255, 255, -2),
                    Radius = 1.5f,
                },
                new GroundLabelDecorator(Hud)
                {
                    BackgroundBrush = Hud.Render.CreateBrush(180, 255, 255, 255, 0),
                    BorderBrush = Hud.Render.CreateBrush(0, 0, 0, 0, 0),
                    TextFont = Hud.Render.CreateFont("tahoma", 7, 255, 0, 0, 0, true, false, false)
                },
                new MapShapeDecorator(Hud)
                {
                    ShapePainter = new CircleShapePainter(Hud),
                    Brush = Hud.Render.CreateBrush(220, 255, 255, 255, 3),
                    ShadowBrush = Hud.Render.CreateBrush(96, 0, 0, 0, 1),
                    Radius = 8f,
                },
                new MapShapeDecorator(Hud)
                {
                    ShapePainter = new CircleShapePainter(Hud),
                    Brush = Hud.Render.CreateBrush(100, 220, 220, 220, 3),
                    ShadowBrush = Hud.Render.CreateBrush(96, 0, 0, 0, 1),
                    Radius = 4.5f,
                }
                );

            OdiousCollectorDecorator = new WorldDecoratorCollection(
                new GroundCircleDecorator(Hud)
                {
                    Brush = Hud.Render.CreateBrush(180, 0, 255, 0, -2),
                    Radius = 1.5f,
                },
                new GroundLabelDecorator(Hud)
                {
                    BackgroundBrush = Hud.Render.CreateBrush(180, 0, 255, 0, 0),
                    BorderBrush = Hud.Render.CreateBrush(0, 0, 0, 0, 0),
                    TextFont = Hud.Render.CreateFont("tahoma", 7, 255, 0, 0, 0, true, false, false)
                },
                new MapShapeDecorator(Hud)
                {
                    ShapePainter = new CircleShapePainter(Hud),
                    Brush = Hud.Render.CreateBrush(255, 255, 255, 255, 3),
                    ShadowBrush = Hud.Render.CreateBrush(96, 0, 0, 0, 1),
                    Radius = 8f,
                },
                new MapShapeDecorator(Hud)
                {
                    ShapePainter = new CircleShapePainter(Hud),
                    Brush = Hud.Render.CreateBrush(255, 0, 255, 0, 3),
                    ShadowBrush = Hud.Render.CreateBrush(96, 0, 0, 0, 1),
                    Radius = 4.5f,
                }
                );

            MalevolentTormentorDecorator = new WorldDecoratorCollection(
                new GroundCircleDecorator(Hud)
                {
                    Brush = Hud.Render.CreateBrush(180, 255, 255, 0, -2),
                    Radius = 1.5f,
                },
                new GroundLabelDecorator(Hud)
                {
                    BackgroundBrush = Hud.Render.CreateBrush(180, 255, 255, 0, 0),
                    BorderBrush = Hud.Render.CreateBrush(0, 0, 0, 0, 0),
                    TextFont = Hud.Render.CreateFont("tahoma", 7, 255, 0, 0, 0, true, false, false)
                },
                new MapShapeDecorator(Hud)
                {
                    ShapePainter = new CircleShapePainter(Hud),
                    Brush = Hud.Render.CreateBrush(180, 255, 255, 112, 3),
                    ShadowBrush = Hud.Render.CreateBrush(96, 0, 0, 0, 1),
                    Radius = 8f,
                },
                new MapShapeDecorator(Hud)
                {
                    ShapePainter = new CircleShapePainter(Hud),
                    Brush = Hud.Render.CreateBrush(180, 255, 255, 0, 3),
                    ShadowBrush = Hud.Render.CreateBrush(96, 0, 0, 0, 1),
                    Radius = 4.5f,
                }
                );

            BloodThiefDecorator = new WorldDecoratorCollection(
                new GroundCircleDecorator(Hud)
                {
                    Brush = Hud.Render.CreateBrush(180, 155, 0, 255, -2),
                    Radius = 1.5f,
                },
                new GroundLabelDecorator(Hud)
                {
                    BackgroundBrush = Hud.Render.CreateBrush(180, 155, 0, 255, 0),
                    BorderBrush = Hud.Render.CreateBrush(0, 0, 0, 0, 0),
                    TextFont = Hud.Render.CreateFont("tahoma", 7, 255, 255, 255, 255, true, false, false)
                },
                new MapShapeDecorator(Hud)
                {
                    ShapePainter = new CircleShapePainter(Hud),
                    Brush = Hud.Render.CreateBrush(180, 255, 87, 87, 3),
                    ShadowBrush = Hud.Render.CreateBrush(96, 0, 0, 0, 1),
                    Radius = 8f,
                },
                new MapShapeDecorator(Hud)
                {
                    ShapePainter = new CircleShapePainter(Hud),
                    Brush = Hud.Render.CreateBrush(180, 255, 0, 0, 3),
                    ShadowBrush = Hud.Render.CreateBrush(96, 0, 0, 0, 1),
                    Radius = 4.5f,
                }
                );
        }

        public void PaintWorld(WorldLayer layer)
        {
            var portals = Hud.Game.Actors.Where(x => x.SnoActor.Sno == 410460);
            foreach (var actor in portals)
            {
                PortalDecorator.Paint(layer, actor, actor.FloorCoordinate, null);
            }

            var goblins = Hud.Game.AliveMonsters.Where(x => x.SnoMonster.Priority == MonsterPriority.goblin);
            foreach (var goblin in goblins)
            {
                if (EnableSpeak && (goblin.LastSpeak == null) && Hud.Sound.LastSpeak.TimerTest(5000))
                {
                    Hud.Sound.Speak(goblin.SnoMonster.NameLocalized);
                    goblin.LastSpeak = Hud.Time.CreateAndStartWatch();
                }

                WorldDecoratorCollection decorator;
                if (!SnoMapping.TryGetValue(goblin.SnoActor.Sno, out decorator))
                {
                    decorator = DefaultGoblinDecorator;
                }

                decorator.Paint(layer, goblin, goblin.FloorCoordinate, goblin.SnoMonster.NameLocalized);
            }
        }

        public IEnumerable<WorldDecoratorCollection> AllGoblinDecorators()
        {
            yield return MalevolentTormentorDecorator;
            yield return BloodThiefDecorator;
            yield return OdiousCollectorDecorator;
            yield return GemHoarderDecorator;
            yield return GelatinousDecorator;
            yield return GildedBaronDecorator;
            yield return InsufferableMiscreantDecorator;
            yield return DefaultGoblinDecorator;
            yield return RainbowGoblinDecorator;
            yield return MenageristGoblinDecorator;
            yield return TreasureFiendGoblinDecorator;
        }

    }

}