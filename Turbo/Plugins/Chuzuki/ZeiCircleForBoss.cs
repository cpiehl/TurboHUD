using System.Linq;
using Turbo.Plugins.Default;

namespace Turbo.Plugins.Chuzuki
{
    public class ZeiCircleForBoss : BasePlugin, IInGameWorldPainter
    {
		public GroundCircleDecorator ZeiDecorator { get; set; }
        public bool OnlyEquipped { get; set; }
        public ZeiCircleForBoss()
        {
            Enabled = true;
            OnlyEquipped = true;
        }
        public override void Load(IController hud)
        {
            base.Load(hud);

            ZeiDecorator = new GroundCircleDecorator(Hud)
            {
                Brush = Hud.Render.CreateBrush(255,192,96,0, 1.5f),
                Radius = 50f
            };
        }

        public void PaintWorld(WorldLayer layer)
        {
			if (Hud.Game.Me.Powers.BuffIsActive(403468, 0) || !OnlyEquipped)
			{
				var monsters = Hud.Game.AliveMonsters.Where(x => x.SnoMonster.Priority == MonsterPriority.boss);
				foreach (var monster in monsters)
				{
					ZeiDecorator.Paint(monster, monster.FloorCoordinate, null);
				}
			}
        }
    }
}
