namespace Turbo.Plugins.Default
{

    public class PlayerSkillPlugin : BasePlugin, IInGameWorldPainter
    {

        public WorldDecoratorCollection HydraDecorator { get; set; }
        public WorldDecoratorCollection SentryDecorator { get; set; }
        public WorldDecoratorCollection SentryWithCustomEngineeringDecorator { get; set; }
        public WorldDecoratorCollection BlackHoleDecorator { get; set; }
        public WorldDecoratorCollection PiranhadoDecorator { get; set; }
        public WorldDecoratorCollection SpiritWalkDecorator { get; set; }
        public WorldDecoratorCollection SpiritWalkWithJauntDecorator { get; set; }
        public WorldDecoratorCollection BigBadVoodooDecorator { get; set; }
        public WorldDecoratorCollection BigBadVoodooWithJungleDrumsDecorator { get; set; }
        public WorldDecoratorCollection InnerSanctuaryDefaultDecorator { get; set; }
        public WorldDecoratorCollection InnerSanctuaryTempleOfProtecteionDecorator { get; set; }
        public WorldDecoratorCollection InnerSanctuarySafeHavenDecorator { get; set; }
        public WorldDecoratorCollection InnerSanctuarySanctifiedGroundDecorator { get; set; }

        public PlayerSkillPlugin()
        {
            Enabled = true;
        }

        public override void Load(IController hud)
        {
            base.Load(hud);

            HydraDecorator = new WorldDecoratorCollection(
                new MapShapeDecorator(Hud)
                {
                    Brush = Hud.Render.CreateBrush(255, 255, 100, 100, 2),
                    ShadowBrush = Hud.Render.CreateBrush(96, 0, 0, 0, 1),
                    ShapePainter = new TriangleShapePainter(Hud),
                    Radius = 4f,
                },
                new GroundLabelDecorator(Hud)
                {
                    CountDownFrom = 15,
                    TextFont = Hud.Render.CreateFont("tahoma", 9, 255, 255, 200, 200, true, false, 128, 0, 0, 0, true),
                },
                new GroundTimerDecorator(Hud)
                {
                    CountDownFrom = 15,
                    BackgroundBrushEmpty = Hud.Render.CreateBrush(128, 0, 0, 0, 0),
                    BackgroundBrushFill = Hud.Render.CreateBrush(230, 255, 50, 50, 0),
                    Radius = 30,
                }
                );

            SentryDecorator = new WorldDecoratorCollection(
                new MapShapeDecorator(Hud)
                {
                    Brush = Hud.Render.CreateBrush(255, 240, 148, 32, 2),
                    ShadowBrush = Hud.Render.CreateBrush(96, 0, 0, 0, 1),
                    ShapePainter = new TriangleShapePainter(Hud),
                    Radius = 4f,
                },
                new GroundLabelDecorator(Hud)
                {
                    CountDownFrom = 30,
                    TextFont = Hud.Render.CreateFont("tahoma", 9, 255, 240, 148, 32, true, false, 128, 0, 0, 0, true),
                },
                new GroundTimerDecorator(Hud)
                {
                    CountDownFrom = 30,
                    BackgroundBrushEmpty = Hud.Render.CreateBrush(128, 0, 0, 0, 0),
                    BackgroundBrushFill = Hud.Render.CreateBrush(160, 240, 148, 32, 0),
                    Radius = 30,
                }
                );

            SentryWithCustomEngineeringDecorator = new WorldDecoratorCollection(
                new MapShapeDecorator(Hud)
                {
                    Brush = Hud.Render.CreateBrush(255, 240, 148, 32, 2),
                    ShadowBrush = Hud.Render.CreateBrush(96, 0, 0, 0, 1),
                    ShapePainter = new TriangleShapePainter(Hud),
                    Radius = 4f,
                },
                new GroundLabelDecorator(Hud)
                {
                    CountDownFrom = 60,
                    TextFont = Hud.Render.CreateFont("tahoma", 9, 255, 240, 148, 32, true, false, 128, 0, 0, 0, true),
                },
                new GroundTimerDecorator(Hud)
                {
                    CountDownFrom = 60,
                    BackgroundBrushEmpty = Hud.Render.CreateBrush(128, 0, 0, 0, 0),
                    BackgroundBrushFill = Hud.Render.CreateBrush(160, 240, 148, 32, 0),
                    Radius = 30,
                }
                );

            BlackHoleDecorator = new WorldDecoratorCollection(
                new GroundLabelDecorator(Hud)
                {
                    CountDownFrom = 2,
                    TextFont = Hud.Render.CreateFont("tahoma", 9, 255, 255, 200, 200, true, false, 128, 0, 0, 0, true),
                },
                new GroundTimerDecorator(Hud)
                {
                    CountDownFrom = 2,
                    BackgroundBrushEmpty = Hud.Render.CreateBrush(128, 0, 0, 0, 0),
                    BackgroundBrushFill = Hud.Render.CreateBrush(230, 255, 50, 50, 0),
                    Radius = 30,
                }
                );

            PiranhadoDecorator = new WorldDecoratorCollection(
                new GroundLabelDecorator(Hud)
                {
                    CountDownFrom = 4,
                    TextFont = Hud.Render.CreateFont("tahoma", 9, 255, 100, 255, 150, true, false, 128, 0, 0, 0, true),
                },
                new GroundTimerDecorator(Hud)
                {
                    CountDownFrom = 4,
                    BackgroundBrushEmpty = Hud.Render.CreateBrush(128, 0, 0, 0, 0),
                    BackgroundBrushFill = Hud.Render.CreateBrush(160, 100, 255, 150, 0),
                    Radius = 30,
                }
                );

            SpiritWalkDecorator = new WorldDecoratorCollection(
                new GroundLabelDecorator(Hud)
                {
                    CountDownFrom = 2,
                    TextFont = Hud.Render.CreateFont("tahoma", 9, 255, 255, 150, 255, true, false, 128, 0, 0, 0, true),
                },
                new GroundTimerDecorator(Hud)
                {
                    CountDownFrom = 2,
                    BackgroundBrushEmpty = Hud.Render.CreateBrush(128, 0, 0, 0, 0),
                    BackgroundBrushFill = Hud.Render.CreateBrush(160, 255, 150, 255, 0),
                    Radius = 30,
                }
                );

            SpiritWalkWithJauntDecorator = new WorldDecoratorCollection(
                new GroundLabelDecorator(Hud)
                {
                    CountDownFrom = 3,
                    TextFont = Hud.Render.CreateFont("tahoma", 9, 255, 255, 150, 255, true, false, 128, 0, 0, 0, true),
                },
                new GroundTimerDecorator(Hud)
                {
                    CountDownFrom = 3,
                    BackgroundBrushEmpty = Hud.Render.CreateBrush(128, 0, 0, 0, 0),
                    BackgroundBrushFill = Hud.Render.CreateBrush(160, 255, 150, 255, 0),
                    Radius = 30,
                }
                );

            BigBadVoodooDecorator = new WorldDecoratorCollection(
                new GroundLabelDecorator(Hud)
                {
                    CountDownFrom = 20,
                    TextFont = Hud.Render.CreateFont("tahoma", 9, 255, 100, 200, 100, true, false, 128, 0, 0, 0, true),
                },
                new GroundTimerDecorator(Hud)
                {
                    CountDownFrom = 20,
                    BackgroundBrushEmpty = Hud.Render.CreateBrush(128, 0, 0, 0, 0),
                    BackgroundBrushFill = Hud.Render.CreateBrush(160, 100, 200, 100, 0),
                    Radius = 30,
                }
                );

            BigBadVoodooWithJungleDrumsDecorator = new WorldDecoratorCollection(
                new GroundLabelDecorator(Hud)
                {
                    CountDownFrom = 30,
                    TextFont = Hud.Render.CreateFont("tahoma", 9, 255, 100, 200, 100, true, false, 128, 0, 0, 0, true),
                },
                new GroundTimerDecorator(Hud)
                {
                    CountDownFrom = 30,
                    BackgroundBrushEmpty = Hud.Render.CreateBrush(128, 0, 0, 0, 0),
                    BackgroundBrushFill = Hud.Render.CreateBrush(160, 100, 200, 100, 0),
                    Radius = 30,
                }
                );

            InnerSanctuaryDefaultDecorator = new WorldDecoratorCollection(
                new GroundCircleDecorator(Hud)
                {
                    Brush = Hud.Render.CreateBrush(100, 255, 30, 30, 4),
                    Radius = 11,
                },
                new GroundLabelDecorator(Hud)
                {
                    CountDownFrom = 6,
                    TextFont = Hud.Render.CreateFont("tahoma", 9, 255, 100, 255, 150, true, false, 128, 0, 0, 0, true),
                },
                new GroundTimerDecorator(Hud)
                {
                    CountDownFrom = 6,
                    BackgroundBrushEmpty = Hud.Render.CreateBrush(128, 0, 0, 0, 0),
                    BackgroundBrushFill = Hud.Render.CreateBrush(100, 255, 30, 30, 0),
                    Radius = 35,
                }
            );

            InnerSanctuarySanctifiedGroundDecorator = new WorldDecoratorCollection(
                new GroundCircleDecorator(Hud)
                {
                    Brush = Hud.Render.CreateBrush(100, 255, 30, 30, 4),
                    Radius = 11,
                },
                new GroundLabelDecorator(Hud)
                {
                    CountDownFrom = 8,
                    TextFont = Hud.Render.CreateFont("tahoma", 9, 255, 100, 255, 150, true, false, 128, 0, 0, 0, true),
                },
                new GroundTimerDecorator(Hud)
                {
                    CountDownFrom = 8,
                    BackgroundBrushEmpty = Hud.Render.CreateBrush(128, 0, 0, 0, 0),
                    BackgroundBrushFill = Hud.Render.CreateBrush(100, 255, 30, 30, 0),
                    Radius = 35,
                }
            );

            InnerSanctuaryTempleOfProtecteionDecorator = new WorldDecoratorCollection(
                new GroundCircleDecorator(Hud)
                {
                    Brush = Hud.Render.CreateBrush(100, 255, 204, 0, 4),
                    Radius = 11,
                },
                new GroundLabelDecorator(Hud)
                {
                    CountDownFrom = 6,
                    TextFont = Hud.Render.CreateFont("tahoma", 9, 255, 100, 255, 150, true, false, 128, 0, 0, 0, true),
                },
                new GroundTimerDecorator(Hud)
                {
                    CountDownFrom = 6,
                    BackgroundBrushEmpty = Hud.Render.CreateBrush(128, 0, 0, 0, 0),
                    BackgroundBrushFill = Hud.Render.CreateBrush(100, 255, 204, 0, 0),
                    Radius = 35,
                }
            );

            InnerSanctuarySafeHavenDecorator = new WorldDecoratorCollection(
                new GroundCircleDecorator(Hud)
                {
                    Brush = Hud.Render.CreateBrush(100, 255, 26, 179, 4),
                    Radius = 11,
                },
                new GroundLabelDecorator(Hud)
                {
                    CountDownFrom = 6,
                    TextFont = Hud.Render.CreateFont("tahoma", 9, 255, 100, 255, 150, true, false, 128, 0, 0, 0, true),
                },
                new GroundTimerDecorator(Hud)
                {
                    CountDownFrom = 6,
                    BackgroundBrushEmpty = Hud.Render.CreateBrush(128, 0, 0, 0, 0),
                    BackgroundBrushFill = Hud.Render.CreateBrush(100, 255, 26, 179, 0),
                    Radius = 35,
                }
            );
        }

        public void PaintWorld(WorldLayer layer)
        {
            var actors = Hud.Game.Actors;
            foreach (var actor in actors)
            {
                if (actor.SummonerAcdDynamicId == Hud.Game.Me.SummonerId)
                {
                    switch (actor.SnoActor.Sno)
                    {
                        case 81230: // light
                        case 81232: // arcane
                        case 325807:
                        case 83024:
                            HydraDecorator.Paint(layer, actor, actor.FloorCoordinate.Offset(2f, 2f, 0), null);
                            break;
                        case 83959: // mammoth
                            HydraDecorator.Paint(layer, actor, actor.FloorCoordinate, null);
                            break;
                        case 141402:
                        case 150025:
                        case 150024:
                        case 168815:
                        case 150026:
                        case 150027:
                            if (!Hud.Game.Me.Powers.BuffIsActive(208610, 0))
                            {
                                SentryDecorator.Paint(layer, actor, actor.FloorCoordinate, null);
                            }
                            else
                            {
                                SentryWithCustomEngineeringDecorator.Paint(layer, actor, actor.FloorCoordinate, null);
                            }
                            break;
                        case 341426:
                        case 341411:
                        case 341381:
                        case 341396:
                        case 341441:
                            BlackHoleDecorator.Paint(layer, actor, actor.FloorCoordinate.Offset(0, 0, 5.2f), null);
                            break;
                        case 107705:
                        case 106584:
                            {
                                var skill = Hud.Game.Me.Powers.UsedWitchDoctorPowers.SpiritWalk;
                                if (skill != null)
                                {
                                    if (skill.Rune == 1)
                                    {
                                        SpiritWalkWithJauntDecorator.Paint(layer, actor, actor.FloorCoordinate, null);
                                    }
                                    else
                                    {
                                        SpiritWalkDecorator.Paint(layer, actor, actor.FloorCoordinate, null);
                                    }
                                }
                            }
                            break;
                        case 117574:
                        case 182276:
                        case 182278:
                        case 182271:
                        case 182283:
                            {
                                var skill = Hud.Game.Me.Powers.UsedWitchDoctorPowers.BigBadVoodoo;
                                if (skill != null)
                                {
                                    if (skill.Rune == 1)
                                    {
                                        BigBadVoodooWithJungleDrumsDecorator.Paint(layer, actor, actor.FloorCoordinate, null);
                                    }
                                    else
                                    {
                                        BigBadVoodooDecorator.Paint(layer, actor, actor.FloorCoordinate, null);
                                    }
                                }
                            }
                            break;
                    }
                }
                switch (actor.SnoActor.Sno)
                {
                    case 357846:
                        PiranhadoDecorator.Paint(layer, actor, actor.FloorCoordinate, null);
                        break;
                    case 149848:
                        InnerSanctuarySanctifiedGroundDecorator.Paint(layer, actor, actor.FloorCoordinate, null);
                        break;
                    case 320136:
                    case 319583:
                    case 319337:
                        InnerSanctuaryDefaultDecorator.Paint(layer, actor, actor.FloorCoordinate, null);
                        break;
                    case 320135:
                        InnerSanctuarySafeHavenDecorator.Paint(layer, actor, actor.FloorCoordinate, null);
                        break;
                    case 319776:
                        InnerSanctuaryTempleOfProtecteionDecorator.Paint(layer, actor, actor.FloorCoordinate, null);
                        break;
                }
            }
        }

    }

}