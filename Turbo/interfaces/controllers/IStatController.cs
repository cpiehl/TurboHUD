namespace Turbo.Plugins
{
    public interface IStatController
    {
        IPerfCounter RenderPerfCounter { get; }
        IPerfCounter MonsterHitpointDecreasePerfCounter { get; }
    }
}