namespace Turbo.Plugins
{
    public interface IScene
    {
        ISnoScene SnoScene { get; }
        ISnoArea SnoArea { get; }
        uint WorldSno { get; }
        uint NavMeshId { get; }
        uint SceneId { get; }
        float PosX { get; }
        float PosY { get; }
        float MaxX { get; }
        float MaxY { get; }
        float W { get; }
        float H { get; }
        float Z { get; }
        string CalculatedPosId { get; }
    }
}