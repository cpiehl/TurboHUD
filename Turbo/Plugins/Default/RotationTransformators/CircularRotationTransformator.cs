namespace Turbo.Plugins.Default
{
    public class CircularRotationTransformator : IRotationTransformator
    {
        public IController Hud { get; }
        public int Speed { get; set; }

        public CircularRotationTransformator(IController hud, int speed)
        {
            Hud = hud;
            Speed = speed;
        }

        public float TransformRotation(float angle)
        {
            if (Speed <= 0)
                return angle;

            var msec = Hud.Game.CurrentRealTimeMilliseconds;

            return msec / Speed % 360;
        }
    }
}