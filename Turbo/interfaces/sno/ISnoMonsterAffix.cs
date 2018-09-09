namespace Turbo.Plugins
{
    public interface ISnoMonsterAffix
    {
        uint Id { get; }
        MonsterAffix Affix { get; }

        string NameLocalized { get; }
        string NameEnglish { get; }
    }
}