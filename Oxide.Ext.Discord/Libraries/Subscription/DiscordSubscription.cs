using System;
using Oxide.Core;
using Oxide.Core.Libraries;
using Oxide.Core.Plugins;
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

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="plugin">Plugin the subscription is for</param>
        /// <param name="callback">Callback when the channel message is sent</param>
        public DiscordSubscription(Plugin plugin, Action<DiscordMessage> callback)
        {
            Plugin = plugin; ;
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
                Plugin.TrackStart();
                Callback.Invoke(message);
                Plugin.TrackEnd();
            });
        }
    }
}