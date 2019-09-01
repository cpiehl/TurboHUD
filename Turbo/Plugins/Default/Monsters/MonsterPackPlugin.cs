using System.Collections.Generic;
using System.Linq;

namespace Turbo.Plugins.Default
{
    public class MonsterPackPlugin : BasePlugin, IInGameWorldPainter
    {
        //
        // this plugin is work-in-progress and will be finished in a later release
        //
        // todo: add sidebar
        //

        public IBrush ChampionPackLineBrush { get; set; }
        public IFont ChampionPackNameFont { get; set; }
        public IBrush RarePackLineBrush { get; set; }
        public IFont RarePackNameFont { get; set; }

        public float MiddleHealthBarHeight { get; set; }
        public float MiddleHealthBarWidth { get; set; }
        public IBrush HealthBorder { get; set; }
        public IBrush HealthBackgroundMax { get; set; }
        public IBrush HealthBackgroundChampionRemaining { get; set; }
        public IBrush HealthBackgroundRareRemaining { get; set; }
        public IBrush HealthBackgroundRareMinionRemaining { get; set; }

        public GroundLabelDecorator WeakDecorator { get; set; }
        public Dictionary<MonsterAffix, GroundLabelDecorator> AffixDecorators { get; set; }
        public Dictionary<MonsterAffix, string> CustomAffixNames { get; set; }

        public Dictionary<MonsterAffix, int> Priorities { get; private set; }

        public bool HideOnIllusions { get; set; } = true;

        public MonsterPackPlugin()
        {
            Enabled = false;
            Order = 20000;
        }

        public override void Load(IController hud)
        {
            base.Load(hud);

            Priorities = new Dictionary<MonsterAffix, int>() {
                { MonsterAffix.Juggernaut, 20 },
                { MonsterAffix.Arcane, 10 },
                { MonsterAffix.Desecrator, 10 },
                { MonsterAffix.Frozen, 10 },
                { MonsterAffix.Molten, 9 },
                { MonsterAffix.FrozenPulse, 8 },
                { MonsterAffix.Thunderstorm, 8 },
                { MonsterAffix.Reflect, 7 },
                { MonsterAffix.Electrified, 5 },
                { MonsterAffix.Jailer, 5 },
                { MonsterAffix.Mortar, 5 },
                { MonsterAffix.Orbiter, 5 },
                { MonsterAffix.Plagued, 6 },
                { MonsterAffix.Poison, 5 },
                { MonsterAffix.Waller, 3 },
                { MonsterAffix.FireChains, 1 },
                { MonsterAffix.Shielding, 1 },
                { MonsterAffix.Avenger, 0 },
                { MonsterAffix.ExtraHealth, 0 },
                { MonsterAffix.Fast, 0 },
                { MonsterAffix.HealthLink, 0 },
                { MonsterAffix.Horde, 0 },
                { MonsterAffix.Illusionist, 0 },
                { MonsterAffix.Knockback, 0 },
                { MonsterAffix.MissileDampening, 0 },
                { MonsterAffix.Nightmarish, 0 },
                { MonsterAffix.Teleporter, 0 },
                { MonsterAffix.Vampiric, 0 },
                { MonsterAffix.Vortex, 0 },
                { MonsterAffix.Wormhole, 0 } };

            ChampionPackLineBrush = Hud.Render.CreateBrush(255, 125, 175, 240, -1.5f);
            ChampionPackNameFont = Hud.Render.CreateFont("tahoma", 9.0f, 255, 125, 175, 240, true, false, 255, 0, 0, 0, true);
            RarePackLineBrush = Hud.Render.CreateBrush(255, 240, 175, 125, -1.5f);
            RarePackNameFont = Hud.Render.CreateFont("tahoma", 9.0f, 255, 240, 175, 125, true, false, 255, 0, 0, 0, true);

            MiddleHealthBarWidth = 0.05f;
            MiddleHealthBarHeight = 0.007f;

            HealthBorder = Hud.Render.CreateBrush(255, 0, 0, 0, 1);
            HealthBackgroundMax = Hud.Render.CreateBrush(255, 200, 0, 0, 0);
            HealthBackgroundChampionRemaining = Hud.Render.CreateBrush(255, 125, 175, 240, 0);
            HealthBackgroundRareRemaining = Hud.Render.CreateBrush(255, 240, 175, 125, 0);
            HealthBackgroundRareMinionRemaining = Hud.Render.CreateBrush(255, 180, 180, 180, 0);

            WeakDecorator = new GroundLabelDecorator(Hud)
            {
                BackgroundBrush = Hud.Render.CreateBrush(255, 50, 50, 50, 0),
                BorderBrush = Hud.Render.CreateBrush(128, 0, 0, 0, 2),
                TextFont = Hud.Render.CreateFont("tahoma", 5f, 200, 220, 120, 0, false, false, false),
                ForceOnScreen = false,
                CenterBaseLine = false,
            };

            CustomAffixNames = new Dictionary<MonsterAffix, string>();

            var importantBorderBrush = Hud.Render.CreateBrush(128, 0, 0, 0, 2);
            var importantLabelFont = Hud.Render.CreateFont("tahoma", 6f, 255, 255, 255, 255, true, false, false);

            AffixDecorators = new Dictionary<MonsterAffix, GroundLabelDecorator>
            {
                {
                    MonsterAffix.Arcane,
                    new GroundLabelDecorator(Hud)
                    {
                        BorderBrush = importantBorderBrush,
                        TextFont = importantLabelFont,
                        BackgroundBrush = Hud.Render.CreateBrush(255, 120, 0, 120, 0),
                        ForceOnScreen = false,
                        CenterBaseLine = false,
                    }
                },
                {
                    MonsterAffix.Desecrator,
                    new GroundLabelDecorator(Hud)
                    {
                        BorderBrush = importantBorderBrush,
                        TextFont = importantLabelFont,
                        BackgroundBrush = Hud.Render.CreateBrush(255, 170, 50, 0, 0),
                        ForceOnScreen = false,
                        CenterBaseLine = false,
                    }
                },
                {
                    MonsterAffix.Electrified,
                    new GroundLabelDecorator(Hud)
                    {
                        BorderBrush = importantBorderBrush,
                        TextFont = importantLabelFont,
                        BackgroundBrush = Hud.Render.CreateBrush(255, 40, 40, 240, 0),
                        ForceOnScreen = false,
                        CenterBaseLine = false,
                    }
                },
                {
                    MonsterAffix.Frozen,
                    new GroundLabelDecorator(Hud)
                    {
                        BorderBrush = importantBorderBrush,
                        TextFont = importantLabelFont,
                        BackgroundBrush = Hud.Render.CreateBrush(255, 0, 0, 120, 0),
                        ForceOnScreen = false,
                        CenterBaseLine = false,
                    }
                },
                {
                    MonsterAffix.FrozenPulse,
                    new GroundLabelDecorator(Hud)
                    {
                        BorderBrush = importantBorderBrush,
                        TextFont = importantLabelFont,
                        BackgroundBrush = Hud.Render.CreateBrush(255, 0, 0, 120, 0),
                        ForceOnScreen = false,
                        CenterBaseLine = false,
                    }
                },
                {
                    MonsterAffix.Jailer,
                    new GroundLabelDecorator(Hud)
                    {
                        BorderBrush = importantBorderBrush,
                        TextFont = importantLabelFont,
                        BackgroundBrush = Hud.Render.CreateBrush(255, 120, 0, 120, 0),
                        ForceOnScreen = false,
                        CenterBaseLine = false,
                    }
                },
                {
                    MonsterAffix.Juggernaut,
                    new GroundLabelDecorator(Hud)
                    {
                        BorderBrush = importantBorderBrush,
                        TextFont = importantLabelFont,
                        BackgroundBrush = Hud.Render.CreateBrush(255, 200, 0, 0, 0),
                        ForceOnScreen = false,
                        CenterBaseLine = false,
                    }
                },
                {
                    MonsterAffix.Molten,
                    new GroundLabelDecorator(Hud)
                    {
                        BorderBrush = importantBorderBrush,
                        TextFont = importantLabelFont,
                        BackgroundBrush = Hud.Render.CreateBrush(255, 170, 50, 0, 0),
                        ForceOnScreen = false,
                        CenterBaseLine = false,
                    }
                },
                {
                    MonsterAffix.Mortar,
                    new GroundLabelDecorator(Hud)
                    {
                        BorderBrush = importantBorderBrush,
                        TextFont = importantLabelFont,
                        BackgroundBrush = Hud.Render.CreateBrush(255, 170, 50, 0, 0),
                        ForceOnScreen = false,
                        CenterBaseLine = false,
                    }
                },
                {
                    MonsterAffix.Orbiter,
                    new GroundLabelDecorator(Hud)
                    {
                        BorderBrush = importantBorderBrush,
                        TextFont = importantLabelFont,
                        BackgroundBrush = Hud.Render.CreateBrush(255, 40, 40, 240, 0),
                        ForceOnScreen = false,
                        CenterBaseLine = false,
                    }
                },
                {
                    MonsterAffix.Plagued,
                    new GroundLabelDecorator(Hud)
                    {
                        BorderBrush = importantBorderBrush,
                        TextFont = importantLabelFont,
                        BackgroundBrush = Hud.Render.CreateBrush(255, 0, 120, 0, 0),
                        ForceOnScreen = false,
                        CenterBaseLine = false,
                    }
                },
                {
                    MonsterAffix.Poison,
                    new GroundLabelDecorator(Hud)
                    {
                        BorderBrush = importantBorderBrush,
                        TextFont = importantLabelFont,
                        BackgroundBrush = Hud.Render.CreateBrush(255, 0, 120, 0, 0),
                        ForceOnScreen = false,
                        CenterBaseLine = false,
                    }
                },
                {
                    MonsterAffix.Reflect,
                    new GroundLabelDecorator(Hud)
                    {
                        BorderBrush = importantBorderBrush,
                        TextFont = importantLabelFont,
                        BackgroundBrush = Hud.Render.CreateBrush(255, 120, 50, 0, 0),
                        ForceOnScreen = false,
                        CenterBaseLine = false,
                    }
                },
                {
                    MonsterAffix.Thunderstorm,
                    new GroundLabelDecorator(Hud)
                    {
                        BorderBrush = importantBorderBrush,
                        TextFont = importantLabelFont,
                        BackgroundBrush = Hud.Render.CreateBrush(255, 40, 40, 240, 0),
                        ForceOnScreen = false,
                        CenterBaseLine = false,
                    }
                },
                {
                    MonsterAffix.Waller,
                    new GroundLabelDecorator(Hud)
                    {
                        BorderBrush = importantBorderBrush,
                        TextFont = importantLabelFont,
                        BackgroundBrush = Hud.Render.CreateBrush(255, 50, 50, 50, 0),
                        ForceOnScreen = false,
                        CenterBaseLine = false,
                    }
                },

                { MonsterAffix.ExtraHealth, WeakDecorator },
                { MonsterAffix.HealthLink, WeakDecorator },
                { MonsterAffix.Fast, WeakDecorator },
                { MonsterAffix.FireChains, WeakDecorator },
                { MonsterAffix.Knockback, WeakDecorator },
                { MonsterAffix.Nightmarish, WeakDecorator },
                { MonsterAffix.Illusionist, WeakDecorator },
                { MonsterAffix.Shielding, WeakDecorator },
                { MonsterAffix.Teleporter, WeakDecorator },
                { MonsterAffix.Vampiric, WeakDecorator },
                { MonsterAffix.Vortex, WeakDecorator },
                { MonsterAffix.Wormhole, WeakDecorator },
                { MonsterAffix.Avenger, WeakDecorator },
                { MonsterAffix.Horde, WeakDecorator },
                { MonsterAffix.MissileDampening, WeakDecorator }
            };
        }

        public void PaintWorld(WorldLayer layer)
        {
            if (layer != WorldLayer.Ground)
                return;

            var packs = Hud.Game.MonsterPacks;
            if (!packs.Any())
                return;

            var padding = 6.0f * Hud.Window.Size.Height / 1200f;

            var maxHealth = packs.Max(pack => pack.MonstersAlive.Any() ? pack.MonstersAlive.Max(monster => monster.MaxHealth) : 0);
            var middleBarHeight = Hud.Window.Size.Height * MiddleHealthBarHeight;
            var middleBarWidth = Hud.Window.Size.Width * MiddleHealthBarWidth;

            foreach (var pack in packs)
            {
                var alive = !HideOnIllusions ? pack.MonstersAlive.ToList() : pack.MonstersAlive.Where(x => (x.SummonerAcdDynamicId == 0) || (x.Rarity != ActorRarity.RareMinion)).ToList();
                if (alive.Count == 0)
                    continue;

                alive.Sort((a, b) =>
                {
                    var r = a.Rarity.CompareTo(b.Rarity);
                    if (r == 0)
                        r = -a.MaxHealth.CompareTo(b.MaxHealth);
                    return r;
                });

                var center = Hud.Window.CreateWorldCoordinate(0, 0, 0);

                var n = 0;
                if (alive.Any(x => x.FloorCoordinate.IsOnScreen()))
                {
                    foreach (var monster in alive.Where(x => x.FloorCoordinate.IsOnScreen()))
                    {
                        center.Add(monster.FloorCoordinate);
                        n++;
                    }
                }
                else
                {
                    foreach (var monster in alive)
                    {
                        center.Add(monster.FloorCoordinate);
                        n++;
                    }
                }

                center.Set(center.X / n, center.Y / n, center.Z / n);

                var centerScreenCoordinate = center.ToScreenCoordinate(false);

                var y = centerScreenCoordinate.Y;

                if (pack.IsFullChampionPack)
                {
                    if (ChampionPackLineBrush != null && (alive.Count > 1))
                    {
                        PaintFloorLines(alive, ChampionPackLineBrush);
                    }

                    if (ChampionPackNameFont != null)
                    {
                        var layout = ChampionPackNameFont.GetTextLayout(pack.LeadSnoMonster.NameLocalized);
                        ChampionPackNameFont.DrawText(layout, centerScreenCoordinate.X - (layout.Metrics.Width / 2), y);
                        y += layout.Metrics.Height + padding;
                    }
                }
                else
                {
                    if (RarePackLineBrush != null && (alive.Count > 1))
                    {
                        PaintFloorLines(alive, RarePackLineBrush);
                    }

                    if (RarePackNameFont != null)
                    {
                        var layout = RarePackNameFont.GetTextLayout(pack.LeadSnoMonster.NameLocalized);
                        RarePackNameFont.DrawText(layout, centerScreenCoordinate.X - (layout.Metrics.Width / 2), y);
                        y += layout.Metrics.Height + padding;
                    }
                }

                var decoHeight = 0.0f;

                var snoMonsterAffixList = pack.AffixSnoList.ToList();
                snoMonsterAffixList.Sort((a, b) => -Priorities[a.Affix].CompareTo(Priorities[b.Affix]));

                foreach (var snoMonsterAffix in snoMonsterAffixList)
                {
                    if (!AffixDecorators.TryGetValue(snoMonsterAffix.Affix, out var decorator))
                        continue;

                    var affixName = CustomAffixNames.ContainsKey(snoMonsterAffix.Affix) ? CustomAffixNames[snoMonsterAffix.Affix] : snoMonsterAffix.NameLocalized;

                    if (decoHeight == 0.0f)
                    {
                        decoHeight = decorator.TextFont.GetTextLayout(affixName).Metrics.Height * 1.2f;
                    }

                    decorator.OffsetY = y - centerScreenCoordinate.Y;

                    decorator.Paint(null, center, affixName);
                }

                y += decoHeight + (padding / 2);

                foreach (var monster in alive)
                {
                    var curW = (float)(middleBarWidth / maxHealth * monster.CurHealth);
                    var maxW = (float)(middleBarWidth / maxHealth * monster.MaxHealth);

                    HealthBackgroundMax.DrawRectangleGridFit(centerScreenCoordinate.X - (maxW / 2), y, maxW, middleBarHeight);
                    switch (monster.Rarity)
                    {
                        case ActorRarity.Champion:
                            HealthBackgroundChampionRemaining.DrawRectangleGridFit(centerScreenCoordinate.X - (maxW / 2), y, curW, middleBarHeight);
                            break;
                        case ActorRarity.Rare:
                            HealthBackgroundRareRemaining.DrawRectangleGridFit(centerScreenCoordinate.X - (maxW / 2), y, curW, middleBarHeight);
                            break;
                        case ActorRarity.RareMinion:
                            HealthBackgroundRareMinionRemaining.DrawRectangleGridFit(centerScreenCoordinate.X - (maxW / 2), y, curW, middleBarHeight);
                            break;
                    }

                    HealthBorder.DrawRectangleGridFit(centerScreenCoordinate.X - (maxW / 2), y, maxW, middleBarHeight);

                    y += middleBarHeight;
                }
            }
        }

        private void PaintFloorLines(IEnumerable<IMonster> monsters, IBrush brush)
        {
            var list = new List<IMonster>(monsters);
            IMonster nearest = null;
            foreach (var monster in list)
            {
                if (nearest == null || monster.CentralXyDistanceToMe < nearest.CentralXyDistanceToMe)
                    nearest = monster;
            }

            list.Remove(nearest);
            while (list.Count > 0)
            {
                IMonster nextNearest = null;
                foreach (var monster in list)
                {
                    if (nextNearest == null || monster.FloorCoordinate.XYDistanceTo(nearest.FloorCoordinate) < nextNearest.FloorCoordinate.XYDistanceTo(nearest.FloorCoordinate))
                        nextNearest = monster;
                }

                brush.DrawLineWorld(nearest.FloorCoordinate, nextNearest.FloorCoordinate);
                nearest = nextNearest;
                list.Remove(nextNearest);
            }
        }
    }
}