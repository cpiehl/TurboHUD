using System.Collections.Generic;

namespace Turbo.Plugins.Default
{
    public class BuffPaintInfo
    {
        public uint Id { get; set; }
        public ISnoPower SnoPower { get; set; }
        public double Elapsed { get; set; }
        public double TimeLeft { get; set; }
        public int Stacks { get; set; }
        public List<SnoPowerIcon> Icons { get; set; }
        public BuffRule Rule { get; set; }
        public ITexture BackgroundTexture { get; set; }
        public ITexture Texture { get; set; }

        public float Size { get; set; }
        public bool? TimeLeftNumbersOverride { get; set; }
    }
}