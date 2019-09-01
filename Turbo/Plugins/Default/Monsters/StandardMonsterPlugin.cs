namespace Turbo.Plugins.Default
{
    public class StandardMonsterPlugin : BasePlugin, IInGameWorldPainter
    {
        public WorldDecoratorCollection TrashDecorator { get; set; }
        public WorldDecoratorCollection InvisibleDecorator { get; set; }
        public WorldDecoratorCollection EliteChampionDecorator { get; set; }
        public WorldDecoratorCollection EliteMinionDecorator { get; set; }
        public WorldDecoratorCollection EliteLeaderDecorator { get; set; }
        public WorldDecoratorCollection EliteUniqueDecorator { get; set; }
        public WorldDecoratorCollection KeywardenDecorator { get; set; }
        public WorldDecoratorCollection BossDecorator { get; set; }

        public bool HideOnIllusions { get; set; } = true;

        public StandardMonsterPlugin()
        {
            Enabled = true;
        }

        public override void Load(IController hud)
        {
            base.Load(hud);

            var shadowBrush = Hud.Render.CreateBrush(96, 0, 0, 0, 1);

            TrashDecorator = new WorldDecoratorCollection(
                new MapShapeDecorator(Hud)
                {
                    Brush = Hud.Render.CreateBrush(128, 200, 200, 200, 0),
                    ShadowBrush = shadowBrush,
                    ShapePainter = new CircleShapePainter(Hud),
                    Radius = 2,
                }
                );

            var invisibleGroundBrush = Hud.Render.CreateBrush(128, 255, 170, 255, 3, SharpDX.Direct2D1.DashStyle.Dash);
            InvisibleDecorator = new WorldDecoratorCollection(
                new GroundCircleDecorator(Hud)
                {
                    Brush = invisibleGroundBrush,
                    Radius = -1,
                },
                new MapShapeDecorator(Hud)
                {
                    Brush = Hud.Render.CreateBrush(128, 255, 170, 255, 0),
                    ShadowBrush = shadowBrush,
                    Radius = 2,
                    ShapePainter = new CircleShapePainter(Hud),
                }
                );

            EliteChampionDecorator = new WorldDecoratorCollection(
                new MapShapeDecorator(Hud)
                {
                    Brush = Hud.Render.CreateBrush(180, 64, 128, 255, 0),
                    ShadowBrush = shadowBrush,
                    Radius = 10,
                    ShapePainter = new CircleShapePainter(Hud),
                }
                );

            EliteMinionDecorator = new WorldDecoratorCollection(
                new MapShapeDecorator(Hud)
                {
                    Brush = Hud.Render.CreateBrush(180, 192, 92, 20, 0),
                    ShadowBrush = shadowBrush,
                    Radius = 8,
                    ShapePainter = new CircleShapePainter(Hud),
                }
                );

            EliteLeaderDecorator = new WorldDecoratorCollection(
                new MapShapeDecorator(Hud)
                {
                    Brush = Hud.Render.CreateBrush(180, 255, 148, 20, 0),
                    ShadowBrush = shadowBrush,
                    Radius = 10,
                    ShapePainter = new CircleShapePainter(Hud),
                }
                );

            EliteUniqueDecorator = new WorldDecoratorCollection(
                new MapShapeDecorator(Hud)
                {
                    Brush = Hud.Render.CreateBrush(180, 255, 140, 255, 0),
                    ShadowBrush = shadowBrush,
                    Radius = 10,
                    ShapePainter = new CircleShapePainter(Hud),
                }
                );

            KeywardenDecorator = new WorldDecoratorCollection(
                new MapShapeDecorator(Hud)
                {
                    Brush = Hud.Render.CreateBrush(180, 255, 255, 255, 0),
                    Radius = 6.0f,
                    ShapePainter = new CircleShapePainter(Hud),
                },
                new MapShapeDecorator(Hud)
                {
                    Brush = Hud.Render.CreateBrush(180, 170, 0, 255, 0),
                    ShadowBrush = shadowBrush,
                    Radius = 6.0f,
                    ShapePainter = new CircleShapePainter(Hud),
                },
                new GroundLabelDecorator(Hud)
                {
                    BackgroundBrush = Hud.Render.CreateBrush(255, 170, 0, 255, 0),
                    BorderBrush = Hud.Render.CreateBrush(192, 255, 255, 255, 1),
                    TextFont = Hud.Render.CreateFont("tahoma", 6.5f, 255, 255, 255, 255, true, false, false)
                }
                );

            BossDecorator = new WorldDecoratorCollection(
                new MapShapeDecorator(Hud)
                {
                    Brush = Hud.Render.CreateBrush(180, 255, 96, 0, 0),
                    ShadowBrush = shadowBrush,
                    Radius = 10.0f,
                    ShapePainter = new CircleShapePainter(Hud),
                }
                );
        }

        public void PaintWorld(WorldLayer layer)
        {
            var inRift = Hud.Game.SpecialArea == SpecialArea.Rift || Hud.Game.SpecialArea == SpecialArea.GreaterRift;

            var monsters = Hud.Game.AliveMonsters;
            foreach (var monster in monsters)
            {
                if (HideOnIllusions && (monster.SummonerAcdDynamicId != 0) && (monster.Rarity == ActorRarity.RareMinion))
                    continue;

                // trash
                if (!inRift && !monster.Invisible && !monster.IsElite && (monster.SnoMonster.Priority != MonsterPriority.goblin) && (monster.SnoMonster.Priority != MonsterPriority.boss) && (monster.SnoMonster.Priority != MonsterPriority.keywarden))
                {
                    TrashDecorator.Paint(layer, monster, monster.FloorCoordinate, monster.SnoMonster.NameLocalized);
                }

                if (monster.Invisible)
                {
                    InvisibleDecorator.Paint(layer, monster, monster.FloorCoordinate, monster.SnoMonster.NameLocalized);
                }

                if (monster.Rarity == ActorRarity.Champion)
                {
                    EliteChampionDecorator.Paint(layer, monster, monster.FloorCoordinate, monster.SnoMonster.NameLocalized);
                }

                if (monster.Rarity == ActorRarity.RareMinion)
                {
                    EliteMinionDecorator.Paint(layer, monster, monster.FloorCoordinate, monster.SnoMonster.NameLocalized);
                }

                if (monster.Rarity == ActorRarity.Rare)
                {
                    EliteLeaderDecorator.Paint(layer, monster, monster.FloorCoordinate, monster.SnoMonster.NameLocalized);
                }

                if ((monster.Rarity == ActorRarity.Unique) && (monster.SnoMonster.Priority < MonsterPriority.keywarden))
                {
                    EliteUniqueDecorator.Paint(layer, monster, monster.FloorCoordinate, monster.SnoMonster.NameLocalized);
                }

                if (monster.SnoMonster.Priority == MonsterPriority.keywarden)
                {
                    KeywardenDecorator.Paint(layer, monster, monster.FloorCoordinate, monster.SnoMonster.NameLocalized);
                }

                if (monster.SnoMonster.Priority == MonsterPriority.boss)
                {
                    BossDecorator.Paint(layer, monster, monster.FloorCoordinate, monster.SnoMonster.NameLocalized);
                }
            }
        }
    }
}