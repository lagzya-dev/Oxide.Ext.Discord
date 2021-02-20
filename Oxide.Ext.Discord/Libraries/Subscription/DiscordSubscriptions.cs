using System;
using System.Collections.Generic;
using System.Linq;
using Oxide.Core;
using Oxide.Core.Libraries;
using Oxide.Core.Plugins;
using Oxide.Ext.Discord.Entities;
using Oxide.Ext.Discord.Entities.Channels;
using Oxide.Ext.Discord.Entities.Messages;
using Oxide.Ext.Discord.Extensions;
using Oxide.Plugins;

namespace Oxide.Ext.Discord.Libraries.Subscription
{
    public class DiscordSubscriptions : Library
    {
        private readonly Hash<Snowflake, List<DiscordSubscription>> _subscriptions = new Hash<Snowflake, List<DiscordSubscription>>();
        
        /// <summary>
        /// Sourced from Command.cs of OxideMod (https://github.com/OxideMod/Oxide.Rust/blob/develop/src/Libraries/Command.cs#L104)
        /// </summary>
        private readonly Hash<Plugin, Event.Callback<Plugin, PluginManager>> _pluginRemovedFromManager = new Hash<Plugin, Event.Callback<Plugin, PluginManager>>();

        public bool HasSubscriptions()
        {
            return _subscriptions.Count != 0;
        }
        
        public void AddChannelSubscription(Plugin plugin, Snowflake channelId, Action<DiscordMessage> message)
        {
            if (plugin == null)
            {
                throw new ArgumentNullException(nameof(plugin));
            }

            if (!channelId.IsValid())
            {
                throw new ArgumentException("Value should be valid.", nameof(channelId));
            }

            if (message == null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            List<DiscordSubscription> subscriptions = _subscriptions[channelId];
            if (subscriptions == null)
            {
                subscriptions = new List<DiscordSubscription>();
                _subscriptions[channelId] = subscriptions;
            }
            
            subscriptions.Add(new DiscordSubscription(plugin, message));
            if (!_pluginRemovedFromManager.ContainsKey(plugin))
            {
                _pluginRemovedFromManager[plugin] = plugin.OnRemovedFromManager.Add(RemovePluginSubscription);
            }
        }
        
        public void RemoveChannelSubscription(Plugin plugin, Snowflake channelId)
        {
            List<DiscordSubscription> subscriptions = _subscriptions[channelId];
            if (subscriptions == null)
            {
                return;
            }
            
            subscriptions.RemoveAll(s => s.Plugin == plugin);
            if (subscriptions.Count == 0)
            {
                _subscriptions.Remove(channelId);
            }

            if (_subscriptions.Values.All(s => s.All(p => p.Plugin != plugin)) && _pluginRemovedFromManager.TryGetValue(plugin, out Event.Callback<Plugin, PluginManager> callback))
            {
                callback.Remove();
                _pluginRemovedFromManager.Remove(plugin);
            }
        }
        
        public void RemovePluginSubscription(Plugin plugin, PluginManager manager)
        {
            foreach (List<DiscordSubscription> value in _subscriptions.Values)
            {
                value.RemoveAll(s => s.Plugin == plugin);
            }
            
            _subscriptions.RemoveAll(s => s.Count == 0);
            
            if(_pluginRemovedFromManager.TryGetValue(plugin, out Event.Callback<Plugin, PluginManager> callback))
            {
                callback.Remove();
                _pluginRemovedFromManager.Remove(plugin);
            }
        }

        public void HandleMessage(DiscordMessage message, Channel channel)
        {
            List<DiscordSubscription> subs = _subscriptions[message.ChannelId];
            foreach (DiscordSubscription sub in subs)
            {
                sub.Invoke(message);
            }

            if (channel?.ParentId != null)
            {
                subs = _subscriptions[channel.ParentId.Value];
                foreach (DiscordSubscription sub in subs)
                {
                    sub.Invoke(message);
                }
            }
        }
    }
}