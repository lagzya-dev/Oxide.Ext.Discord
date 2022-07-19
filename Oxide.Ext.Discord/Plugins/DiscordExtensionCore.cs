using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oxide.Core.Libraries.Covalence;
using Oxide.Core.Plugins;
using Oxide.Ext.Discord.Entities.Applications;
using Oxide.Ext.Discord.Entities.Gatway;
using Oxide.Ext.Discord.Logging;
using Oxide.Ext.Discord.Rest;
using Oxide.Ext.Discord.Rest.Buckets;
using Oxide.Ext.Discord.Rest.Requests;
using Oxide.Ext.Discord.WebSockets;

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
            AddCovalenceCommand(new[] { "de.rws" }, nameof(ResetWebSocketCommand), "de.rws");
            AddCovalenceCommand(new[] { "de.consolelog" }, nameof(ConsoleLogCommand), "de.consolelog");
            AddCovalenceCommand(new[] { "de.filelog" }, nameof(FileLogCommand), "de.filelog");
            AddCovalenceCommand(new[] { "de.debug" }, nameof(DiscordDebugCommand), "de.debug");
            
            foreach (KeyValuePair<string, Dictionary<string, string>> language in Localization.Languages)
            {
                Lang.RegisterMessages(language.Value, this, language.Key);
            }
        }

        [HookMethod("OnServerShutdown")]
        private void OnServerShutdown()
        {
            DiscordExtension.IsShuttingDown = true;
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
                DiscordWebSocket websocket = bot.WebSocket;
                RestHandler rest = bot.Rest;
                sb.Append('=', 50);
                sb.AppendLine();
                sb.Append("Client: ");
                sb.AppendLine(bot.Settings.GetHiddenToken());
                sb.Append("Initialized: ");
                sb.AppendLine(bot.Initialized ? "Yes" : "No");
                sb.Append("Bot: ");
                sb.AppendLine(bot.BotUser?.GetFullUserName ?? "Unknown");
                sb.Append("Log Level: ");
                sb.AppendLine(bot.Settings.LogLevel.ToString());
                sb.Append("Intents: ");
                sb.AppendLine(bot.Settings.Intents.ToString());
                sb.Append("Plugins: ");
                sb.AppendLine(string.Join(", ", bot.Clients.Select(c => c.PluginName).ToArray()));
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

                sb.Append("Websocket: ");
                if (websocket != null)
                {
                    sb.AppendLine(websocket.Handler.SocketState.ToString());
                    sb.Append("\tPending Commands: ");
                    IReadOnlyCollection<CommandPayload> pendingCommands = websocket.Commands.GetPendingCommands();
                    if (pendingCommands.Count == 0)
                    {
                        sb.AppendLine("None");
                    }
                    else
                    {
                        sb.AppendLine();
                        foreach (CommandPayload command in pendingCommands)
                        {
                            sb.Append("\tCommand: ");
                            sb.AppendLine(command.OpCode.ToString());
                        }
                    }
                }
                else
                {
                    sb.AppendLine("Is NULL!");
                }

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
                            sb.AppendLine(resetIn.ToString());
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

            string message = sb.ToString();
            player.Message(message);
            DiscordLogger.FileLogger.AddMessage(DiscordLogLevel.Info, message, null);
        }
        #endregion
    }
}