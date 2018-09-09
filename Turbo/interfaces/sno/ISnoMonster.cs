namespace Turbo.Plugins
{
    public interface ISnoMonster
    {
        ISnoActor SnoActor { get; }
        uint Sno { get; }
        string Code { get; }

        string NameLocalized { get; }
        string NameEnglish { get; }

        bool IsUnique { get; }
        MonsterPriority Priority { get; }
        float RiftProgression { get; }
    }
}