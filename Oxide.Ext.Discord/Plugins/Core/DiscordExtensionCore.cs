using System;
using System.Collections.Generic;
using Oxide.Core.Libraries.Covalence;
using Oxide.Core.Plugins;
using Oxide.Ext.Discord.Cache;
using Oxide.Ext.Discord.Configuration;
using Oxide.Ext.Discord.Data.Users;
using Oxide.Ext.Discord.Extensions;
using Oxide.Ext.Discord.Factory;
using Oxide.Ext.Discord.Libraries.AppCommands;
using Oxide.Ext.Discord.Libraries.Command;
using Oxide.Ext.Discord.Libraries.Placeholders;
using Oxide.Ext.Discord.Libraries.Pooling;
using Oxide.Ext.Discord.Libraries.Subscription;
using Oxide.Ext.Discord.Logging;

namespace Oxide.Ext.Discord.Plugins.Core
{
    internal partial class DiscordExtensionCore : BaseDiscordPlugin
    {
        #region Fields
        public static DiscordExtensionCore Instance;
        private ILogger _logger;

        internal bool IsServerLoaded;
        #endregion
        
        #region Setup & Loading
        public DiscordExtensionCore()
        {
            Name = "DiscordExtension";
            Title = "Discord Extension";
        }
        
        // ReSharper disable once UnusedMember.Local
        [HookMethod(nameof(Init))]
        private void Init()
        {
            Instance = this;
            _logger = DiscordLoggerFactory.Instance.CreateExtensionLogger(DiscordLogLevel.Info);
            DiscordPool.Instance.CreateInternal(this);
            AddCovalenceCommand(new[] { "de.version" }, nameof(VersionCommand), "de.version");
            AddCovalenceCommand(new[] { "de.websocket.reset" }, nameof(ResetWebSocketCommand), "de.websocket.reset");
            AddCovalenceCommand(new[] { "de.websocket.reconnect" }, nameof(ReconnectWebSocketCommand), "de.websocket.reconnect");
            AddCovalenceCommand(new[] { "de.rest.reset" }, nameof(ResetRestApiCommand), "de.rest.reset");
            AddCovalenceCommand(new[] { "de.clearpool" }, nameof(ClearDiscordPool), "de.clearpool");
            AddCovalenceCommand(new[] { "de.wipepool" }, nameof(WipeDiscordPool), "de.wipepool");
            AddCovalenceCommand(new[] { "de.consolelog" }, nameof(ConsoleLogCommand), "de.consolelog");
            AddCovalenceCommand(new[] { "de.filelog" }, nameof(FileLogCommand), "de.filelog");
            AddCovalenceCommand(new[] { "de.debug" }, nameof(DiscordDebugCommand), "de.debug");
            
            foreach (KeyValuePair<string, Dictionary<string, string>> language in Localization.Languages)
            {
                Lang.RegisterMessages(language.Value, this, language.Key);
            }

            CreateTemplates();
            DiscordPlaceholders.Instance.RegisterPlaceholders();
        }

        // ReSharper disable once UnusedMember.Local
        [HookMethod(nameof(OnServerInitialized))]
        private void OnServerInitialized()
        {
            IsServerLoaded = true;
        }

        // ReSharper disable once UnusedMember.Local
        [HookMethod(nameof(OnServerSave))]
        private void OnServerSave()
        {
            DiscordUserData.Instance.Save(false);
        }
        
        // ReSharper disable once UnusedMember.Local
        [HookMethod(nameof(OnServerShutdown))]
        private void OnServerShutdown()
        {
            DiscordExtension.IsShuttingDown = true;
        }

        // ReSharper disable once UnusedMember.Local
        [HookMethod(nameof(Unload))]
        private void Unload()
        {
            Instance = null;
        }
        #endregion

        #region Commands
        [HookMethod(nameof(VersionCommand))]
        private void VersionCommand(IPlayer player, string cmd, string[] args)
        {
            Chat(player, LangKeys.Version, DiscordExtension.FullExtensionVersion);
        }

        [HookMethod(nameof(ResetWebSocketCommand))]
        private void ResetWebSocketCommand(IPlayer player, string cmd, string[] args)
        {
            BotClientFactory.Instance.ResetAllWebSockets();
            Chat(player, LangKeys.ResetWebSocket);
        }
        
        [HookMethod(nameof(ReconnectWebSocketCommand))]
        private void ReconnectWebSocketCommand(IPlayer player, string cmd, string[] args)
        {
            BotClientFactory.Instance.ReconnectAllWebSockets();
            Chat(player, LangKeys.ReconnectWebSocket);
        }
        
        [HookMethod(nameof(ResetRestApiCommand))]
        private void ResetRestApiCommand(IPlayer player, string cmd, string[] args)
        {
            BotClientFactory.Instance.ResetAllRestApis();
            Chat(player, LangKeys.ResetRestApi);
        }
        
        [HookMethod(nameof(ClearDiscordPool))]
        private void ClearDiscordPool(IPlayer player, string cmd, string[] args)
        {
            Chat(player, LangKeys.ClearPool);
            DiscordPool.Instance.Clear();
        }
        
        [HookMethod(nameof(WipeDiscordPool))]
        private void WipeDiscordPool(IPlayer player, string cmd, string[] args)
        {
            Chat(player, LangKeys.WipePool);
            DiscordPool.Instance.Wipe();
        }
        
        [HookMethod(nameof(ConsoleLogCommand))]
        private void ConsoleLogCommand(IPlayer player, string cmd, string[] args)
        {
            if (args.Length == 0)
            {
                Chat(player, LangKeys.ShowLog, "Console", DiscordConfig.Instance.Logging.ConsoleLogLevel);
                return;
            }

            try
            {
                DiscordLogLevel log = (DiscordLogLevel)Enum.Parse(typeof(DiscordLogLevel), args[0], true);
                DiscordConfig.Instance.Logging.ConsoleLogLevel = log;
                DiscordConfig.Instance.Save();

                Chat(player, LangKeys.SetLog, "Console", log);
            }
            catch
            {
                Chat(player, LangKeys.InvalidLogEnum, args[0]);
            }
        }
        
        [HookMethod(nameof(FileLogCommand))]
        private void FileLogCommand(IPlayer player, string cmd, string[] args)
        {
            if (args.Length == 0)
            {
                Chat(player, LangKeys.ShowLog, "File", DiscordConfig.Instance.Logging.FileLogLevel);
                return;
            }
            
            try
            {
                DiscordLogLevel log = (DiscordLogLevel)Enum.Parse(typeof(DiscordLogLevel), args[0], true);
                DiscordConfig.Instance.Logging.FileLogLevel = log;
                DiscordConfig.Instance.Save();

                Chat(player, LangKeys.SetLog, "Console", log);
            }
            catch
            {
                Chat(player, LangKeys.InvalidLogEnum, args[0]);
            }
        }

        [HookMethod(nameof(DiscordDebugCommand))]
        private void DiscordDebugCommand(IPlayer player, string cmd, string[] args)
        {
            DebugLogger logger = new DebugLogger();
            logger.StartArray("Bot Clients");
            foreach (BotClient bot in BotClientFactory.Instance.Clients)
            {
                bot.LogDebug(logger);
            }
            logger.EndArray();

            logger.StartObject("Libraries");
            logger.AppendObject("Discord Application Command", DiscordAppCommand.Instance);
            logger.AppendObject("Discord Command", DiscordCommand.Instance);
            logger.AppendObject("Discord Subscriptions", DiscordSubscriptions.Instance);
            DiscordAppCommand.Instance.LogDebug(logger);
            DiscordCommand.Instance.LogDebug(logger);
            DiscordSubscriptions.Instance.LogDebug(logger);
            logger.EndObject();

            string message = logger.ToString();
            player.Message(message);
            _logger.Info(message);
        }
        #endregion

        #region Hooks
        // ReSharper disable once UnusedMember.Local
        [HookMethod(nameof(OnPluginUnloaded))]
        private void OnPluginUnloaded(Plugin plugin)
        {
            if (plugin.Id() == "PlaceholderAPI")
            {
                HandlePlaceholderApiUnloaded();
            }
        }

        // ReSharper disable once UnusedMember.Local
        [HookMethod(nameof(OnUserConnected))]
        private void OnUserConnected(IPlayer player)
        {
            if (player.IsLinked())
            {
                ServerPlayerCache.Instance.SetPlayer(player);
            }
        }
        #endregion
    }
}