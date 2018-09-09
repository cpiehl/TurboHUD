namespace Turbo.Plugins.Default
{

    public class EliteMonsterSkillPlugin : BasePlugin, IInGameWorldPainter
	{

        public WorldDecoratorCollection FrozenBallDecorator { get; set; }
        public WorldDecoratorCollection MoltenDecorator { get; set; }
        public WorldDecoratorCollection MoltenExplosionDecorator { get; set; }
        public WorldDecoratorCollection DesecratorDecorator { get; set; }
        public WorldDecoratorCollection ThunderstormDecorator { get; set; }
        public WorldDecoratorCollection PlaguedDecorator { get; set; }
        public WorldDecoratorCollection GhomDecorator { get; set; }
        public WorldDecoratorCollection ArcaneDecorator { get; set; }
        public WorldDecoratorCollection ArcaneSpawnDecorator { get; set; }
        public WorldDecoratorCollection FrozenPulseDecorator { get; set; }

        public EliteMonsterSkillPlugin()
		{
            Enabled = true;
		}

        public override void Load(IController hud)
        {
            base.Load(hud);

            FrozenBallDecorator = new WorldDecoratorCollection(
                new GroundCircleDecorator(Hud)
                {
                    Brush = Hud.Render.CreateBrush(128, 200, 200, 255, 3, SharpDX.Direct2D1.DashStyle.Dash),
                    Radius = 15.8f,
                },
                new GroundLabelDecorator(Hud)
                {
                    CountDownFrom = 3,
                    TextFont = Hud.Render.CreateFont("tahoma", 9, 255, 255, 255, 255, true, false, 128, 0, 0, 0, true),
                },
                new GroundTimerDecorator(Hud)
                {
                    CountDownFrom = 3,
                    BackgroundBrushEmpty = Hud.Render.CreateBrush(128, 0, 0, 0, 0),
                    BackgroundBrushFill = Hud.Render.CreateBrush(160, 100, 100, 240, 0),
                    Radius = 30,
                }
                );
            MoltenDecorator = new WorldDecoratorCollection(
                new GroundCircleDecorator(Hud)
                {
                    Brush = Hud.Render.CreateBrush(160, 255, 50, 50, 3, SharpDX.Direct2D1.DashStyle.Dash),
                    Radius = 13f,
                }
                );
            MoltenExplosionDecorator = new WorldDecoratorCollection(
                new GroundCircleDecorator(Hud)
                {
                    Brush = Hud.Render.CreateBrush(160, 255, 50, 50, 3, SharpDX.Direct2D1.DashStyle.Dash),
                    Radius = 13f,
                },
                new GroundLabelDecorator(Hud)
                {
                    CountDownFrom = 3,
                    TextFont = Hud.Render.CreateFont("tahoma", 9, 255, 255, 255, 255, true, false, 128, 0, 0, 0, true),
                },
                new GroundTimerDecorator(Hud)
                {
                    CountDownFrom = 3,
                    BackgroundBrushEmpty = Hud.Render.CreateBrush(128, 0, 0, 0, 0),
                    BackgroundBrushFill = Hud.Render.CreateBrush(200, 255, 32, 32, 0),
                    Radius = 30,
                }
                );
            DesecratorDecorator = new WorldDecoratorCollection(
                new GroundCircleDecorator(Hud)
                {
                    Brush = Hud.Render.CreateBrush(160, 255, 50, 50, 3, SharpDX.Direct2D1.DashStyle.Dash),
                    Radius = 8f,
                }
                );
            ThunderstormDecorator = new WorldDecoratorCollection(
                new GroundCircleDecorator(Hud)
                {
                    Brush = Hud.Render.CreateBrush(16, 200, 200, 255, 0),
                    Radius = 16f,
                },
                new GroundCircleDecorator(Hud)
                {
                    Brush = Hud.Render.CreateBrush(128, 200, 200, 255, 3, SharpDX.Direct2D1.DashStyle.Dash),
                    Radius = 16f,
                }
                );
            PlaguedDecorator = new WorldDecoratorCollection(
                new GroundCircleDecorator(Hud)
                {
                    Brush = Hud.Render.CreateBrush(128, 160, 255, 160, 3, SharpDX.Direct2D1.DashStyle.Dash),
                    Radius = 12f,
                }
                );
            GhomDecorator = new WorldDecoratorCollection(
                new GroundCircleDecorator(Hud)
                {
                    Brush = Hud.Render.CreateBrush(128, 160, 255, 160, 3, SharpDX.Direct2D1.DashStyle.Dash),
                    Radius = 20f,
                }
                );
            ArcaneDecorator = new WorldDecoratorCollection(
                new GroundCircleDecorator(Hud)
                {
                    Brush = Hud.Render.CreateBrush(128, 255, 60, 255, 3, SharpDX.Direct2D1.DashStyle.Dash),
                    Radius = 6f,
                }
                );
            ArcaneSpawnDecorator = new WorldDecoratorCollection(
                new GroundCircleDecorator(Hud)
                {
                    Brush = Hud.Render.CreateBrush(128, 255, 60, 255, 3, SharpDX.Direct2D1.DashStyle.Dash),
                    Radius = 6f,
                },
                new GroundLabelDecorator(Hud)
                {
                    CountDownFrom = 2,
                    TextFont = Hud.Render.CreateFont("tahoma", 9, 255, 255, 255, 255, true, false, 128, 0, 0, 0, true),
                },
                new GroundTimerDecorator(Hud)
                {
                    CountDownFrom = 2,
                    BackgroundBrushEmpty = Hud.Render.CreateBrush(128, 0, 0, 0, 0),
                    BackgroundBrushFill = Hud.Render.CreateBrush(200, 255, 32, 255, 0),
                    Radius = 30,
                }
                );
            FrozenPulseDecorator = new WorldDecoratorCollection(
                new GroundCircleDecorator(Hud)
                {
                    Brush = Hud.Render.CreateBrush(128, 200, 200, 255, 3, SharpDX.Direct2D1.DashStyle.Dash),
                    Radius = 14f,
                }
                );
        }

        public void PaintWorld(WorldLayer layer)
        {
            var actors = Hud.Game.Actors;
            foreach (var actor in actors)
            {
                switch (actor.SnoActor.Sno)
                {
                    case 223675:
                        FrozenBallDecorator.Paint(layer, actor, actor.FloorCoordinate, null);
                        break;
                    case 4803:
                        MoltenExplosionDecorator.Paint(layer, actor, actor.FloorCoordinate, null);
                        break;
                    case 4804:
                    case 224225:
                    case 247987:
                        MoltenDecorator.Paint(layer, actor, actor.FloorCoordinate, null);
                        break;
                    case 84608:
                        DesecratorDecorator.Paint(layer, actor, actor.FloorCoordinate, null);
                        break;
                    case 341512:
                        ThunderstormDecorator.Paint(layer, actor, actor.FloorCoordinate, null);
                        break;
                    case 108869:
                    case 3865:
                        PlaguedDecorator.Paint(layer, actor, actor.FloorCoordinate, null);
                        break;
                    case 93837:
                        GhomDecorator.Paint(layer, actor, actor.FloorCoordinate, null);
                        break;
                    case 219702:
                    case 221225:
                        ArcaneDecorator.Paint(layer, actor, actor.FloorCoordinate, null);
                        break;
                    case 257306:
                        ArcaneSpawnDecorator.Paint(layer, actor, actor.FloorCoordinate, null);
                        break;
                    case 349774:
                        FrozenPulseDecorator.Paint(layer, actor, actor.FloorCoordinate, null);
                        break;
                }
            }
        }

    }

}