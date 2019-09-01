namespace Turbo.Plugins.Default
{
    public class BuffRule
    {
        public uint PowerSno { get; }
        public int? IconIndex { get; set; }
        public int? NoIconIndex { get; set; }
        public int MinimumIconCount { get; set; } = 1;
        public bool ShowStacks { get; set; } = false;
        public bool ShowTimeLeft { get; set; } = true;
        public bool UsePowersTexture { get; set; } = false;
        public ISnoItem UseLegendaryItemTexture { get; set; }
        public bool UsePowersName { get; set; } = false;
        public bool UsePowersDesc { get; set; } = false;
        public bool AllowInGameMergeRules { get; set; } = true;
        public bool DisableName { get; set; } = false;
        public float IconSizeMultiplier { get; set; } = 1.0f;

        public BuffRule(uint powerSno)
        {
            PowerSno = powerSno;
        }
    }
}