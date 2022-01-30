using System.Collections.Generic;
using Oxide.Core.Libraries.Covalence;
using Oxide.Core.Plugins;

namespace Oxide.Ext.Discord.Plugins
{
    internal class DiscordExtensionCore : BaseDiscordPlugin
    {
        #region Setup & Loading
        public DiscordExtensionCore()
        {
            Title = "Discord Extension Core";
        }
        
        [HookMethod("Init")]
        private void Init()
        {
            AddCovalenceCommand(new[] { "de.version" }, nameof(VersionCommand), "de.version");
            AddCovalenceCommand(new[] { "de.reconnectws" }, nameof(ReconnectWebsockets), "de.reconnectws");
            
            foreach (KeyValuePair<string, Dictionary<string, string>> language in Localization.Languages)
            {
                Lang.RegisterMessages(language.Value, this, language.Key);
            }
        }
        #endregion

        #region Commands
        [HookMethod(nameof(VersionCommand))]
        private void VersionCommand(IPlayer player, string cmd, string[] args)
        {
            Chat(player, LangKeys.Version, DiscordExtension.FullExtensionVersion);
        }

        [HookMethod(nameof(ReconnectWebsockets))]
        private void ReconnectWebsockets(IPlayer player, string cmd, string[] args)
        {
            foreach (BotClient client in BotClient.ActiveBots.Values)   
            {
                client.DisconnectWebsocket(true);   
            }
            
            Chat(player, LangKeys.ReconnectWebSocket);
        }
        #endregion
    }
}