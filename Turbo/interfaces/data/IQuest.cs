namespace Turbo.Plugins
{
    public interface IQuest
    {
        ISnoQuest SnoQuest { get; }
        int CreatedOn { get; }
        uint QuestStepId { get; }
        ISnoQuestStep QuestStep { get; }
        QuestState State { get; }
        IWatch CompletedOn { get; }
        IWatch StartedOn { get; }
        float Progress { get; }
        int Counter { get; }

        bool IsFinished(uint stepId);
    }
}