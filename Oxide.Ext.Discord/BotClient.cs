using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using Oxide.Ext.Discord.Entities.Channels;
using Oxide.Ext.Discord.Entities.Gatway;
using Oxide.Ext.Discord.Entities.Gatway.Commands;
using Oxide.Ext.Discord.Entities.Gatway.Events;
using Oxide.Ext.Discord.Entities.Guilds;
using Oxide.Ext.Discord.Entities.Interactions;
using Oxide.Ext.Discord.Logging;
using Oxide.Ext.Discord.REST;
using Oxide.Ext.Discord.WebSockets;
using Oxide.Plugins;
using Timer = System.Timers.Timer;

namespace Oxide.Ext.Discord
{
    public class BotClient
    {
        public static readonly Hash<string, BotClient> ActiveBots = new Hash<string, BotClient>();
        
        public string SessionId;
        public int Sequence;
        public bool Initialized;
        public bool HeartbeatAcknowledged;
        
        public readonly List<DiscordClient> Clients = new List<DiscordClient>();
        public readonly DiscordSettings Settings;
        private readonly ILogger _logger;
       
        public Hash<string, Guild> Servers { get; internal set; }
        public Hash<string, Channel> DirectMessages { get; } = new  Hash<string, Channel>();
        
        public Application Application { get; internal set; }
        public RestHandler Rest { get; private set; }
        
        private Socket _webSocket;
        private Timer _timer;
        
        internal Ready ReadyData;
        
        public BotClient(DiscordSettings settings)
        {
            Settings = new DiscordSettings
            {
                ApiToken = settings.ApiToken,
                LogLevel = settings.LogLevel,
                Intents = settings.Intents
            };

            _logger = new Logger(Settings.LogLevel);
        }
        
        public static BotClient GetOrCreate(DiscordClient client)
        {
            BotClient bot = ActiveBots[client.Settings.ApiToken];
            if (bot == null)
            {
                DiscordExtension.GlobalLogger.Debug($"{nameof(BotClient)}.{nameof(GetOrCreate)} Creating new BotClient");
                bot = new BotClient(client.Settings);
                ActiveBots[client.Settings.ApiToken] = bot;
            }
            
            bot.AddClient(client);
            return bot;
        }
        
        private void Connect()
        {
            if (Initialized)
            {
                throw new Exception("Bot Client already initialized");
            }

            Initialized = true;
            
            Rest = new RestHandler(this, _logger);
            _webSocket = new Socket(this, _logger);

            ConnectWebSocket();
        }
        
        public void ConnectWebSocket()
        {
            if (Initialized)
            {
                _logger.Debug($"{nameof(BotClient)}.{nameof(ConnectWebSocket)} Connecting to websocket");
                _webSocket.Connect();
            }
        }
        
        public void DisconnectWebsocket(bool attemptReconnect = false, bool attemptResume = false)
        {
            if (Initialized)
            {
                _webSocket.Disconnect(attemptReconnect, attemptResume);
            }
        }

        public void ShutdownBot()
        {
            _logger.Debug($"{nameof(BotClient)}.{nameof(ShutdownBot)} Shutting down the bot");
            Initialized = false;
            _webSocket.Shutdown();
            _webSocket = null;
            DestroyHeartbeat();
            Rest?.Shutdown();
            Rest = null;
            ReadyData = null;
        }
        
        public void AddClient(DiscordClient client)
        {
            Clients.RemoveAll(c => c == client);
            Clients.Add(client);
            
            _logger.Debug($"{nameof(BotClient)}.{nameof(AddClient)} Add client for plugin {client.Owner.Title}");
            
            if (Clients.Count == 1)
            {
                _logger.Debug($"{nameof(BotClient)}.{nameof(AddClient)} Clients.Count == 1 connecting bot");
                Connect();
            }
            else
            {
                if (client.Settings.LogLevel < Settings.LogLevel)
                {
                    UpdateLogLevel(client.Settings.LogLevel);
                }

                //Our intents have changed. Disconnect websocket and reconnect with new intents.
                BotIntents intents = Settings.Intents | client.Settings.Intents;
                if (intents != Settings.Intents)
                {
                    Settings.Intents = intents;
                    DisconnectWebsocket(true);
                    return;
                }
            }

            if (ReadyData != null)
            {
                ReadyData.Guilds = Servers.Values.ToList();
                client.CallHook("Discord_Ready", null, ReadyData, true);
            }
        }

        public void RemoveClient(DiscordClient client)
        {
            Clients.Remove(client);
            _logger.Debug($"{nameof(BotClient)}.{nameof(RemoveClient)} Client Removed");
            if (Clients.Count == 0)
            {
                ShutdownBot();
                _logger.Debug($"{nameof(BotClient)}.{nameof(RemoveClient)} Bot count 0 shutting down bot");
                return;
            }

            LogLevel level = Clients.Min(c => c.Settings.LogLevel);
            if (level > Settings.LogLevel)
            {
                UpdateLogLevel(level);
            }

            BotIntents intents = BotIntents.None;
            foreach (DiscordClient exitingClients in Clients)
            {
                intents |= exitingClients.Settings.Intents;
            }

            //Our intents have changed. Disconnect websocket and reconnect with new intents.
            if (intents != Settings.Intents)
            {
                Settings.Intents = intents;
                DisconnectWebsocket(true);
            }
        }
        
        private void UpdateLogLevel(LogLevel level)
        {
            _logger.Debug($"{nameof(BotClient)}.{nameof(UpdateLogLevel)} Updating log level from:{Settings.LogLevel} to: {level}");
            Settings.LogLevel = level;
            _logger.UpdateLogLevel(level);
        }

        public void CallHook(string hookName, params object[] args)
        {
            foreach (DiscordClient client in Clients)
            {
                client.CallHook(hookName, args);
            }
        }
        
        #region Heartbeat
        public void SetupHeartbeat(float heartbeatInterval)
        {
            if (_timer != null)
            {
                _logger.Debug($"{nameof(DiscordClient)}.{nameof(SetupHeartbeat)} Previous heartbeat timer exists.");
                DestroyHeartbeat();
            }

            HeartbeatAcknowledged = true;
            _timer = new Timer(heartbeatInterval);
            _timer.Elapsed += HeartbeatElapsed;
            _timer.Start();
            _logger.Debug($"{nameof(DiscordClient)}.{nameof(SetupHeartbeat)} Creating heartbeat with interval {heartbeatInterval}ms.");
        }

        public void DestroyHeartbeat()
        {
            if(_timer != null)
            {
                _logger.Debug($"{nameof(DiscordClient)}.{nameof(DestroyHeartbeat)} Destroy Heartbeat");
                _timer.Dispose();
                _timer = null;
            }
        }

        private void HeartbeatElapsed(object sender, ElapsedEventArgs e)
        {
            _logger.Debug($"{nameof(DiscordClient)}.{nameof(HeartbeatElapsed)} heartbeat Elapsed");
            if (!_webSocket.IsAlive() && !_webSocket.IsConnecting())
            {
                if (!_webSocket.IsReconnectTimerActive())
                {
                    _logger.Debug($"{nameof(DiscordClient)}.{nameof(HeartbeatElapsed)} Websocket is offline and is NOT connecting. Start reconnect timer.");
                    _webSocket.StartReconnectTimer(1f, () => UpdateGatewayUrl(ConnectWebSocket));
                }
                else
                {
                    _logger.Debug($"{nameof(DiscordClient)}.{nameof(HeartbeatElapsed)} Websocket is offline and is waiting to connect.");
                }

                return;
            }

            SendHeartbeat();
        }
        
        public void SendHeartbeat()
        {
            if(!HeartbeatAcknowledged)
            {
                //Discord did not acknowledge our last sent heartbeat. This is a zombie connection we should reconnect.
                if (_webSocket.IsAlive())
                {
                    _webSocket.Disconnect(true, true, true);
                }
                else if (!_webSocket.IsReconnectTimerActive())
                {
                    _logger.Debug($"{nameof(DiscordClient)}.{nameof(HeartbeatElapsed)} Heartbeat Elapsed and bot is not online or connecting.");
                    _webSocket.StartReconnectTimer(1f, ConnectWebSocket);
                }
                else
                {
                    _logger.Debug($"{nameof(DiscordClient)}.{nameof(HeartbeatElapsed)} Heartbeat Elapsed and bot is not online but is waiting to connect.");
                }
                
                return;
            }
            
            HeartbeatAcknowledged = false;
            _webSocket.Send(GatewayCommandCode.Heartbeat, Sequence);
            CallHook("DiscordSocket_HeartbeatSent");
            _logger.Debug($"Heartbeat sent - {_timer.Interval}ms interval.");
        }
        #endregion
        
        internal void UpdateGatewayUrl(Action callback)
        {
            Gateway.GetGateway(this, gateway =>
            {
                // Example: wss://gateway.discord.gg/?v=6&encoding=json
                Gateway.WebsocketUrl = $"{gateway.Url}/?{Entities.Gatway.Connect.Serialize()}";
                _logger.Debug($"Got Gateway url: {gateway.Url}");
                callback.Invoke();
            });
        }
        
        public void Identify()
        {
            // Sent immediately after connecting. Opcode 2: Identify
            // Ref: https://discordapp.com/developers/docs/topics/gateway#identifying

            if (!Initialized)
            {
                return;
            }
            
            Identify identify = new Identify()
            {
                Token = Settings.ApiToken,
                Properties = new Properties()
                {
                    OS = "Oxide.Ext.Discord",
                    Browser = "Oxide.Ext.Discord",
                    Device = "Oxide.Ext.Discord"
                },
                Intents = Settings.Intents,
                Compress = false,
                LargeThreshold = 50,
                Shard = new List<int>() { 0, 1 }
            };
            
            _webSocket.Send(GatewayCommandCode.Identify, identify);
        }
        
        public void Resume()
        {
            if (!Initialized)
            {
                return;
            }
            
            Resume resume = new Resume
            {
                Sequence = Sequence,
                SessionId = SessionId,
                Token = Settings.ApiToken
            };

            _webSocket.Send(GatewayCommandCode.Resume, resume);
        }
        
        public void RequestGuildMembers(GuildMembersRequest request)
        {
            if (!Initialized)
            {
                return;
            }

            _webSocket.Send(GatewayCommandCode.RequestGuildMembers, request);
        }

        public void UpdateVoiceState( VoiceStateUpdate voiceState)
        {
            if (!Initialized)
            {
                return;
            }

            _webSocket.Send(GatewayCommandCode.VoiceStateUpdate, voiceState);
        }

        public void UpdateStatus(StatusUpdate statusUpdate)
        {
            if (!Initialized)
            {
                return;
            }
            
            _webSocket.Send(GatewayCommandCode.StatusUpdate, statusUpdate);
        }

        public Guild GetGuild(string id)
        {
            return Servers[id];
        }

        public void AddGuild(Guild guild)
        {
            Servers[guild.Id] = guild;
        }
        
        internal void RemoveGuild(string guildId)
        {
            Servers.Remove(guildId);
        }
    }
}