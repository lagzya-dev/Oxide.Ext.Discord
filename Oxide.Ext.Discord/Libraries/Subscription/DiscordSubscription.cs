using System;
using Oxide.Core;
using Oxide.Core.Libraries;
using Oxide.Core.Plugins;
using Oxide.Ext.Discord.Entities;
using Oxide.Ext.Discord.Entities.Messages;

namespace Oxide.Ext.Discord.Libraries.Subscription
{
    /// <summary>
    /// Represents a channel subscription for a plugin
    /// </summary>
    public class DiscordSubscription
    {
        internal readonly Plugin Plugin;
        internal readonly Action<DiscordMessage> Callback;
        internal readonly Snowflake ChannelId;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="channelId">ID of the channel</param>
        /// <param name="plugin">Plugin the subscription is for</param>
        /// <param name="callback">Callback when the channel message is sent</param>
        public DiscordSubscription(Snowflake channelId, Plugin plugin, Action<DiscordMessage> callback)
        {
            ChannelId = channelId;
            Plugin = plugin;
            Callback = callback;
        }

        /// <summary>
        /// Invokes the callback with the message
        /// </summary>
        /// <param name="message">Message that was sent in the given channel</param>
        public void Invoke(DiscordMessage message)
        {
            Interface.Oxide.NextTick(() =>
            {
                try
                {
                    Plugin.TrackStart();
                    Callback.Invoke(message);
                    Plugin.TrackEnd();
                }
                catch(Exception ex)
                {
                    DiscordExtension.GlobalLogger.Exception($"An exception occured for discord subscription in channel {ChannelId.ToString()} for plugin {Plugin?.Name}", ex);   
                }
            });
        }
    }
}