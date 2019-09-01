using System;

namespace Turbo.Plugins.Default
{
    public class StandardPingRadiusTransformator : IRadiusTransformator
    {
        public IController Hud { get; }

        public int PingSpeed { get; set; }
        public float RadiusMinimumMultiplier { get; set; }
        public float RadiusMaximumMultiplier { get; set; }

        public StandardPingRadiusTransformator(IController hud, int pingSpeed, float radiusMinimumMultiplier = 0.5f, float radiusMaximumMultiplier = 1.0f)
        {
            Hud = hud;
            PingSpeed = pingSpeed;
            RadiusMinimumMultiplier = radiusMinimumMultiplier;
            RadiusMaximumMultiplier = radiusMaximumMultiplier;
        }

        public float TransformRadius(float radius)
        {
            if (PingSpeed <= 0)
                return radius;

            var msec = Hud.Game.CurrentRealTimeMilliseconds;
            return Math.Floor((double)msec / PingSpeed) % 2 == 1
                ? radius * (RadiusMinimumMultiplier + ((RadiusMaximumMultiplier - RadiusMinimumMultiplier) * (msec % PingSpeed) / PingSpeed))
                : radius * (RadiusMaximumMultiplier - ((RadiusMaximumMultiplier - RadiusMinimumMultiplier) * (msec % PingSpeed) / PingSpeed));
        }
    }
}