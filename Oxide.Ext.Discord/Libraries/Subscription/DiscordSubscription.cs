using System;
using Oxide.Core.Plugins;
using Oxide.Ext.Discord.Callbacks.Libraries;
using Oxide.Ext.Discord.Entities;
using Oxide.Ext.Discord.Entities.Messages;
using Oxide.Ext.Discord.Extensions;

namespace Oxide.Ext.Discord.Libraries.Subscription
{
    /// <summary>
    /// Represents a channel subscription for a plugin
    /// </summary>
    public class DiscordSubscription
    {
        internal Plugin Plugin;
        internal readonly Action<DiscordMessage> Callback;
        internal readonly Snowflake ChannelId;
        
        private readonly string _pluginId;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="channelId">ID of the channel</param>
        /// <param name="plugin">Plugin the subscription is for</param>
        /// <param name="callback">Callback when the channel message is sent</param>
        public DiscordSubscription(Snowflake channelId, Plugin plugin, Action<DiscordMessage> callback)
        {
            _pluginId = plugin.Id();
            ChannelId = channelId;
            Plugin = plugin;
            Callback = callback;
        }

        /// <summary>
        /// Returns if a subscription can run.
        /// They can only run for the client that they were created for.
        /// </summary>
        /// <param name="client">Client to compare against</param>
        /// <returns>True if same bot client; false otherwise</returns>
        public bool CanRun(BotClient client)
        {
            return client != null && DiscordClient.Clients[_pluginId]?.Bot == client;
        }

        /// <summary>
        /// Invokes the callback with the message
        /// </summary>
        /// <param name="message">Message that was sent in the given channel</param>
        public void Invoke(DiscordMessage message)
        {
            SubscriptionCallback.Start(Plugin, message, Callback);
        }

        internal void OnRemoved()
        {
            Plugin = null;
        }
    }
}