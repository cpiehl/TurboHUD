using Turbo.Plugins.Default;

namespace Turbo.Plugins.Chuzuki
{
    public class RiftProgressionLabels : BasePlugin, IInGameWorldPainter
    {
        public DecoratorsHandler Decorators { get; set; }
		public WorldDecoratorCollection CursorDecorator { get; set; }

        public RiftProgressionLabels()
        {
            Enabled = true;
        }

        public override void Load(IController hud)
        {
            base.Load(hud);
            Decorators = new DecoratorsHandler(hud);
        }

        public void PaintWorld(WorldLayer layer)
        {
			if ((Hud.Game.SpecialArea != SpecialArea.Rift) && (Hud.Game.SpecialArea != SpecialArea.GreaterRift) && (Hud.Game.SpecialArea != SpecialArea.ChallengeRift)) return;

			foreach (var monster in Hud.Game.AliveMonsters)
			{
				if (!monster.IsOnScreen || monster.Invisible) continue;
				//~ var rp = (monster.SnoMonster.RiftProgression / Hud.Game.MaxQuestProgress * 100d).ToString("0.000");
				Decorators.Paint(layer, monster);
			}
		}
	}

	public class ProgressionDecorator
	{
		public int Priority { get; set; }
		public IController Hud { get; set; }
		private WorldDecoratorCollection Decorator;
		//~ private Task<bool> ShouldDraw;

		public ProgressionDecorator(IController Hud, int priority, Color foreColor, Color backColor)
		{
			this.Priority = priority;
			//~ this.ShouldDraw = ShouldDraw;

			this.Decorator = new WorldDecoratorCollection
			(
				new GroundLabelDecorator(Hud)
				{
					BackgroundBrush = Hud.Render.CreateBrush(255, backColor.R, backColor.G, backColor.B, 0),
					TextFont = Hud.Render.CreateFont("tahoma", 12.0f, 255, foreColor.R, foreColor.G, foreColor.B, false, false, false),
				}
			);
		}

		public void Paint(WorldLayer layer, IActor actor, string text)
		{
			Decorator.Paint(layer, actor, actor.FloorCoordinate, text);
		}
	}

	public class DecoratorsHandler
	{
		public System.Collections.Generic.Dictionary<string, ProgressionDecorator> Decorators { get; set; }
		private IController Hud { get; set; }
		//~ public delegate bool IsValidDelegate(IActor actor);

		public DecoratorsHandler(IController Hud)
		{
			this.Hud = Hud;
			InitDecorators();
		}

		public void Paint(WorldLayer layer, IMonster monster)
		{
			// rift progression per time to kill
			var rp = monster.SnoMonster.RiftProgression / monster.CurHealth * Hud.Game.Me.Damage.RunDps / Hud.Game.MaxQuestProgress * 1000;
			string text = rp.ToString("0");
			if (monster.IsElite && monster.SummonerAcdDynamicId == 0) // no minions
				Decorators["blue"].Paint(layer, monster, text);
			else if (rp < 25)
				Decorators["red"].Paint(layer, monster, text);
			else if (rp < 100)
				Decorators["orange"].Paint(layer, monster, text);
			else if (rp < 250)
				Decorators["yellow"].Paint(layer, monster, text);
			else
				Decorators["green"].Paint(layer, monster, text);
		}

        private void InitDecorators()
        {
            Decorators = new System.Collections.Generic.Dictionary<string, ProgressionDecorator>();
			Decorators["red"] = new ProgressionDecorator(
				Hud,
				0,
				new Color(255,255,255),
				new Color(255,64,64)
			);
			Decorators["orange"] = new ProgressionDecorator(
				Hud,
				1,
				new Color(255,255,255),
				new Color(255,128,64)
			);
			Decorators["yellow"] = new ProgressionDecorator(
				Hud,
				2,
				new Color(255,255,255),
				new Color(128,255,64)
			);
			Decorators["green"] = new ProgressionDecorator(
				Hud,
				3,
				new Color(255,255,255),
				new Color(64,255,64)
			);
			Decorators["blue"] = new ProgressionDecorator(
				Hud,
				3,
				new Color(255,255,255),
				new Color(64,128,255)
			);
		}
    }
}
