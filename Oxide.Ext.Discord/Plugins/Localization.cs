using System.Collections.Generic;
namespace Oxide.Ext.Discord.Plugins
{
    internal static class LangKeys
    {
        public const string Chat = nameof(Chat);
        public const string Version = nameof(Version);
        public const string ReconnectWebSocket = nameof(ReconnectWebSocket);
    }
    
    internal static class Localization
    {
        internal static readonly Dictionary<string, Dictionary<string, string>> Languages = new Dictionary<string, Dictionary<string, string>>
        {
            ["en"] = new Dictionary<string, string>
            {
                [LangKeys.Chat] = "[Discord Extension] {0}",
                [LangKeys.Version] = "Server is running Discord Extension v{0}",
                [LangKeys.ReconnectWebSocket] = "All websockets have been requested to reconnect"
            }
        };
    }
}