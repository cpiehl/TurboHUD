namespace Turbo.Plugins
{
    public interface ISnoArea
    {
        uint Sno { get; }
        string Code { get; }

        string NameLocalized { get; }
        string NameEnglish { get; }

        AreaType Type { get; }
        bool IsRandom { get; }
        bool IsTown { get; }
        int Act { get; }
        string AreaGroupInWorld { get; }
        ISnoWorld SnoWorld { get; }
        uint HostAreaSno { get; }
        ISnoArea HostSnoArea { get; }
    }
}