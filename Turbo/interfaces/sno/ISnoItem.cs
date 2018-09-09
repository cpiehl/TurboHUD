using System.Collections.Generic;

namespace Turbo.Plugins
{
    public interface ISnoItem
    {
        uint Sno { get; }
        ISnoActor SnoActor { get; }
        ItemKind Kind { get; }
        uint PrefixStringSno { get; }
        uint SuffixStringSno { get; }
        int Level { get; }
        int ItemWidth { get; }
        int ItemHeight { get; }
        string Code { get; }

        string NameLocalized { get; }
        string NameEnglish { get; }

        string MainGroupCode { get; }
        string[] GroupCodes { get; }
        ItemLocation UsedLocation1 { get; }
        ItemLocation UsedLocation2 { get; }
        ISnoItemType SnoItemType { get; }
        int StackSize { get; }
        int GoldPrice { get; }
        int UnsocketPrice { get; }
        int RequiredLevel { get; }
        uint BaseItemSno { get; }
        uint SetItemBonusesSno { get; }
        ItemCraftQuality CraftQuality { get; }
        ISnoItemMod[] Mods { get; }
        ISnoItemAffixGroupLink[] AffixGroups { get; }
        List<ISnoSocketedEffect> SocketedEffects { get; set; }
        bool CanKanaiCube { get; }

        int DropWeight { get; }
        int DropSmartDemonHunter { get; }
        int DropSmartBarbarian { get; }
        int DropSmartWizard { get; }
        int DropSmartWitchDoctor { get; }
        int DropSmartMonk { get; }
        int DropSmartCrusader { get; }

        bool HasGroupCode(string code);

        ISnoPower LegendaryPower { get; }
    }
}