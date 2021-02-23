using System;
using Oxide.Core;
using Oxide.Core.Libraries;
using Oxide.Core.Plugins;
using Oxide.Ext.Discord.Entities.Messages;

namespace Oxide.Ext.Discord.Libraries.Subscription
{
    public class DiscordSubscription : Library
    {
        internal readonly Plugin Plugin;
        internal readonly Action<DiscordMessage> Callback;

        public DiscordSubscription(Plugin plugin, Action<DiscordMessage> callback)
        {
            Plugin = plugin; ;
            Callback = callback;
        }

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