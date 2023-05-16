using System;
using System.Linq;
using Oxide.Core.Plugins;
using Oxide.Ext.Discord.Callbacks.Libraries;
using Oxide.Ext.Discord.Entities;
using Oxide.Ext.Discord.Entities.Channels;
using Oxide.Ext.Discord.Entities.Guilds;
using Oxide.Ext.Discord.Entities.Messages;
using Oxide.Ext.Discord.Extensions;
using Oxide.Ext.Discord.Interfaces.Logging;
using Oxide.Ext.Discord.Logging;

namespace Oxide.Ext.Discord.Libraries.Subscription
{
    /// <summary>
    /// Represents a channel subscription for a plugin
    /// </summary>
    public class DiscordSubscription : IDebugLoggable
    {
        private readonly Snowflake _channelId;
        private readonly DiscordClient _client;
        private readonly Action<DiscordMessage> _callback;
        
        private Plugin _plugin;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="client">Discord Client for the subscription</param>
        /// <param name="channelId">ID of the channel</param>
        /// <param name="callback">Callback when the channel message is sent</param>
        public DiscordSubscription(DiscordClient client, Snowflake channelId, Action<DiscordMessage> callback)
        {
            _client = client;
            _plugin = client.Plugin;
            _channelId = channelId;
            _callback = callback;
        }

        /// <summary>
        /// Returns if a subscription can run.
        /// They can only run for the client that they were created for.
        /// </summary>
        /// <param name="client">Client to compare against</param>
        /// <returns>True if same bot client; false otherwise</returns>
        public bool CanRun(BotClient client)
        {
            return client != null && _client.Bot == client;
        }

        /// <summary>
        /// Invokes the callback with the message
        /// </summary>
        /// <param name="message">Message that was sent in the given channel</param>
        public void Invoke(DiscordMessage message)
        {
            SubscriptionCallback.Start(_plugin, message, _callback);
        }

        public void LogDebug(DebugLogger logger)
        {
            logger.AppendField("Plugin", _plugin.FullName());
            logger.AppendMethod("Method", _callback.Method);
            
            DiscordGuild guild = _client?.Bot.Servers.Values.FirstOrDefault(g => g.Channels.ContainsKey(_channelId));
            if (guild == null)
            {
                logger.AppendField("Guild", "Unknown Guild");
                return;
            }
            
            DiscordChannel channel = guild.Channels[_channelId];
            if (channel == null)
            {
                logger.AppendField("Channel", "Unknown Channel");
                return;
            }

            DiscordChannel parent = null;
            if (channel.ParentId.HasValue)
            {
                parent = guild.Channels[channel.ParentId.Value];
            }
            
            logger.AppendChannelPath("Path", guild, channel, parent);
            logger.AppendObject("Channel", channel);
        }

        internal void OnRemoved()
        {
            _plugin = null;
        }
    }
}