using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oxide.Core.Libraries.Covalence;
using Oxide.Core.Plugins;
using Oxide.Ext.Discord.Cache;
using Oxide.Ext.Discord.Data.Users;
using Oxide.Ext.Discord.Entities.Applications;
using Oxide.Ext.Discord.Entities.Channels;
using Oxide.Ext.Discord.Entities.Gatway.Commands;
using Oxide.Ext.Discord.Entities.Guilds;
using Oxide.Ext.Discord.Extensions;
using Oxide.Ext.Discord.Libraries.AppCommands.Commands;
using Oxide.Ext.Discord.Libraries.Command;
using Oxide.Ext.Discord.Libraries.Subscription;
using Oxide.Ext.Discord.Logging;
using Oxide.Ext.Discord.Pooling;
using Oxide.Ext.Discord.Rest;
using Oxide.Ext.Discord.Rest.Buckets;
using Oxide.Ext.Discord.Rest.Requests;
using Oxide.Ext.Discord.WebSockets;

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
        
        [HookMethod(nameof(Init))]
        private void Init()
        {
            Instance = this;
            _logger = DiscordLoggerFactory.Instance.GetExtensionLogger(DiscordLogLevel.Info);
            AddCovalenceCommand(new[] { "de.version" }, nameof(VersionCommand), "de.version");
            AddCovalenceCommand(new[] { "de.websocket.reset" }, nameof(ResetWebSocketCommand), "de.websocket.reset");
            AddCovalenceCommand(new[] { "de.websocket.reconnect" }, nameof(ReconnectWebSocketCommand), "de.websocket.reconnect");
            AddCovalenceCommand(new[] { "de.rest.reset" }, nameof(ResetRestApiCommand), "de.rest.reset");
            AddCovalenceCommand(new[] { "de.clearpool" }, nameof(ClearDiscordPool), "de.clearpool");
            AddCovalenceCommand(new[] { "de.consolelog" }, nameof(ConsoleLogCommand), "de.consolelog");
            AddCovalenceCommand(new[] { "de.filelog" }, nameof(FileLogCommand), "de.filelog");
            AddCovalenceCommand(new[] { "de.debug" }, nameof(DiscordDebugCommand), "de.debug");
            
            foreach (KeyValuePair<string, Dictionary<string, string>> language in Localization.Languages)
            {
                Lang.RegisterMessages(language.Value, this, language.Key);
            }

            CreateTemplates();
            DiscordExtension.DiscordPlaceholders.RegisterPlaceholders();
        }

        [HookMethod(nameof(OnServerInitialized))]
        private void OnServerInitialized()
        {
            IsServerLoaded = true;
        }

        [HookMethod(nameof(OnServerSave))]
        private void OnServerSave()
        {
            DiscordUserData.Instance.Save(false);
        }
        
        [HookMethod(nameof(OnServerShutdown))]
        private void OnServerShutdown()
        {
            DiscordExtension.IsShuttingDown = true;
        }

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
            Chat(player, LangKeys.ResetWebSocket);
            
            foreach (BotClient client in BotClient.ActiveBots.Values)
            {
                client.ResetWebSocket();
            }
        }
        
        [HookMethod(nameof(ReconnectWebSocketCommand))]
        private void ReconnectWebSocketCommand(IPlayer player, string cmd, string[] args)
        {
            Chat(player, LangKeys.ReconnectWebSocket);
            
            foreach (BotClient client in BotClient.ActiveBots.Values)
            {
                client.WebSocket.Disconnect(true, true, true);
            }
        }
        
        [HookMethod(nameof(ResetRestApiCommand))]
        private void ResetRestApiCommand(IPlayer player, string cmd, string[] args)
        {
            Chat(player, LangKeys.ResetRestApi);
            
            foreach (BotClient client in BotClient.ActiveBots.Values)
            {
                client.ResetRestApi();
            }
        }
        
        [HookMethod(nameof(ClearDiscordPool))]
        private void ClearDiscordPool(IPlayer player, string cmd, string[] args)
        {
            Chat(player, LangKeys.ResetRestApi);
            
            DiscordPool.Clear();
        }
        
        [HookMethod(nameof(ConsoleLogCommand))]
        private void ConsoleLogCommand(IPlayer player, string cmd, string[] args)
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
        
        [HookMethod(nameof(FileLogCommand))]
        private void FileLogCommand(IPlayer player, string cmd, string[] args)
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

        [HookMethod(nameof(DiscordDebugCommand))]
        private void DiscordDebugCommand(IPlayer player, string cmd, string[] args)
        {
            StringBuilder sb = new StringBuilder();
            foreach (BotClient bot in BotClient.ActiveBots.Values)
            {
                sb.Append('=', 50);
                sb.AppendLine();
                sb.Append("Client: ");
                sb.AppendLine(bot.Settings.GetHiddenToken());
                sb.Append("Initialized: ");
                sb.AppendLine(bot.Initialized ? "Yes" : "No");
                sb.Append("Bot: ");
                sb.AppendLine(bot.BotUser?.FullUserName ?? "Unknown");
                sb.Append("Log Level: ");
                sb.AppendLine(bot.Settings.LogLevel.ToString());
                sb.Append("Intents: ");
                sb.AppendLine(bot.Settings.Intents.ToString());
                sb.Append("Plugins: ");
                sb.AppendLine(bot.GetClientPluginList());
                sb.AppendLine("Application Flags: ");
                if (bot.Application?.Flags == null || (int)bot.Application.Flags == 0)
                {
                    sb.Append("\t");
                    sb.AppendLine(ApplicationFlags.None.ToString());
                }
                else
                {
                    foreach (ApplicationFlags flag in Enum.GetValues(typeof(ApplicationFlags)).Cast<ApplicationFlags>())
                    {
                        if (flag != ApplicationFlags.None && bot.Application.HasApplicationFlag(flag))
                        {
                            sb.Append("\t");
                            sb.AppendLine(flag.ToString());
                        }
                    }
                }

                DebugWebsocket(bot, sb);
                DebugRest(bot, sb);
            }

            sb.AppendLine("Libraries:");
            DebugApplicationCommands(sb);
            DebugDiscordCommands(sb);
            DebugSubscriptions(sb);

            string message = sb.ToString();
            player.Message(message);
            _logger.Info(message);
        }
        
        private void DebugWebsocket(BotClient bot, StringBuilder sb)
        {
            DiscordWebSocket websocket = bot.WebSocket;
            sb.Append("Websocket: ");
            if (websocket != null)
            {
                sb.AppendLine(websocket.Handler.SocketState.ToString());
                sb.Append("\tPending Commands: ");
                IReadOnlyCollection<WebSocketCommand> pendingCommands = websocket.Commands.GetPendingCommands();
                if (pendingCommands.Count == 0)
                {
                    sb.AppendLine("None");
                }
                else
                {
                    sb.AppendLine();
                    foreach (WebSocketCommand command in pendingCommands)
                    {
                        sb.Append("\tCommand: ");
                        sb.Append(command.Client.PluginName);
                        sb.Append(' ');
                        sb.AppendLine(command.Payload.OpCode.ToString());
                    }
                }
            }
            else
            {
                sb.AppendLine("Is NULL!");
            }
        }
        
        private void DebugRest(BotClient bot, StringBuilder sb)
        {
            RestHandler rest = bot.Rest;
            sb.AppendLine("REST: ");
            if (rest != null)
            {
                sb.AppendLine("\tBuckets: ");
                Bucket[] buckets = rest.Buckets.Values.ToArray();
                if (buckets.Length == 0)
                {
                    sb.AppendLine("\t\tNone");
                }
                else
                {
                    for (int index = 0; index < buckets.Length; index++)
                    {
                        Bucket bucket = buckets[index];
                        sb.Append("\t\tID: ");
                        sb.Append(bucket.Id);
                        sb.Append(" (Known Bucket: ");
                        sb.AppendLine(bucket.IsKnowBucket ? "Yes)" : "No)");
                        sb.Append("\t\tRemaining: ");
                        sb.Append(bucket.Remaining.ToString());
                        sb.Append(" Limit: ");
                        sb.Append(bucket.Limit.ToString());
                        sb.Append(" Reset In: ");
                        double resetIn = bucket.ResetAt < DateTimeOffset.UtcNow ? 0 : (bucket.ResetAt - DateTimeOffset.UtcNow).TotalSeconds;
                        sb.Append(resetIn.ToString());
                        sb.AppendLine(" Seconds");
                        sb.Append("\t\tRequest Queue Count: ");
                        sb.AppendLine(bucket.Requests.Count.ToString());
                        sb.Append("\t\tSemaphore: ");
                        sb.Append(bucket.Semaphore.Available.ToString());
                        sb.Append('/');
                        sb.AppendLine(bucket.Semaphore.MaximumCount.ToString());
                        sb.AppendLine("\t\tRoutes:");
                        foreach (KeyValuePair<string, string> route in rest.RouteToHash)
                        {
                            if (route.Value == bucket.Id)
                            {
                                sb.Append("\t\t\t");
                                sb.AppendLine(route.Key);
                            }
                        }
                        sb.AppendLine("\t\tRequests:");
                        foreach (RequestHandler handler in bucket.Requests.Values)
                        {
                            BaseRequest request = handler.Request;
                            sb.Append("\t\t\tID: ");
                            sb.AppendLine(request.Id.ToString());
                            sb.Append("\t\t\tMethod: ");
                            sb.AppendLine(request.Method.ToString());
                            sb.Append("\t\t\tRoute: ");
                            sb.AppendLine(request.Route);
                            sb.Append("\t\t\tStatus: ");
                            sb.AppendLine(request.Status.ToString());
                            sb.AppendLine();
                        }
                        sb.AppendLine();
                    }
                }
            }
            else
            {
                sb.AppendLine("Is NULL!");
            }
        }

        private void DebugApplicationCommands(StringBuilder sb)
        {
            sb.AppendLine("\tApplication Commands:");

            foreach (BotClient client in BotClient.ActiveBots.Values)
            {
                sb.Append("\t\tApplication ID: ");
                sb.AppendLine(client.Application.Id);
                foreach (BaseAppCommand command in DiscordExtension.DiscordAppCommand.GetCommands(client.Application.Id))
                {
                    if (command is ComponentCommand componentCommand)
                    {
                        sb.Append("\t\t\tCommand Name: ");
                        sb.AppendLine(componentCommand.CustomId);
                        sb.Append("\t\t\tInteraction Type: ");
                        sb.AppendLine(componentCommand.Type.ToString());
                        sb.Append("\t\t\tPlugin: ");
                        sb.Append(componentCommand.Plugin.FullName());
                        sb.AppendLine();
                    }
                    else if (command is AutoCompleteCommand autoCompleteCommand)
                    {
                        sb.Append("\t\t\tCommand Name: ");
                        sb.AppendLine(autoCompleteCommand.Command.ToString());
                        sb.Append("\t\t\tInteraction Type: ");
                        sb.AppendLine(autoCompleteCommand.Type.ToString());
                        sb.Append("\t\t\tPlugin: ");
                        sb.Append(autoCompleteCommand.Plugin.FullName());
                        sb.AppendLine();
                    }
                    else if (command is AppCommand appCommand)
                    {
                        sb.Append("\t\t\tCommand Name: ");
                        sb.AppendLine(appCommand.Command.ToString());
                        sb.Append("\t\t\tInteraction Type: ");
                        sb.AppendLine(appCommand.Type.ToString());
                        sb.Append("\t\t\tPlugin: ");
                        sb.Append(appCommand.Plugin.FullName());
                        sb.AppendLine();
                    }
                }
            }
        }
        public void DebugDiscordCommands(StringBuilder sb)
        {
            sb.AppendLine("\tDiscord Commands:");
            foreach (BaseCommand command in DiscordExtension.DiscordCommand.GetCommands())
            {
                if (command is GuildCommand guildCommand)
                {
                    sb.Append("\t\tCommand Name: ");
                    sb.AppendLine(guildCommand.Name);
                    sb.Append("\t\tPlugin: ");
                    sb.Append(guildCommand.Plugin.FullName());
                    sb.AppendLine("\t\tType: Guild Command");
                    sb.AppendLine();
                }
                else if (command is DirectMessageCommand directMessageCommand)
                {
                    sb.Append("\t\tCommand Name: ");
                    sb.AppendLine(directMessageCommand.Name);
                    sb.Append("\t\tPlugin: ");
                    sb.Append(directMessageCommand.Plugin.FullName());
                    sb.AppendLine("\t\tType: Direct Message Command");
                    sb.AppendLine();
                }
            }
        }
        
        public void DebugSubscriptions(StringBuilder sb)
        {
            sb.AppendLine("\tDiscord Channel Subscriptions:");
            foreach (DiscordSubscription sub in DiscordExtension.DiscordSubscriptions.GetSubscriptions())
            {
                DiscordChannel channel = null;
                DiscordChannel parent = null;
                foreach (BotClient client in BotClient.ActiveBots.Values)
                {
                    foreach (DiscordGuild guild in client.Servers.Values)
                    {
                        channel = guild.Channels[sub.ChannelId];
                        if (channel != null)
                        {
                            if (channel.ParentId.HasValue)
                            {
                                parent = guild.Channels[channel.ParentId.Value];
                            }
                            break;
                        }
                    }
                }

                sb.Append("\t\tChannel Name: ");

                if (parent != null)
                {
                    sb.Append(parent.Name);
                    sb.Append('/');
                    sb.AppendLine(channel.Name);
                }
                else
                {
                    sb.AppendLine(channel?.Name ?? "Unknown Channel");
                }

                sb.Append("\t\tPlugin: ");
                sb.Append(sub.Plugin.FullName());
                sb.Append("\t\tMethod: ");
                sb.Append(sub.Callback.Method.DeclaringType.Name);
                sb.Append('.');
                sb.AppendLine(sub.Callback.Method.Name);
                sb.AppendLine();
            }
        }
        #endregion

        #region Hooks
        [HookMethod(nameof(OnPluginUnloaded))]
        private void OnPluginUnloaded(Plugin plugin)
        {
            if (plugin.Id() == "PlaceholderAPI")
            {
                HandlePlaceholderApiUnloaded();
            }
        }

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