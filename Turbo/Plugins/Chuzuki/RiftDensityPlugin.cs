using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Turbo.Plugins.Default;

namespace Turbo.Plugins.Chuzuki
{
    public class RiftDensityPlugin : BasePlugin, IInGameWorldPainter
	{
        private Dictionary<IMonster, float> Clusters { get; set; }
        public WorldDecoratorCollection Decorator { get; set; }
        public MapShapeDecorator MapDecorator { get; set; }
        public GroundCircleDecorator GroundDecorator { get; set; }
        public GroundLabelDecorator LabelDecorator { get; set; }
        public float Range { get; set; }
        public double ProgressMin { get; set; }
        public bool IncludeOffScreen { get; set; }
        public int ClustersMax { get; set; }

        public RiftDensityPlugin()
		{
            Enabled = true;
        }

        public override void Load(IController hud)
		{
            base.Load(hud);

            Clusters = new Dictionary<IMonster, float>();

            Range = 20f;
            ProgressMin = 1d;
            IncludeOffScreen = true;
            ClustersMax = 99;

            Decorator = new WorldDecoratorCollection(GroundDecorator = new GroundCircleDecorator(Hud)
			{
                Brush = Hud.Render.CreateBrush(155, 200, 200, 200, 1),
				Radius = Range
            },
			MapDecorator = new MapShapeDecorator(Hud)
			{
                Brush = Hud.Render.CreateBrush(50, 255, 0, 0, 0),
				ShadowBrush = Hud.Render.CreateBrush(128, 0, 0, 0, 1),
				Radius = Range,
				ShapePainter = new CircleShapePainter(Hud),
            },
			LabelDecorator = new GroundLabelDecorator(Hud)
			{
                BackgroundBrush = Hud.Render.CreateBrush(175, 0, 0, 0, 0),
				TextFont = Hud.Render.CreateFont("tahoma", 12, 255, 255, 255, 255, true, false, true)
            });
        }

        public void PaintWorld(WorldLayer layer)
		{
            if ((Hud.Game.SpecialArea != SpecialArea.Rift) && (Hud.Game.SpecialArea != SpecialArea.GreaterRift) && (Hud.Game.SpecialArea != SpecialArea.ChallengeRift)) return;

            var monsters = Hud.Game.AliveMonsters.Where(x =>(x.SnoMonster != null) && (IncludeOffScreen || x.IsOnScreen) && !((x.SummonerAcdDynamicId != 0) && (x.Rarity == ActorRarity.RareMinion)));

            foreach (var monster in monsters)
			{
                var nearMe = monsters.Where(x => x != monster && x.FloorCoordinate.XYDistanceTo(monster.FloorCoordinate) <= Range);
                Clusters.Add(monster, nearMe.Sum(x => x.SnoMonster.RiftProgression) + monster.SnoMonster.RiftProgression);
            }

            var clusters = 0;

            while (Clusters.Count > 0 && clusters < ClustersMax)
			{
                var cluster = Clusters.Aggregate((l, r) => l.Value > r.Value ? l : r);

                if ((cluster.Value / Hud.Game.MaxQuestProgress * 100d) >= ProgressMin)
				{
                    Decorator.Paint(layer, cluster.Key, cluster.Key.FloorCoordinate, (cluster.Value / Hud.Game.MaxQuestProgress * 100d).ToString("F2", CultureInfo.InvariantCulture) + "%");

                    var nearMeAgain = monsters.Where(x => x.FloorCoordinate.XYDistanceTo(cluster.Key.FloorCoordinate) <= Range * 2f);
                    foreach (var monster in nearMeAgain)
					{
                        Clusters.Remove(monster);
                    }
                    clusters++;
                }
				else
				{
                    clusters = ClustersMax;
                }
            }

            Clusters.Clear();
        }
    }
}
