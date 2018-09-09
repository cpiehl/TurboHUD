namespace Turbo.Plugins
{
    public interface ISnoItemAffix
    {
        uint BaseAffixId { get; }
        HeroClass HeroClass { get; }
        uint Id { get; }
        bool Minor { get; set; }
        ISnoItemMod[] Mods { get; set; }

        string NameLocalized { get; }
        string NameEnglish { get; }

        ISnoItemAffixGroup SnoAffixGroup1 { get; }
        ISnoItemAffixGroup SnoAffixGroup2 { get; }
    }
}