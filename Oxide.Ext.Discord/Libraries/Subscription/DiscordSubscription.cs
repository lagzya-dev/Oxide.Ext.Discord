using System;
using Oxide.Core.Libraries;
using Oxide.Core.Plugins;
using Oxide.Ext.Discord.Entities.Messages;

namespace Oxide.Ext.Discord.Libraries.Subscription
{
    public class DiscordSubscription : Library
    {
        internal readonly Plugin Plugin;
        internal readonly Action<DiscordMessage> _callback;

        public DiscordSubscription(Plugin plugin, Action<DiscordMessage> callback)
        {
            Plugin = plugin; ;
            _callback = callback;
        }

        public void Invoke(DiscordMessage message)
        {
            Plugin.TrackStart();
            _callback.Invoke(message);
            Plugin.TrackEnd();
        }
    }
}