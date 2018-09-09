namespace Turbo.Plugins
{
    public interface IItemKeepDecisionHandler : IPlugin
    {
        /// <summary>
        /// Called when an item's <see cref="ItemKeepDecision"/> is calculated.
        /// </summary>
        ItemKeepDecision CalculateItemKeepDecision(IItem item, ItemKeepDecision previousDecision);
    }
}