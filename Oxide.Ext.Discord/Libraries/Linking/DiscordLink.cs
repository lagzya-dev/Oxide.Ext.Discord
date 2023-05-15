using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Oxide.Core.Libraries.Covalence;
using Oxide.Core.Plugins;
using Oxide.Ext.Discord.Cache;
using Oxide.Ext.Discord.Constants;
using Oxide.Ext.Discord.Entities;
using Oxide.Ext.Discord.Entities.Guilds;
using Oxide.Ext.Discord.Entities.Users;
using Oxide.Ext.Discord.Extensions;
using Oxide.Ext.Discord.Hooks;
using Oxide.Ext.Discord.Interfaces.Logging;
using Oxide.Ext.Discord.Logging;
using Oxide.Ext.Discord.Types;
using Oxide.Plugins;

namespace Oxide.Ext.Discord.Libraries.Linking
{
    /// <summary>
    /// Represents a library for discord linking
    /// </summary>
    public class DiscordLink : BaseDiscordLibrary<DiscordLink>, IDebugLoggable
    {
        public readonly ReadOnlyDictionary<PlayerId, Snowflake> SteamToDiscordIds;
        public readonly ReadOnlyDictionary<Snowflake, PlayerId> DiscordIdToSteamId;
        public readonly ReadonlySet<PlayerId> SteamIds;
        public readonly ReadonlySet<Snowflake> DiscordIds;
        
        private readonly Hash<PlayerId, Snowflake> _steamIdToDiscordId = new Hash<PlayerId, Snowflake>();
        private readonly Hash<Snowflake, PlayerId> _discordIdToSteamId = new Hash<Snowflake, PlayerId>();
        private readonly HashSet<PlayerId> _steamIds = new HashSet<PlayerId>();
        private readonly HashSet<Snowflake> _discordIds = new HashSet<Snowflake>();
        private readonly Hash<string, IDictionary<string, Snowflake>> _pluginLinks = new Hash<string, IDictionary<string, Snowflake>>();
        private readonly List<IDiscordLinkPlugin> _linkPlugins = new List<IDiscordLinkPlugin>();

        private readonly ILogger _logger;

        /// <summary>
        /// DiscordLink Constructor
        /// </summary>
        /// <param name="logger">Logger for Discord Link</param>
        internal DiscordLink(ILogger logger)
        {
            _logger = logger;
            SteamToDiscordIds = new ReadOnlyDictionary<PlayerId, Snowflake>(_steamIdToDiscordId);
            DiscordIdToSteamId = new ReadOnlyDictionary<Snowflake, PlayerId>(_discordIdToSteamId);
            SteamIds = new ReadonlySet<PlayerId>(_steamIds);
            DiscordIds = new ReadonlySet<Snowflake>(_discordIds);
        }

        /// <summary>
        /// Returns if there is a registered link plugin
        /// </summary>
        /// <returns></returns>
        public bool IsEnabled()
        {
            return _linkPlugins.Count != 0;
        }

        /// <summary>
        /// Adds a link plugin to be the plugin used with the Discord Link library
        /// </summary>
        /// <param name="plugin"></param>
        public void AddLinkPlugin(IDiscordLinkPlugin plugin)
        {
            if (plugin == null) throw new ArgumentNullException(nameof(plugin));

            if (!_linkPlugins.Contains(plugin))
            {
                _linkPlugins.Add(plugin);
            }

            IDictionary<string, Snowflake> data = plugin.GetSteamToDiscordIds();
            if (data == null)
            {
                _logger.Error($"{{0}} returned null when {nameof(plugin.GetSteamToDiscordIds)} was called", plugin.Name);
                return;
            }

            _pluginLinks[plugin.Name] = data;

            foreach (KeyValuePair<string,Snowflake> pair in data)
            {
                AddLink(new PlayerId(pair.Key), pair.Value);
            }
            
            _logger.Debug("{0} has been registered as a DiscordLink plugin", plugin.Name);
        }

        /// <summary>
        /// Removes a link plugin from the Discord Link library
        /// </summary>
        /// <param name="plugin"></param>
        public void RemoveLinkPlugin(IDiscordLinkPlugin plugin)
        {
            if (plugin == null) throw new ArgumentNullException(nameof(plugin));

            IDictionary<string, Snowflake> pluginData = _pluginLinks[plugin.Name];
            if (pluginData != null)
            {
                foreach (KeyValuePair<string,Snowflake> link in pluginData)
                {
                    RemoveLink(new PlayerId(link.Key), link.Value);
                }
            }
            
            _linkPlugins.Remove(plugin);
        }

        ///<inheritdoc/>
        protected override void OnPluginLoaded(Plugin plugin) { }

        ///<inheritdoc/>
        protected override void OnPluginUnloaded(Plugin plugin)
        {
            // ReSharper disable once SuspiciousTypeConversion.Global
            if (plugin is IDiscordLinkPlugin link)
            {
                RemoveLinkPlugin(link);
            }
        }

        /// <summary>
        /// Returns if the specified ID is linked
        /// </summary>
        /// <param name="steamId">Steam ID of the player</param>
        /// <returns>True if the ID is linked; false otherwise</returns>
        public bool IsLinked(string steamId)
        {
            return _steamIds.Contains(new PlayerId(steamId));
        }
        
        /// <summary>
        /// Returns if the specified ID is linked
        /// </summary>
        /// <param name="discordId">Discord ID of the player</param>
        /// <returns>True if the ID is linked; false otherwise</returns>
        public bool IsLinked(Snowflake discordId)
        {
            return _discordIds.Contains(discordId);
        }
        
        /// <summary>
        /// Returns if the specified ID is linked
        /// </summary>
        /// <param name="player">Player to check if linked</param>
        /// <returns>True if the player is linked; false otherwise</returns>
        public bool IsLinked(IPlayer player) => IsLinked(player.Id);

        /// <summary>
        /// Returns if the specified ID is linked
        /// </summary>
        /// <param name="user">Discord user to check</param>
        /// <returns>True if the user is linked; false otherwise</returns>
        public bool IsLinked(DiscordUser user) => IsLinked(user.Id);

        /// <summary>
        /// Returns the Steam ID of the given Discord ID if there is a link
        /// </summary>
        /// <param name="discordId">Discord ID to get steam ID for</param>
        /// <returns>Steam ID of the given given discord ID if linked; null otherwise</returns>
        public string GetSteamId(Snowflake discordId) => _discordIdToSteamId[discordId].Id;

        /// <summary>
        /// Returns the Steam ID of the given Discord ID if there is a link
        /// </summary>
        /// <param name="user"><see cref="DiscordUser"/> to get steam Id for</param>
        /// <returns>Steam ID of the given given discord ID if linked; null otherwise</returns>
        public string GetSteamId(DiscordUser user) => GetSteamId(user.Id);

        /// <summary>
        /// Returns the IPlayer for the given Discord ID
        /// </summary>
        /// <param name="discordId">Discord ID to get IPlayer for</param>
        /// <returns>IPlayer for the given Discord ID; null otherwise</returns>
        public IPlayer GetPlayer(Snowflake discordId)
        {
            string id = GetSteamId(discordId);
            if (string.IsNullOrEmpty(id))
            {
                return null;
            }

            return ServerPlayerCache.Instance.GetPlayer(id);
        }

        /// <summary>
        /// Returns the Discord ID for the given Steam ID
        /// </summary>
        /// <param name="steamId">Steam ID to get Discord ID for</param>
        /// <returns>Discord ID for the given Steam ID; null otherwise</returns>
        public Snowflake? GetDiscordId(string steamId)
        {
            return _steamIdToDiscordId.TryGetValue(new PlayerId(steamId), out Snowflake id) ? id : new Snowflake?();
        }

        /// <summary>
        /// Returns the Discord ID for the given IPlayer
        /// </summary>
        /// <param name="player">Player to get Discord ID for</param>
        /// <returns>Discord ID for the given Steam ID; null otherwise</returns>
        public Snowflake? GetDiscordId(IPlayer player) => GetDiscordId(player.Id);

        /// <summary>
        /// Returns a minimal Discord User
        /// </summary>
        /// <param name="steamId">ID of the in game player</param>
        /// <returns>Discord ID for the given Steam ID; null otherwise</returns>
        public DiscordUser GetDiscordUser(string steamId)
        {
            Snowflake? discordId = GetDiscordId(steamId);
            if (!discordId.HasValue)
            {
                return null;
            }

            return DiscordUserCache.Instance.GetOrCreate(discordId.Value);
        }
        
        /// <summary>
        /// Returns a minimal Discord User
        /// </summary>
        /// <param name="player">Player to get the Discord User for</param>
        /// <returns>Discord ID for the given Steam ID; null otherwise</returns>
        public DiscordUser GetDiscordUser(IPlayer player) => GetDiscordUser(player.Id);

        /// <summary>
        /// Returns a linked guild member for the matching steam id in the given guild
        /// </summary>
        /// <param name="steamId">ID of the in game player</param>
        /// <param name="guild">Guild the member is in</param>
        /// <returns>Discord ID for the given Steam ID; null otherwise</returns>
        public GuildMember GetLinkedMember(string steamId, DiscordGuild guild)
        {
            Snowflake? discordId = GetDiscordId(steamId);
            if (!discordId.HasValue || !guild.IsAvailable)
            {
                return null;
            }

            return guild.Members[discordId.Value];
        }

        /// <summary>
        /// Returns a linked guild member for the matching <see cref="IPlayer"/> in the given guild
        /// </summary>
        /// <param name="player">Player to get the Discord User for</param>
        /// <param name="guild">Guild the member is in</param>
        /// <returns>Discord ID for the given Steam ID; null otherwise</returns>
        public GuildMember GetLinkedMember(IPlayer player, DiscordGuild guild)
        {
            return GetLinkedMember(player.Id, guild);
        }

        /// <summary>
        /// Returns the number of linked players
        /// </summary>
        /// <returns></returns>
        public int GetLinkedCount() => _steamIds.Count;

        /// <summary>
        /// Returns Steam ID's for all linked players
        /// </summary>
        /// <returns></returns>
        public IEnumerable<string> GetSteamIds()
        {
            foreach (PlayerId id in _steamIds)
            {
                yield return id.Id;
            }
        }

        /// <summary>
        /// Called by a link plugin when a link occured
        /// </summary>
        /// <param name="plugin">Plugin that initiated the link</param>
        /// <param name="player">Player being linked</param>
        /// <param name="discord">DiscordUser being linked</param>
        public void OnLinked(Plugin plugin, IPlayer player, DiscordUser discord)
        {
            if (player == null) throw new ArgumentNullException(nameof(player));
            if (discord == null) throw new ArgumentNullException(nameof(discord));

            if (!IsValidLinkPlugin(plugin))
            {
                return;
            }
            
            _pluginLinks[plugin.Title][player.Id] = discord.Id;
            AddLink(new PlayerId(player.Id), discord.Id);
            
            DiscordHook.CallGlobalHook(DiscordExtHooks.OnDiscordPlayerLinked, player, discord);
        }

        /// <summary>
        /// Called by a link plugin when an unlink occured
        /// </summary>
        /// <param name="plugin">Plugin that is unlinking</param>
        /// <param name="player">Player being unlinked</param>
        /// <param name="discord">DiscordUser being unlinked</param>
        public void OnUnlinked(Plugin plugin, IPlayer player, DiscordUser discord)
        {
            if (player == null) throw new ArgumentNullException(nameof(player));
            if (discord == null) throw new ArgumentNullException(nameof(discord));
            
            if (!IsValidLinkPlugin(plugin))
            {
                return;
            }
            
            DiscordHook.CallGlobalHook(DiscordExtHooks.OnDiscordPlayerUnlink, player, discord);
            RemoveLink(new PlayerId(player.Id), discord.Id);
            _pluginLinks[plugin.Title].Remove(player.Id);
            DiscordHook.CallGlobalHook(DiscordExtHooks.OnDiscordPlayerUnlinked, player, discord);
        }
        
        private void AddLink(PlayerId playerId, Snowflake userId)
        {
            _steamIdToDiscordId[playerId] = userId;
            _discordIdToSteamId[userId] = playerId;
            _steamIds.Add(playerId);
            _discordIds.Add(userId);
        }
        
        private void RemoveLink(PlayerId playerId, Snowflake userId)
        {
            _steamIdToDiscordId.Remove(playerId);
            _discordIdToSteamId.Remove(userId);
            _steamIds.Remove(playerId);
            _discordIds.Remove(userId);
        }
        
        private bool IsValidLinkPlugin(Plugin plugin)
        {
            IDiscordLinkPlugin link = plugin as IDiscordLinkPlugin;
            if (link == null)
            {
                _logger.Error($"{plugin.Name} tried to link but is not registered as a link plugin");
                return false;
            }

            if (!_linkPlugins.Contains(link))
            {
                _logger.Error($"{plugin.Name} has not been added as a link plugin and cannot set a link");
                return false;
            }
            
            return true;
        }

        public void LogDebug(DebugLogger logger)
        {
            logger.AppendField("Total Links", _steamIds.Count);
            logger.StartArray("Plugins");
            foreach (string plugin in _pluginLinks.Keys)
            {
                logger.AppendField("Plugin", plugin);
            }
            logger.EndArray();
        }
    }
}