namespace Turbo.Plugins
{
    public interface ISnoItemAffixGroupLink
    {
        ISnoItemAffixGroup Group { get; }
        int Level { get; }
        int Flag { get; }
    }
}