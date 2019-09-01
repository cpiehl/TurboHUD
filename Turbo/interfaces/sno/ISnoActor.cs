namespace Turbo.Plugins
{
    public interface ISnoActor
    {
        ActorSnoEnum Sno { get; }
        ActorKind Kind { get; }
        ActorType Type { get; }
        string Code { get; }

        string NameLocalized { get; }
        string NameEnglish { get; }

        bool IsStatic { get; }
        uint[] TextureMap { get; }
    }
}