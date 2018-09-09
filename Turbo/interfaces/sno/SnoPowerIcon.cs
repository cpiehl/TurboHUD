namespace Turbo.Plugins
{
    public struct SnoPowerIcon
    {
        public bool Exists;

        public string TitleLocalized;
        public string DescriptionLocalized;

        public uint TextureId;
        public bool IsHidden;
        public bool ShowDuration;
        public bool UserCanCancel;
        public bool MergesTooltip;
        public uint MergesTooltipIndex;
        public bool Harmful;
        public bool ShowActiveOnSkillButton;
        public bool ShowDurationOnSkillButton;
        public bool ShowInBuffHolder;
    }
}