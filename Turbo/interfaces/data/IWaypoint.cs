namespace Turbo.Plugins
{
    public interface IWaypoint
    {
        ISnoArea TargetSnoArea { get; }
        BountyAct BountyAct { get; }
        System.Drawing.PointF CoordinateOnMapUiElement { get; }
    }
}