using System.Collections.Generic;
using System.Linq;

namespace Turbo.Plugins.Default
{
    public class CosmeticItemsPlugin : BasePlugin, IInGameWorldPainter
    {

        public WorldDecoratorCollection Decorator { get; set; }
        public string DisplayTextOnActors { get; set; }
        public string DisplayTextOnMonsters { get; set; }
        public string DisplayTextOnItems { get; set; }

        private HashSet<uint> _monsterSnoList = new HashSet<uint>();
        private HashSet<uint> _actorSnoList = new HashSet<uint>();

        public CosmeticItemsPlugin()
        {
            Enabled = true;
            DisplayTextOnActors = "cosmetics";
            DisplayTextOnMonsters = "cosmetics";
            DisplayTextOnItems = "cosmetics";
        }

        public override void Load(IController hud)
        {
            base.Load(hud);

            Decorator = new WorldDecoratorCollection(
                new MapShapeDecorator(Hud)
                {
                    ShapePainter = new RotatingTriangleShapePainter(Hud),
                    Brush = Hud.Render.CreateBrush(160, 255, 128, 0, 10),
                    ShadowBrush = Hud.Render.CreateBrush(96, 0, 0, 0, 1),
                    Radius = 2,
                },
                new GroundCircleDecorator(Hud)
                {
                    Brush = Hud.Render.CreateBrush(150, 255, 128, 0, 0),
                    Radius = 1.125f,
                },
                new GroundLabelDecorator(Hud)
                {
                    BackgroundBrush = Hud.Render.CreateBrush(90, 0, 0, 0, 0),
                    TextFont = Hud.Render.CreateFont("tahoma", 8, 255, 255, 128, 0, true, false, 100, 0, 0, 0, true),
                }
                );

            _monsterSnoList.Add(214948); // Princess Stardust
            _monsterSnoList.Add(218804); // Creampuff
            _monsterSnoList.Add(225114); // Jay Wilson
            _monsterSnoList.Add(316439); // Josh Mosqueira
            _monsterSnoList.Add(373833); // Super Awesome Sparkle Cake
            _monsterSnoList.Add(444994); // The Succulent
            _monsterSnoList.Add(450997); // Regreb the Slayer
            _monsterSnoList.Add(450999); // Princess Lilian
            _monsterSnoList.Add(451002); // Sir William
            _monsterSnoList.Add(451004); // Graw the Herald
            _monsterSnoList.Add(451011); // Nevaz
            _monsterSnoList.Add(451121); // Ravi Lilywhite
            _monsterSnoList.Add(450993); // Menagerist Goblin
            _monsterSnoList.Add(217744); // Nine Toads
            _monsterSnoList.Add(156738); // Moontooth Dreadshark
            _monsterSnoList.Add(178619); // Urzel Mordreg

            _actorSnoList.Add(207706); // Mysterious Chest
            _actorSnoList.Add(451035); // Mysterious Chest
            _actorSnoList.Add(451028); // Mysterious Chest
            _actorSnoList.Add(451030); // Mysterious Chest
            _actorSnoList.Add(451047); // Mysterious Chest
            _actorSnoList.Add(451029); // Mysterious Chest
            _actorSnoList.Add(451038); // Mysterious Chest
            _actorSnoList.Add(451034); // Mysterious Chest
            _actorSnoList.Add(451033); // Mysterious Chest
            _actorSnoList.Add(451027); // Mysterious Chest
            _actorSnoList.Add(451039); // Mysterious Barrel
            _actorSnoList.Add(211861); // Pinata
            _actorSnoList.Add(457828); // Wirt's Stash
            _actorSnoList.Add(225782); // Bishibosh's Remains
            _actorSnoList.Add(173325); // Anvil of Fury
            _actorSnoList.Add(113845); // Fallen Shrine
        }

        private bool IsCosmetic(IItem item)
        {
            return item.SnoItem.HasGroupCode("cosmetic") || item.SnoItem.HasGroupCode("cosmetic_pet") || item.SnoItem.HasGroupCode("cosmetic_portrait_frame") || item.SnoItem.HasGroupCode("cosmetic_wing") || item.SnoItem.HasGroupCode("cosmetic_pennant");
        }

        public void PaintWorld(WorldLayer layer)
        {
            var actors = Hud.Game.Actors.Where(x => !x.IsDisabled && !x.IsOperated && _actorSnoList.Contains(x.SnoActor.Sno));
            foreach (var actor in actors)
            {
                Decorator.Paint(layer, actor, actor.FloorCoordinate, DisplayTextOnActors);
            }

            var monsters = Hud.Game.AliveMonsters.Where(x => _monsterSnoList.Contains(x.SnoActor.Sno));
            foreach (var monster in monsters)
            {
                Decorator.Paint(layer, monster, monster.FloorCoordinate, DisplayTextOnMonsters);
            }

            var items = Hud.Game.Items.Where(x => x.Location == ItemLocation.Floor && IsCosmetic(x));
            foreach (var item in items)
            {
                Decorator.Paint(layer, item, item.FloorCoordinate, DisplayTextOnItems);
            }
        }
    }

}