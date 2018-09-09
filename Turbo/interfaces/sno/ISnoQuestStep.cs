namespace Turbo.Plugins
{
    public interface ISnoQuestStep
    {
        ISnoQuest SnoQuest { get; }
        uint Id { get; }

        string SplashLocalized { get; }
        string SplashEnglish { get; }

        string BnetTitleLocalized { get; }
        string BnetTitleEnglish { get; }

        string BnetTextLocalized { get; }
        string BnetTextEnglish { get; }
    }
}