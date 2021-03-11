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
using Oxide.Ext.Discord.Logging;
using Oxide.Plugins;

namespace Oxide.Ext.Discord.Libraries.Subscription
{
    /// <summary>
    /// Represents Discord Subscriptions Oxide Library
    /// Allows for plugins to subscribe to discord channels
    /// </summary>
    public class DiscordSubscriptions : Library
    {
        private readonly Hash<Snowflake, List<DiscordSubscription>> _subscriptions = new Hash<Snowflake, List<DiscordSubscription>>();
        
        /// <summary>
        /// Sourced from Command.cs of OxideMod (https://github.com/OxideMod/Oxide.Rust/blob/develop/src/Libraries/Command.cs#L104)
        /// </summary>
        private readonly Hash<Plugin, Event.Callback<Plugin, PluginManager>> _pluginRemovedFromManager = new Hash<Plugin, Event.Callback<Plugin, PluginManager>>();

        private readonly ILogger _logger;
        
        /// <summary>
        /// DiscordSubscriptions Constructor
        /// </summary>
        /// <param name="logger">Logger</param>
        public DiscordSubscriptions(ILogger logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Returns if any subscriptions have been registered
        /// </summary>
        /// <returns>True if there are any subscriptions; False otherwise</returns>
        [LibraryFunction(nameof(HasSubscriptions))]
        public bool HasSubscriptions()
        {
            return _subscriptions.Count != 0;
        }
        
        /// <summary>
        /// Allows a plugin to add a subscription to a discord channel
        /// </summary>
        /// <param name="plugin">Plugin that is subscribing</param>
        /// <param name="channelId">Channel ID of the channel</param>
        /// <param name="message">Callback with the message that was created in the channel</param>
        /// <exception cref="ArgumentNullException">Exception if plugin or message is null</exception>
        /// <exception cref="ArgumentException">Exception if Channel ID is not valid</exception>
        [LibraryFunction(nameof(AddChannelSubscription))]
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

            _logger.Debug($"{nameof(DiscordSubscriptions)}.{nameof(AddChannelSubscription)} {plugin.Name} added subscription to channel {channelId}");
            List<DiscordSubscription> subscriptions = _subscriptions[channelId];
            if (subscriptions == null)
            {
                subscriptions = new List<DiscordSubscription>();
                _subscriptions[channelId] = subscriptions;
            }
            
            subscriptions.Add(new DiscordSubscription(channelId, plugin, message));
            if (!_pluginRemovedFromManager.ContainsKey(plugin))
            {
                _pluginRemovedFromManager[plugin] = plugin.OnRemovedFromManager.Add(RemovePluginSubscriptions);
            }
        }
        
        /// <summary>
        /// Removes a subscribed channel for a plugin
        /// </summary>
        /// <param name="plugin">Plugin to remove the subscription for</param>
        /// <param name="channelId">Channel ID to remove</param>
        /// <exception cref="ArgumentNullException">Exception if plugin is null</exception>
        /// <exception cref="ArgumentException">Exception if channel ID is not valid</exception>
        [LibraryFunction(nameof(RemoveChannelSubscription))]
        public void RemoveChannelSubscription(Plugin plugin, Snowflake channelId)
        {
            if (plugin == null)
            {
                throw new ArgumentNullException(nameof(plugin));
            }

            if (!channelId.IsValid())
            {
                throw new ArgumentException("Value should be valid.", nameof(channelId));
            }
            
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
            
            _logger.Debug($"{nameof(DiscordSubscriptions)}.{nameof(RemoveChannelSubscription)} {plugin.Name} removed subscription to channel {channelId}");

            if (_subscriptions.Values.All(s => s.All(p => p.Plugin != plugin)) && _pluginRemovedFromManager.TryGetValue(plugin, out Event.Callback<Plugin, PluginManager> callback))
            {
                callback.Remove();
                _pluginRemovedFromManager.Remove(plugin);
            }
        }

        private void RemovePluginSubscriptions(Plugin plugin, PluginManager manager) => RemovePluginSubscriptions(plugin);
        
        /// <summary>
        /// Remove all subscriptions for a plugin
        /// </summary>
        /// <param name="plugin">Plugin to remove subscriptions for</param>
        /// <exception cref="ArgumentNullException">Exception if plugin is null</exception>
        public void RemovePluginSubscriptions(Plugin plugin)
        {
            if (plugin == null) throw new ArgumentNullException(nameof(plugin));
            int removed = 0;
            foreach (List<DiscordSubscription> value in _subscriptions.Values)
            {
                removed += value.RemoveAll(s => s.Plugin == plugin);
            }
            
            _logger.Debug($"{nameof(DiscordSubscriptions)}.{nameof(RemovePluginSubscriptions)} Removed {removed} subscriptions for plugin {plugin.Name}");

            CleanupEmpty();
            
            if(_pluginRemovedFromManager.TryGetValue(plugin, out Event.Callback<Plugin, PluginManager> callback))
            {
                callback.Remove();
                _pluginRemovedFromManager.Remove(plugin);
            }
        }
        
        internal void HandleMessage(DiscordMessage message, Channel channel)
        {
            List<DiscordSubscription> subs = _subscriptions[message.ChannelId];
            if (subs != null)
            {
                HandleSubs(subs, message);
            }

            if (channel?.ParentId != null)
            {
                subs = _subscriptions[channel.ParentId.Value];
                if (subs != null)
                {
                    HandleSubs(subs, message);
                }
            }
        }

        private void HandleSubs(List<DiscordSubscription> subs, DiscordMessage message)
        {
            bool isLoaded = true;
            foreach (DiscordSubscription sub in subs)
            {
                if (!sub.Plugin.IsLoaded)
                {
                    isLoaded = false;
                }
                    
                sub.Invoke(message);
            }

            if (!isLoaded)
            {
                subs.RemoveAll(s => !s.Plugin.IsLoaded);
                CleanupEmpty();
            }
        }

        private void CleanupEmpty()
        {
            _subscriptions.RemoveAll(s => s.Count == 0);
        }
    }
}