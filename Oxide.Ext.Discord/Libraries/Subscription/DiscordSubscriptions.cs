using System;
using System.Collections.Generic;
using Oxide.Core.Libraries;
using Oxide.Core.Plugins;
using Oxide.Ext.Discord.Entities;
using Oxide.Ext.Discord.Entities.Channels;
using Oxide.Ext.Discord.Entities.Messages;
using Oxide.Ext.Discord.Exceptions.Entities;
using Oxide.Ext.Discord.Extensions;
using Oxide.Ext.Discord.Interfaces.Logging;
using Oxide.Ext.Discord.Logging;
using Oxide.Ext.Discord.Plugins;
using Oxide.Plugins;

namespace Oxide.Ext.Discord.Libraries.Subscription
{
    /// <summary>
    /// Represents Discord Subscriptions Oxide Library
    /// Allows for plugins to subscribe to discord channels
    /// </summary>
    public class DiscordSubscriptions : BaseDiscordLibrary<DiscordSubscriptions>, IDebugLoggable
    {
        private readonly Hash<Snowflake, Hash<PluginId, DiscordSubscription>> _subscriptions = new Hash<Snowflake, Hash<PluginId, DiscordSubscription>>();

        private readonly ILogger _logger;
        
        /// <summary>
        /// DiscordSubscriptions Constructor
        /// </summary>
        /// <param name="logger">Logger</param>
        internal DiscordSubscriptions(ILogger logger)
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
        /// <param name="client">Client that is subscribing</param>
        /// <param name="channelId">Channel ID of the channel</param>
        /// <param name="message">Callback with the message that was created in the channel</param>
        /// <exception cref="ArgumentNullException">Exception if plugin or message is null</exception>
        /// <exception cref="ArgumentException">Exception if Channel ID is not valid</exception>
        [LibraryFunction(nameof(AddChannelSubscription))]
        public void AddChannelSubscription(DiscordClient client, Snowflake channelId, Action<DiscordMessage> message)
        {
            InvalidSnowflakeException.ThrowIfInvalid(channelId, nameof(channelId));
            if (client == null) throw new ArgumentNullException(nameof(client));
            if (message == null) throw new ArgumentNullException(nameof(message));

            Plugin plugin = client.Plugin;
            _logger.Debug($"{nameof(DiscordSubscriptions)}.{nameof(AddChannelSubscription)} {{0}} added subscription to channel {{1}}", plugin.FullName(), channelId);

            Hash<PluginId, DiscordSubscription> channelSubs = _subscriptions[channelId];
            if (channelSubs == null)
            {
                channelSubs = new Hash<PluginId, DiscordSubscription>();
                _subscriptions[channelId] = channelSubs;
            }

            channelSubs[plugin.Id()] = new DiscordSubscription(client, channelId, message);
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
            if (plugin == null) throw new ArgumentNullException(nameof(plugin));
            InvalidSnowflakeException.ThrowIfInvalid(channelId, nameof(channelId));
            
            Hash<PluginId, DiscordSubscription> pluginSubs = _subscriptions[channelId];
            if (pluginSubs == null)
            {
                return;
            }

            pluginSubs.Remove(plugin.Id());
            
            if (pluginSubs.Count == 0)
            {
                _subscriptions.Remove(channelId);
            }
            
            _logger.Debug($"{nameof(DiscordSubscriptions)}.{nameof(RemoveChannelSubscription)} {{0}} removed subscription to channel {{1}}", plugin.Id(), channelId);
        }

        ///<inheritdoc/>
        protected override void OnPluginLoaded(PluginData data) { }

        ///<inheritdoc/>
        protected override void OnPluginUnloaded(Plugin plugin)
        {
            RemovePluginSubscriptions(plugin);
        }
        
        /// <summary>
        /// Remove all subscriptions for a plugin
        /// </summary>
        /// <param name="plugin">Plugin to remove subscriptions for</param>
        /// <exception cref="ArgumentNullException">Exception if plugin is null</exception>
        public void RemovePluginSubscriptions(Plugin plugin)
        {
            if (plugin == null) throw new ArgumentNullException(nameof(plugin));

            List<Snowflake> emptySubs = new List<Snowflake>();

            int removed = 0;
            foreach (KeyValuePair<Snowflake, Hash<PluginId, DiscordSubscription>> hash in _subscriptions)
            {
                DiscordSubscription sub = hash.Value[plugin.Id()];
                if (sub != null)
                {
                    hash.Value.Remove(plugin.Id());
                    sub.OnRemoved();
                    removed++;
                    if (hash.Value.Count == 0)
                    {
                        emptySubs.Add(hash.Key);
                    }
                }
            }

            if (emptySubs.Count != 0)
            {
                for (int i = 0; i < emptySubs.Count; i++)
                {
                    Snowflake emptySub = emptySubs[i];
                    _subscriptions.Remove(emptySub);
                }
            }

            _logger.Debug($"{nameof(DiscordSubscriptions)}.{nameof(RemovePluginSubscriptions)} Removed {{0}} subscriptions for plugin {{1}}", removed, plugin.FullName());
        }
        
        internal void HandleMessage(DiscordMessage message, DiscordChannel channel, BotClient client)
        {
            RunSubs(_subscriptions[message.ChannelId], message, client);

            if (channel.ParentId != null)
            {
                RunSubs(_subscriptions[channel.ParentId.Value], message, client);
            }
        }

        private void RunSubs(Hash<PluginId, DiscordSubscription> subs, DiscordMessage message, BotClient client)
        {
            if (subs == null)
            {
                return;
            }
            
            foreach (DiscordSubscription sub in subs.Values)
            {
                if (sub.CanRun(client))
                {
                    sub.Invoke(message);
                }
            }
        }

        ///<inheritdoc/>
        public void LogDebug(DebugLogger logger)
        {
            logger.AppendList("Subscriptions", GetSubscriptions());
        }
        
        internal IEnumerable<DiscordSubscription> GetSubscriptions()
        {
            foreach (Hash<PluginId, DiscordSubscription> pluginSubscriptions in _subscriptions.Values)
            {
                foreach (DiscordSubscription subscription in pluginSubscriptions.Values)
                {
                    yield return subscription;
                }
            }
        }
    }
}