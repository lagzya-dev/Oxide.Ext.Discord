using System.Collections.Generic;
using Oxide.Core;
using Oxide.Core.Libraries;
using Oxide.Core.Libraries.Covalence;
using Oxide.Core.Plugins;
using Oxide.Ext.Discord.Constants;
using Oxide.Ext.Discord.Entities;
using Oxide.Ext.Discord.Entities.Guilds;
using Oxide.Ext.Discord.Entities.Users;
using Oxide.Plugins;

namespace Oxide.Ext.Discord.Libraries.Linking
{
    /// <summary>
    /// Represents a library for discord linking
    /// </summary>
    public class DiscordLink : Library
    {
        internal IPlayerManager Players => _players ?? (_players = Interface.Oxide.GetLibrary<Covalence>().Players);
        private IPlayerManager _players;

        private readonly Hash<string, Snowflake> _steamIdToDiscordId = new Hash<string, Snowflake>();
        private readonly Hash<Snowflake, string> _discordIdToSteamId = new Hash<Snowflake, string>();
        private readonly HashSet<string> _steamIds = new HashSet<string>();
        private readonly HashSet<Snowflake> _discordIds = new HashSet<Snowflake>();
        
        private Plugin LinkPlugin { get; set; }

        private Event.Callback<Plugin, PluginManager> _onRemoved;

        /// <summary>
        /// Returns if there is a registered link plugin
        /// </summary>
        /// <returns></returns>
        [LibraryFunction(nameof(IsEnabled))]
        public bool IsEnabled()
        {
            return LinkPlugin != null;
        }

        /// <summary>
        /// Adds a link plugin to be the plugin used with the Discord Link library
        /// </summary>
        /// <param name="plugin"></param>
        [LibraryFunction(nameof(AddLinkPlugin))]
        public void AddLinkPlugin(Plugin plugin)
        {
            IDiscordLinkPlugin link = plugin as IDiscordLinkPlugin;
            if (link == null)
            {
                Interface.Oxide.LogWarning("[Discord Link] Tried to register a Discord Link Plugin that does not inherit from IDiscordLinkPlugin: {0}", plugin.Title);
                return;
            }

            if (LinkPlugin != null)
            {
                _onRemoved.Remove();
                Interface.Oxide.LogWarning("[Discord Link] Plugin has been overriden by {0}, Previously {1}", plugin.Title, LinkPlugin.Title);
            }

            LinkPlugin = plugin;
            _onRemoved = LinkPlugin.OnRemovedFromManager.Add(RemovePlugin);
            link.RegisterEvents(OnLinked, OnUnlinked);

            Hash<string, Snowflake> data = link.GetSteamToDiscordIds();
            if (data != null)
            {
                _steamIdToDiscordId.Clear();
                _discordIdToSteamId.Clear();
                foreach (KeyValuePair<string,Snowflake> pair in data)
                {
                    _steamIdToDiscordId[pair.Key] = pair.Value;
                    _discordIdToSteamId[pair.Value] = pair.Key;
                    _steamIds.Add(pair.Key);
                    _discordIds.Add(pair.Value);
                }
            }
        }

        private void RemovePlugin(Plugin plugin, PluginManager manager)
        {
            if (plugin == LinkPlugin)
            {
                LinkPlugin = null;
            }
        }

        /// <summary>
        /// Returns if the specified ID is linked
        /// </summary>
        /// <param name="id">ID of the steam or discord ID. Valid Types are string, ulong, and Snowflake</param>
        /// <returns>True if the ID is linked; false otherwise</returns>
        [LibraryFunction(nameof(IsLinked))]
        public bool IsLinked(object id)
        {
            string checkId = id.ToString();
            Snowflake discordId = GetDiscordId(checkId) ?? default(Snowflake);
            if (discordId != default(Snowflake))
            {
                return true;
            }

            if (Snowflake.TryParse(checkId, out discordId))
            {
                return GetSteamId(discordId) != null;
            }

            return false;
        }
        
        /// <summary>
        /// Returns if the specified ID is linked
        /// </summary>
        /// <param name="steamId">Steam ID of the player</param>
        /// <returns>True if the ID is linked; false otherwise</returns>
        public bool IsLinked(string steamId)
        {
            return GetSteamToDiscordIds()?.ContainsKey(steamId) ?? false;
        }
        
        /// <summary>
        /// Returns if the specified ID is linked
        /// </summary>
        /// <param name="discordId">Discord ID of the player</param>
        /// <returns>True if the ID is linked; false otherwise</returns>
        public bool IsLinked(Snowflake discordId)
        {
            return GetDiscordToSteamIds()?.ContainsKey(discordId) ?? false;
        }

        /// <summary>
        /// Returns the Steam ID of the given Discord ID if there is a link
        /// </summary>
        /// <param name="discordId"></param>
        /// <returns>Steam ID of the given given discord ID if linked; null otherwise</returns>
        [LibraryFunction(nameof(GetSteamId))]
        public string GetSteamId(Snowflake discordId)
        {
            return GetDiscordToSteamIds()?[discordId];
        }

        /// <summary>
        /// Returns the IPlayer for the given Discord ID
        /// </summary>
        /// <param name="discordId">Discord ID to get IPlayer for</param>
        /// <returns>IPlayer for the given Discord ID; null otherwise</returns>
        [LibraryFunction(nameof(GetPlayer))]
        public IPlayer GetPlayer(Snowflake discordId)
        {
            string id = GetSteamId(discordId);
            if (string.IsNullOrEmpty(id))
            {
                return null;
            }

            return Players.FindPlayerById(id);
        }

        /// <summary>
        /// Returns the Discord ID for the given Steam ID
        /// </summary>
        /// <param name="steamId">Steam ID to get Discord ID for</param>
        /// <returns>Discord ID for the given Steam ID; null otherwise</returns>
        [LibraryFunction(nameof(GetDiscordId))]
        public Snowflake? GetDiscordId(string steamId)
        {
            return GetSteamToDiscordIds()?[steamId];
        }

        /// <summary>
        /// Returns the Discord ID for the given IPlayer
        /// </summary>
        /// <param name="player">Player to get Discord ID for</param>
        /// <returns>Discord ID for the given Steam ID; null otherwise</returns>
        public Snowflake? GetDiscordId(IPlayer player)
        {
            return GetDiscordId(player.Id);
        }

        /// <summary>
        /// Returns a minimal Discord User
        /// </summary>
        /// <param name="steamId">ID of the in game player</param>
        /// <returns>Discord ID for the given Steam ID; null otherwise</returns>
        [LibraryFunction(nameof(GetDiscordUser))]
        public DiscordUser GetDiscordUser(string steamId)
        {
            Snowflake? discordId = GetDiscordId(steamId);
            if (!discordId.HasValue)
            {
                return null;
            }

            return new DiscordUser
            {
                Id = discordId.Value,
                Bot = false,
            };
        }
        
        /// <summary>
        /// Returns a minimal Discord User
        /// </summary>
        /// <param name="player">Player to get the Discord User for</param>
        /// <returns>Discord ID for the given Steam ID; null otherwise</returns>
        public DiscordUser GetDiscordUser(IPlayer player)
        {
            return GetDiscordUser(player.Id);
        }

        /// <summary>
        /// Returns a minimal Discord User
        /// </summary>
        /// <param name="steamId">ID of the in game player</param>
        /// <param name="guild">Guild the member is in</param>
        /// <returns>Discord ID for the given Steam ID; null otherwise</returns>
        [LibraryFunction(nameof(GetLinkedMember))]
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
        /// Returns a minimal Discord User
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
        [LibraryFunction(nameof(GetLinkedCount))]
        public int GetLinkedCount()
        {
            return GetSteamToDiscordIds()?.Count ?? -1;
        }

        /// <summary>
        /// Returns Steam ID's for all linked players
        /// </summary>
        /// <returns></returns>
        [LibraryFunction(nameof(GetSteamIds))]
        public HashSet<string> GetSteamIds()
        {
            return _steamIds;
        }

        /// <summary>
        /// Returns Discord ID's for all linked players
        /// </summary>
        /// <returns></returns>
        [LibraryFunction(nameof(GetDiscordIds))]
        public HashSet<Snowflake> GetDiscordIds()
        {
            return _discordIds;
        }

        /// <summary>
        /// Returns a Hash with a Steam ID key and Discord ID value
        /// </summary>
        /// <returns></returns>
        [LibraryFunction(nameof(GetSteamToDiscordIds))]
        public Hash<string, Snowflake> GetSteamToDiscordIds()
        {
            return _steamIdToDiscordId;
        }

        /// <summary>
        /// Returns a Hash with a Discord ID key and Steam ID value
        /// </summary>
        /// <returns></returns>
        [LibraryFunction(nameof(GetDiscordToSteamIds))]
        public Hash<Snowflake, string> GetDiscordToSteamIds()
        {
            return _discordIdToSteamId;
        }

        private void OnLinked(IPlayer player, DiscordUser discord)
        {
            _discordIdToSteamId[discord.Id] = player.Id;
            _steamIdToDiscordId[player.Id] = discord.Id;
            _steamIds.Remove(player.Id);
            _discordIds.Remove(discord.Id);
            DiscordClient.GlobalCallHook(DiscordHooks.OnDiscordPlayerLinked, player, discord);
        }

        private void OnUnlinked(IPlayer player, DiscordUser discord)
        {
            _discordIdToSteamId.Remove(discord.Id);
            _steamIdToDiscordId.Remove(player.Id);
            DiscordClient.GlobalCallHook(DiscordHooks.OnDiscordPlayerUnlinked, player, discord);
        }
    }
}