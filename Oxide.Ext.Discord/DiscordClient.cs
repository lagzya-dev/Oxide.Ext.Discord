using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Timers;
using Newtonsoft.Json;
using Oxide.Core;
using Oxide.Core.Plugins;
using Oxide.Ext.Discord.Attributes;
using Oxide.Ext.Discord.Entities.Channels;
using Oxide.Ext.Discord.Entities.Gatway;
using Oxide.Ext.Discord.Entities.Gatway.Commands;
using Oxide.Ext.Discord.Entities.Guilds;
using Oxide.Ext.Discord.Helpers;
using Oxide.Ext.Discord.Logging;
using Oxide.Ext.Discord.REST;
using Oxide.Ext.Discord.WebSockets;

namespace Oxide.Ext.Discord
{
    public class DiscordClient
    {
        public List<Plugin> Plugins { get; private set; } = new List<Plugin>();

        public RestHandler REST { get; private set; }

        public DiscordSettings Settings { get; set; } = new DiscordSettings();

        public List<Guild> DiscordServers { get; set; } = new List<Guild>();
        public List<Channel> DMs { get; } = new List<Channel>();
        
        public Guild DiscordServer
        {
            get
            {
                return this.DiscordServers?.FirstOrDefault();
            }
        }

        public int Sequence;

        public string SessionID;

        private Socket _webSocket;

        private Timer _timer;

        private double _lastHeartbeat;

        public bool HeartbeatACK = false;

        public bool requestReconnect = false;

        private ILogger _logger;

        internal bool Disconnected;

        public void Initialize(Plugin plugin, DiscordSettings settings)
        {
            if (plugin == null)
            {
                throw new ArgumentNullException(nameof(plugin));
            }

            if (settings == null)
            {
                throw new ArgumentNullException(nameof(settings));
            }

            if (string.IsNullOrEmpty(settings.ApiToken))
            {
                throw new ArgumentNullException(nameof(settings.ApiToken));
            }
            
            _logger = new Logger<DiscordClient>(settings.LogLevel);
            
            if (!string.IsNullOrEmpty(DiscordExtension.TestVersion))
            {
                _logger.LogWarning($"Using Discord Test Version: {DiscordExtension.GetExtensionVersion}");
            }

            /*if(Discord.PendingTokens.Contains(settings.ApiToken)) // Not efficient, will re-do later
            {
                _logger.LogWarning($"[Discord Extension] Connection with same token in short period.. Connection delayed for {plugin.Name}");
                Timer t = new Timer() { AutoReset = false, Interval = 5000f, Enabled = true};
                t.Elapsed += (object sender, ElapsedEventArgs e) =>
                {
                    // TODO: Check if the connection still persists or cancelled
                    _logger.LogWarning($"[Discord Extension] Delayed connection for {plugin.Name} is being resumed..");
                    Initialize(plugin, settings);
                };
                return;
            }*/

            Disconnected = false;
            RegisterPlugin(plugin);
            UpdatePluginReference(plugin);
            CallHook("DiscordSocket_Initialized");

            Settings = settings;

            REST = new RestHandler(this, Settings.ApiToken, Settings.LogLevel);
            _webSocket = new Socket(this);
            
            ConnectToWebSocket();

            /*Discord.PendingTokens.Add(settings.ApiToken); // Not efficient, will re-do later
            Timer t2 = new Timer() { AutoReset = false, Interval = 5000f, Enabled = true };
            t2.Elapsed += (object sender, ElapsedEventArgs e) =>
            {
                if (Discord.PendingTokens.Contains(settings.ApiToken))
                    Discord.PendingTokens.Remove(settings.ApiToken);
            };*/
        }

        public void Disconnect()
        {
            Disconnected = true;
            _webSocket?.Disconnect();
            DestroyHeartbeat();
            _webSocket?.Dispose();
            _webSocket = null;
            REST?.Disconnect();
        }

        public void UpdatePluginReference(Plugin plugin = null)
        {
            List<Plugin> affectedPlugins = (plugin == null) ? Plugins : new List<Plugin>() { plugin };

            foreach (var pluginItem in affectedPlugins)
            {
                foreach (var field in pluginItem.GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static))
                {
                    if (field.GetCustomAttributes(typeof(DiscordClientAttribute), true).Any())
                    {
                        field.SetValue(pluginItem, this);
                    }
                }
            }
        }

        public void RegisterPlugin(Plugin plugin)
        {
            var search = Plugins.Where(x => x.Title == plugin.Title);
            search.ToList().ForEach(x => Plugins.Remove(x));

            Plugins.Add(plugin);
        }

        public void CallHook(string hookName, Plugin specificPlugin = null, params object[] args)
        {
            //Run from next tick so we can be sure it's ran on the main thread.
            Interface.Oxide.NextTick(() =>
            {
                if (specificPlugin != null)
                {
                    if (!specificPlugin.IsLoaded)
                    {
                        return;
                    }

                    specificPlugin.CallHook(hookName, args);
                    return;
                }

                Dictionary<string, object> returnValues = new Dictionary<string, object>();

                foreach (var plugin in Plugins.Where(x => x.IsLoaded))
                {
                    var retVal = plugin.CallHook(hookName, args);
                    returnValues.Add(plugin.Title, retVal);
                }

                if (returnValues.Count(x => x.Value != null) > 1)
                {
                    string conflicts = string.Join("\n", returnValues.Select(x => $"Plugin {x.Key} - {x.Value}").ToArray());
                    _logger.LogWarning($"A hook conflict was triggered on {hookName} between:\n{conflicts}");
                }
            });
        }

        public string GetPluginNames(string delimiter = ", ") => string.Join(delimiter, Plugins.Select(x => x.Name).ToArray());

        public void CreateHeartbeat(float heartbeatInterval)
        {
            if (_timer != null)
            {
                _logger.LogWarning($"Warning: tried to create a heartbeat when one is already registered.");
                return;
            }

            _lastHeartbeat = Time.TimeSinceEpoch();
            HeartbeatACK = true;
            
            _timer = new Timer()
            {
                Interval = heartbeatInterval
            };
            _timer.Elapsed += HeartbeatElapsed;
            _timer.Start();
        }

        public void DestroyHeartbeat()
        {
            if(_timer != null)
            {
                _timer.Dispose();
                _timer = null;
            }
        }

        private void HeartbeatElapsed(object sender, ElapsedEventArgs e)
        {
            if (_webSocket == null || !_webSocket.IsAlive())
            {
                DestroyHeartbeat();
                return;
            }

            SendHeartbeat();
        }

        internal void ConnectToWebSocket()
        {
            if (!string.IsNullOrEmpty(Gateway.WebsocketUrl))
            {
                _webSocket.Connect(Gateway.WebsocketUrl);
                return;
            }
            
            UpdateGatewayUrl(ConnectToWebSocket);
        }

        internal void UpdateGatewayUrl(Action callback)
        {
            Gateway.GetGateway(this, gateway =>
            {
                // Example: wss://gateway.discord.gg/?v=6&encoding=json
                Gateway.WebsocketUrl = $"{gateway.URL}/?{Connect.Serialize()}";
                _logger.LogDebug($"Got Gateway url: {gateway.URL}");
                callback.Invoke();
            });
        }

        #region Discord Events
        
        public void Identify()
        {
            // Sent immediately after connecting. Opcode 2: Identify
            // Ref: https://discordapp.com/developers/docs/topics/gateway#identifying

            Identify identify = new Identify()
            {
                Token = this.Settings.ApiToken,
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
            
            _webSocket.Send(SendOpCode.Identify, identify);
        }
        
        public void Resume()
        {
            var resume = new Resume()
            {
                Sequence = this.Sequence,
                SessionId = this.SessionID,
                Token = Settings.ApiToken
            };

            _webSocket.Send(SendOpCode.Resume, resume);
        }
        
        public void SendHeartbeat()
        {
            if(!HeartbeatACK)
            {
                // Didn't receive an ACK, thus connection can be considered zombie, thus destructing.
                _logger.LogError("Discord did not respond to Heartbeat! Disconnecting..");
                requestReconnect = true;
                _webSocket.ReconnectRequested();
                return;
            }
            
            HeartbeatACK = false;
            
            _webSocket.Send(SendOpCode.Heartbeat, Sequence);

            _lastHeartbeat = Time.TimeSinceEpoch();

            this.CallHook("DiscordSocket_HeartbeatSent");

            _logger.LogDebug($"Heartbeat sent - {_timer.Interval}ms interval.");
        }
        
        public void RequestGuildMembers(string guildId, string query = "", int limit = 0, bool? presences = null, List<string> userIds = null, string nonce = null)
        {
            var requestGuildMembers = new GuildMembersRequest
            {
                GuildId = guildId,
                Query = query,
                Limit = limit,
                Presences = presences,
                UserIds = userIds,
                Nonce = nonce
            };

            _webSocket.Send(SendOpCode.RequestGuildMembers, requestGuildMembers);
        }

        public void RequestGuildMembers(Guild guild, string query = "", int limit = 0, bool? presences = null, List<string> userIds = null, string nonce = null)
        {
            RequestGuildMembers(guild.Id, query, limit);
        }

        public void UpdateVoiceState(string guildID, string channelId, bool selfDeaf, bool selfMute)
        {
            var voiceState = new VoiceStateUpdate()
            {
                ChannelId = channelId,
                GuildId = guildID,
                SelfDeaf = selfDeaf,
                SelfMute = selfMute
            };
            
            _webSocket.Send(SendOpCode.VoiceStateUpdate, voiceState);
        }

        public void UpdateStatus(StatusUpdate statusUpdate)
        {
            _webSocket.Send(SendOpCode.StatusUpdate, statusUpdate);
        }

        public Guild GetGuild(string id)
        {
            return this.DiscordServers?.FirstOrDefault(x => x.Id == id);
        }

        public void UpdateGuild(string g_id, Guild newguild)
        {
            Guild g = this.GetGuild(g_id);
            if (g == null) return;
            int idx = DiscordServers?.IndexOf(g) ?? -1;
            if (idx == -1) return;
            this.DiscordServers[idx] = newguild;
        }

        #endregion
    }
}
