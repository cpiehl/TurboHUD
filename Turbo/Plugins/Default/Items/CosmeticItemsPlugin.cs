using System.Collections.Generic;
using System.Linq;

namespace Turbo.Plugins.Default
{
    public class CosmeticItemsPlugin : BasePlugin, IInGameWorldPainter
    {
        public WorldDecoratorCollection Decorator { get; set; }
        public string DisplayTextOnActors { get; set; } = "cosmetics";
        public string DisplayTextOnMonsters { get; set; } = "cosmetics";
        public string DisplayTextOnItems { get; set; } = "cosmetics";

        private readonly HashSet<ActorSnoEnum> _monsterSnoList = new HashSet<ActorSnoEnum>();
        private readonly HashSet<ActorSnoEnum> _actorSnoList = new HashSet<ActorSnoEnum>();

        public CosmeticItemsPlugin()
        {
            Enabled = true;
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

            _monsterSnoList.Add(ActorSnoEnum._tentaclehorse_c_unique_01); // Princess Stardust
            _monsterSnoList.Add(ActorSnoEnum._tentaclehorse_fat_a_unique_01); // Creampuff
            _monsterSnoList.Add(ActorSnoEnum._unburied_a_tmunique_01); // Jay Wilson
            _monsterSnoList.Add(ActorSnoEnum._triunevesselactivated_a_tmunique_01); // Josh Mosqueira
            _monsterSnoList.Add(ActorSnoEnum._tentaclehorse_fat_unique_b); // Super Awesome Sparkle Cake
            _monsterSnoList.Add(ActorSnoEnum._p4_crab_mother_unique_01); // The Succulent
            _monsterSnoList.Add(ActorSnoEnum._fallenchampion_a_unique_cosmetic_02); // Regreb the Slayer
            _monsterSnoList.Add(ActorSnoEnum._tentaclehorse_c_unique_cosmetic_02); // Princess Lilian
            _monsterSnoList.Add(ActorSnoEnum._tentaclebear_c_unique_cosmetic_02); // Sir William
            _monsterSnoList.Add(ActorSnoEnum._fallenshaman_a_cosmetic_unique_01); // Graw the Herald
            _monsterSnoList.Add(ActorSnoEnum._x1_triunesummoner_c_unique_cosmetic_01); // Nevaz
            _monsterSnoList.Add(ActorSnoEnum._zombieskinny_b_unique_313); // Ravi Lilywhite
            _monsterSnoList.Add(ActorSnoEnum._treasuregoblin_k); // Menagerist Goblin
            _monsterSnoList.Add(ActorSnoEnum._fastmummy_c_unique); // Nine Toads
            _monsterSnoList.Add(ActorSnoEnum._sandshark_b_sewersharkevent); // Moontooth Dreadshark
            _monsterSnoList.Add(ActorSnoEnum._townattack_summoner_unique); // Urzel Mordreg

            _actorSnoList.Add(ActorSnoEnum._caout_oasis_chest_rare_mapvendorcave); // Mysterious Chest
            _actorSnoList.Add(ActorSnoEnum._a1dun_cath_chest_cosmetic_01); // Mysterious Chest
            _actorSnoList.Add(ActorSnoEnum._a3dun_crater_chest_cosmetic_01); // Mysterious Chest
            _actorSnoList.Add(ActorSnoEnum._a4dun_garden_chest_cosmetic_01); // Mysterious Chest
            _actorSnoList.Add(ActorSnoEnum._a4dun_garden_chest_cosmetic_02); // Mysterious Chest
            _actorSnoList.Add(ActorSnoEnum._p4_forest_snow_chest_snowy_cosmetic_01); // Mysterious Chest
            _actorSnoList.Add(ActorSnoEnum._p4_ruins_frost_chest_rare_cosmetic_01); // Mysterious Chest
            _actorSnoList.Add(ActorSnoEnum._trout_fields_chest_cosmetic_01); // Mysterious Chest
            _actorSnoList.Add(ActorSnoEnum._x1_bog_chest_cosmetic_01); // Mysterious Chest
            _actorSnoList.Add(ActorSnoEnum._x1_westm_chest_cosmetic_01); // Mysterious Chest
            _actorSnoList.Add(ActorSnoEnum._a3dun_keep_barrel_b_breakable_cosmetic_01); // Mysterious Barrel
            _actorSnoList.Add(ActorSnoEnum._pinata); // Pinata
            _actorSnoList.Add(ActorSnoEnum._trout_highlands_chest_wirt); // Wirt's Stash
            _actorSnoList.Add(ActorSnoEnum._trout_tristramfields_denofevil_fallenshaman_special); // Bishibosh's Remains
            _actorSnoList.Add(ActorSnoEnum._trout_oldtristram_anviloffury); // Anvil of Fury
            _actorSnoList.Add(ActorSnoEnum._caout_oasis_rakanishu_centerstone_a); // Fallen Shrine
        }

        private bool IsCosmetic(IItem item)
        {
            return item.SnoItem.HasGroupCode("cosmetic") || item.SnoItem.HasGroupCode("cosmetic_pet") || item.SnoItem.HasGroupCode("cosmetic_portrait_frame") || item.SnoItem.HasGroupCode("cosmetic_wing") || item.SnoItem.HasGroupCode("cosmetic_pennant");
        }

        public void PaintWorld(WorldLayer layer)
        {
            foreach (var actor in Hud.Game.Actors.Where(x => !x.IsDisabled && !x.IsOperated && _actorSnoList.Contains(x.SnoActor.Sno)))
            {
                Decorator.Paint(layer, actor, actor.FloorCoordinate, DisplayTextOnActors);
            }

            foreach (var monster in Hud.Game.AliveMonsters.Where(x => _monsterSnoList.Contains(x.SnoActor.Sno)))
            {
                Decorator.Paint(layer, monster, monster.FloorCoordinate, DisplayTextOnMonsters);
            }

            foreach (var item in Hud.Game.Items.Where(x => x.Location == ItemLocation.Floor && IsCosmetic(x)))
            {
                Decorator.Paint(layer, item, item.FloorCoordinate, DisplayTextOnItems);
            }
        }
    }
}