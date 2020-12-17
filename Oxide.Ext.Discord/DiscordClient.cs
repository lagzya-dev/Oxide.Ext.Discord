using Oxide.Ext.Discord.Logging;

namespace Oxide.Ext.Discord
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Timers;
    using Newtonsoft.Json;
    using Oxide.Core;
    using Oxide.Core.Plugins;
    using Oxide.Ext.Discord.Attributes;
    using Oxide.Ext.Discord.DiscordEvents;
    using Oxide.Ext.Discord.DiscordObjects;
    using Oxide.Ext.Discord.Exceptions;
    using Oxide.Ext.Discord.Gateway;
    using Oxide.Ext.Discord.Helpers;
    using Oxide.Ext.Discord.REST;
    using Oxide.Ext.Discord.WebSockets;

    public class DiscordClient
    {
        public List<Plugin> Plugins { get; private set; } = new List<Plugin>();

        public RESTHandler REST { get; private set; }

        private static string WebSocketUrl { get; set; }

        public DiscordSettings Settings { get; set; } = new DiscordSettings();

        public List<Guild> DiscordServers { get; set; } = new List<Guild>();
        public List<Channel> DMs { get; set; } = new List<Channel>();
        
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

        public void Initialize(Plugin plugin, DiscordSettings settings)
        {
            if (plugin == null)
            {
                throw new PluginNullException();
            }

            if (settings == null)
            {
                throw new SettingsNullException();
            }

            if (string.IsNullOrEmpty(settings.ApiToken))
            {
                throw new APIKeyException();
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

            RegisterPlugin(plugin);
            UpdatePluginReference(plugin);
            CallHook("DiscordSocket_Initialized");

            Settings = settings;

            REST = new RESTHandler(Settings.ApiToken, Settings.LogLevel);
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
            _webSocket?.Disconnect();
            DestroyHeartbeat();
            _webSocket?.Dispose();
            _webSocket = null;

            WebSocketUrl = string.Empty;

            REST?.Shutdown();
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
                _logger.LogWarning($"[Discord Extension] Warning: tried to create a heartbeat when one is already registered.");
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
            if (!string.IsNullOrEmpty(WebSocketUrl))
            {
                _webSocket.Connect(WebSocketUrl);
                return;
            }
            
            ConnectToWebsocketUrl();
        }
        
        internal void ConnectToWebsocketUrl()
        {
            GetGatewayUrl(url =>
            {
                UpdateWebsocketUrl(url);
                ConnectToWebSocket();
            });
        }
        
        private void GetGatewayUrl(Action<string> callback)
        {
            DiscordObjects.Gateway.GetGateway(this, (gateway) =>
            {
                // Example: wss://gateway.discord.gg/?v=6&encoding=json
                string url = $"{gateway.URL}/?{Connect.Serialize()}";

                _logger.LogDebug($"Got Gateway url: {url}");

                callback.Invoke(url);
            });
        }

        public void UpdateWebsocketUrl(string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                return;
            }
            
            WebSocketUrl = url;
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
                Compress = false,
                LargeThreshold = 50,
                Shard = new List<int>() { 0, 1 }
            };
            
            _webSocket.Send(OpCodes.Identify, identify);
        }
        
        public void Resume()
        {
            var resume = new Resume()
            {
                Sequence = this.Sequence,
                SessionID = this.SessionID,
                Token = Settings.ApiToken
            };

            _webSocket.Send(OpCodes.Resume, resume);
        }
        
        public void SendHeartbeat()
        {
            if(!HeartbeatACK)
            {
                // Didn't receive an ACK, thus connection can be considered zombie, thus destructing.
                _logger.LogError("[Discord Extension] Discord did not respond to Heartbeat! Disconnecting..");
                requestReconnect = true;
                _webSocket.Disconnect(false);
                return;
            }
            
            HeartbeatACK = false;
            
            _webSocket.Send(OpCodes.Heartbeat, Sequence);

            _lastHeartbeat = Time.TimeSinceEpoch();

            this.CallHook("DiscordSocket_HeartbeatSent");

            _logger.LogDebug($"[Discord Extension] Heartbeat sent - {_timer.Interval}ms interval.");
        }
        
        public void RequestGuildMembers(string guildId, string query = "", int limit = 0, bool? presences = null, List<string> userIds = null, string nonce = null)
        {
            var requestGuildMembers = new GuildMembersRequest
            {
                GuildID = guildId,
                Query = query,
                Limit = limit,
                Presences = presences,
                UserIds = userIds,
                Nonce = nonce
            };

            _webSocket.Send(OpCodes.RequestGuildMembers, requestGuildMembers);
        }

        public void RequestGuildMembers(Guild guild, string query = "", int limit = 0)
        {
            RequestGuildMembers(guild.id, query, limit);
        }

        public void UpdateVoiceState(string guildID, string channelId, bool selfDeaf, bool selfMute)
        {
            var voiceState = new VoiceStateUpdate()
            {
                ChannelID = channelId,
                GuildID = guildID,
                SelfDeaf = selfDeaf,
                SelfMute = selfMute
            };
            
            _webSocket.Send(OpCodes.VoiceStateUpdate, voiceState);
        }

        public void UpdateStatus(Presence presence)
        {
            _webSocket.Send(OpCodes.StatusUpdate, presence);
        }

        public Guild GetGuild(string id)
        {
            return this.DiscordServers?.FirstOrDefault(x => x.id == id);
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
