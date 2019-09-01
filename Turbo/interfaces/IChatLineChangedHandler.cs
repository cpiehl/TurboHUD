namespace Turbo.Plugins
{
    public interface IChatLineChangedHandler : IPlugin
    {
        /// <summary>
        /// Called when the latest chat line is changed. Ingame color codes are removed from the values automatically.
        /// </summary>
        void OnChatLineChanged(string currentLine, string previousLine);
    }
}