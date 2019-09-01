using System.Linq;

namespace Turbo.Plugins.Default
{
    public class ExplosiveMonsterPlugin : BasePlugin, IInGameWorldPainter
    {
        public WorldDecoratorCollection FastMummyDecorator { get; set; }
        public WorldDecoratorCollection GrotesqueDecorator { get; set; }

        public ExplosiveMonsterPlugin()
        {
            Enabled = true;
        }

        public override void Load(IController hud)
        {
            base.Load(hud);

            FastMummyDecorator = new WorldDecoratorCollection(
                new GroundCircleDecorator(Hud)
                {
                    Brush = Hud.Render.CreateBrush(128, 255, 50, 50, 3, SharpDX.Direct2D1.DashStyle.Dash),
                    Radius = 5,
                }
                );

            // timers does not work for grotesque because it has no death actor with creation ticks and the original monster's creation tick is not the same as the time he died
            GrotesqueDecorator = new WorldDecoratorCollection(
                new GroundCircleDecorator(Hud)
                {
                    Brush = Hud.Render.CreateBrush(160, 255, 50, 50, 3, SharpDX.Direct2D1.DashStyle.Dash),
                    Radius = 20f,
                }
                );
        }

        public void PaintWorld(WorldLayer layer)
        {
            foreach (var monster in Hud.Game.Monsters.Where(x => !x.IsAlive))
            {
                switch (monster.SnoActor.Sno)
                {
                    case ActorSnoEnum._fastmummy_a:
                    case ActorSnoEnum._fastmummy_b:
                    case ActorSnoEnum._fastmummy_c:
                        FastMummyDecorator.Paint(layer, monster, monster.FloorCoordinate, monster.SnoMonster.NameLocalized);
                        break;
                    case ActorSnoEnum._corpulent_a:
                    case ActorSnoEnum._corpulent_b:
                    case ActorSnoEnum._corpulent_c:
                    case ActorSnoEnum._corpulent_d:
                    case ActorSnoEnum._corpulent_a_unique_01:
                    case ActorSnoEnum._corpulent_a_unique_02:
                    case ActorSnoEnum._corpulent_a_unique_03:
                    case ActorSnoEnum._corpulent_b_unique_01:
                    case ActorSnoEnum._corpulent_c_oasisambush_unique:
                    case ActorSnoEnum._corpulent_d_cultistsurvivor_unique:
                    case ActorSnoEnum._corpulent_d_unique_spec_01:
                    case ActorSnoEnum._corpulent_frost_a:
                        GrotesqueDecorator.Paint(layer, monster, monster.FloorCoordinate, monster.SnoMonster.NameLocalized);
                        break;
                }
            }
        }
    }
}