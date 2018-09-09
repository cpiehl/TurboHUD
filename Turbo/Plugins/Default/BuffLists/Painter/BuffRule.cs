namespace Turbo.Plugins.Default
{

    public class BuffRule
    {
        public uint PowerSno { get; private set; }
        public int? IconIndex { get; set; }
        public int? NoIconIndex { get; set; }
        public int MinimumIconCount { get; set; }
        public bool ShowStacks { get; set; }
        public bool ShowTimeLeft { get; set; }
        public bool UsePowersTexture { get; set; }
        public ISnoItem UseLegendaryItemTexture { get; set; }
        public bool UsePowersName { get; set; }
        public bool UsePowersDesc { get; set; }
        public bool AllowInGameMergeRules { get; set; }
        public bool DisableName { get; set; }
        public float IconSizeMultiplier { get; set; }

        public BuffRule(uint powerSno)
        {
            PowerSno = powerSno;
            MinimumIconCount = 1;
            ShowStacks = false;
            ShowTimeLeft = true;
            UseLegendaryItemTexture = null;
            UsePowersTexture = false;
            UsePowersName = false;
            UsePowersDesc = false;
            AllowInGameMergeRules = true;
            DisableName = false;
            IconSizeMultiplier = 1.0f;
        }
    }

}