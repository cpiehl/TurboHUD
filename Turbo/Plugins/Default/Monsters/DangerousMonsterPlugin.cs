using System.Collections.Generic;

namespace Turbo.Plugins.Default
{

    public class DangerousMonsterPlugin : BasePlugin, IInGameWorldPainter
	{

        public WorldDecoratorCollection Decorator { get; set; }
        private Dictionary<string, string> _names = new Dictionary<string, string>();

        public DangerousMonsterPlugin()
		{
            Enabled = true;
		}

        public override void Load(IController hud)
        {
            base.Load(hud);

            AddNames("Wood Wraith", "Highland Walker", "The Old Man", "Fallen Lunatic", "Deranged Fallen", "Fallen Maniac", "Frenzied Lunatic", "Herald of Pestilence", "Terror Demon", "Demented Fallen", "Savage Beast", "Tusked Bogan", "Punisher", "Anarch", "Corrupted Angel", "Winged Assassin", "Exarch");

            Decorator = new WorldDecoratorCollection(
                new MapShapeDecorator(Hud)
                {
                    Brush = Hud.Render.CreateBrush(180, 255, 50, 50, 0),
                    ShadowBrush = Hud.Render.CreateBrush(96, 0, 0, 0, 1),
                    ShapePainter = new CircleShapePainter(Hud),
                    Radius = 2,
                },
                new GroundLabelDecorator(Hud)
                {
                    BackgroundBrush = Hud.Render.CreateBrush(255, 200, 50, 50, 0),
                    TextFont = Hud.Render.CreateFont("tahoma", 6.5f, 255, 255, 255, 255, false, false, false),
                }
                );
        }

        public void AddNames(params string[] names)
        {
            foreach (var name in names)
            {
                _names[name] = name;
            }
        }

        public void RemoveName(string name)
        {
            if (_names.ContainsKey(name)) _names.Remove(name);
        }

        public void PaintWorld(WorldLayer layer)
        {
            var monsters = Hud.Game.AliveMonsters;
            foreach (var monster in monsters)
            {
                if (_names.ContainsKey(monster.SnoMonster.NameEnglish) || _names.ContainsKey(monster.SnoMonster.NameLocalized))
                {
                    Decorator.Paint(layer, monster, monster.FloorCoordinate, monster.SnoMonster.NameLocalized);
                }
            }
        }

    }

}