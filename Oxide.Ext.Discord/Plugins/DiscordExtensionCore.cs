using System;
using System.Collections.Generic;
using Oxide.Core.Libraries.Covalence;
using Oxide.Core.Plugins;
using Oxide.Ext.Discord.Logging;

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
            AddCovalenceCommand(new[] { "de.rws" }, nameof(ReconnectWebsockets), "de.rws");
            AddCovalenceCommand(new[] { "de.consolelog" }, nameof(ConsoleLog), "de.consolelog");
            AddCovalenceCommand(new[] { "de.filelog" }, nameof(FileLog), "de.filelog");
            
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
        
        [HookMethod(nameof(ConsoleLog))]
        private void ConsoleLog(IPlayer player, string cmd, string[] args)
        {
            if (args.Length == 0)
            {
                Chat(player, LangKeys.ShowLog, "Console", DiscordExtension.DiscordConfig.Logging.ConsoleLogLevel);
                return;
            }

            try
            {
                DiscordLogLevel log = (DiscordLogLevel)Enum.Parse(typeof(DiscordLogLevel), args[0], true);
                DiscordExtension.DiscordConfig.Logging.ConsoleLogLevel = log;
                DiscordExtension.DiscordConfig.Save();

                Chat(player, LangKeys.SetLog, "Console", log);
            }
            catch
            {
                Chat(player, LangKeys.InvalidLogEnum, args[0]);
            }
        }
        
        [HookMethod(nameof(FileLog))]
        private void FileLog(IPlayer player, string cmd, string[] args)
        {
            if (args.Length == 0)
            {
                Chat(player, LangKeys.ShowLog, "File", DiscordExtension.DiscordConfig.Logging.FileLogLevel);
                return;
            }
            
            try
            {
                DiscordLogLevel log = (DiscordLogLevel)Enum.Parse(typeof(DiscordLogLevel), args[0], true);
                DiscordExtension.DiscordConfig.Logging.FileLogLevel = log;
                DiscordExtension.DiscordConfig.Save();

                Chat(player, LangKeys.SetLog, "Console", log);
            }
            catch
            {
                Chat(player, LangKeys.InvalidLogEnum, args[0]);
            }
        }
        #endregion
    }
}