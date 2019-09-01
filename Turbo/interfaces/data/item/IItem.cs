using System.Collections.Generic;

namespace Turbo.Plugins
{
    public interface IItem : IActor
    {
        int AncientRank { get; }
        int CaldesannRank { get; }
        int JewelRank { get; }

        bool AccountBound { get; } // bound to any account (current or any other player)
        bool BoundToMyAccount { get; } // bound to current account
        bool VendorBought { get; }

        ISnoItem SnoItem { get; }

        string FullNameLocalized { get; } // same as SnoItem.NameLocalized, but includes "Ancient Legendary", "Ancient Set" and other prefixes if necessary
        string FullNameEnglish { get; }

        int InventoryX { get; }
        int InventoryY { get; }
        bool IsNormal { get; }
        bool IsMagic { get; }
        bool IsRare { get; }
        bool IsLegendary { get; }

        uint SetSno { get; }
        int CountsIntoSet(uint setId);

        bool IsInventoryLocked { get; }
        ItemKeepDecision KeepDecision { get; }

        ItemLocation Location { get; }
        bool SeenInInventory { get; }

        double Perfection { get; }
        ItemQuality Quality { get; }
        long Quantity { get; }
        string RareName { get; }
        int Seed { get; }

        int SocketCount { get; }

        IItem SocketedInto { get; }
        ISnoSocketedEffect GetSocketedEffect();

        bool Unidentified { get; }
        ISnoItemAffix[] Affixes { get; }
        IItemPerfection[] Perfections { get; }
        IItem[] ItemsInSocket { get; }

        IEnumerable<IItemStat> StatList { get; }
        uint EnchantedAffixOriginal { get; }
        uint EnchantedAffixNew { get; }
        int EnchantedAffixCounter { get; }
        string ItemUniqueId { get; }
    }
}