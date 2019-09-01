namespace Turbo.Plugins
{
    public interface ISnoItemType
    {
        uint Id { get; }
        string Code { get; }

        string NameLocalized { get; }
        string NameEnglish { get; }

        ISnoItemType ParentSnoType { get; }
    }
}