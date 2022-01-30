using System.Collections.Generic;
namespace Oxide.Ext.Discord.Plugins
{
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