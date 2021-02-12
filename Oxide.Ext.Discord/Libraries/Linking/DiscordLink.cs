using System.Collections.Generic;
using System.Linq;
using Oxide.Core;
using Oxide.Core.Libraries;
using Oxide.Core.Libraries.Covalence;
using Oxide.Core.Plugins;
using Oxide.Ext.Discord.Entities;
using Oxide.Ext.Discord.Entities.Users;
using Oxide.Plugins;

namespace Oxide.Ext.Discord.Libraries.Linking
{
    /// <summary>
    /// Represents a library for discord linking
    /// </summary>
    public class DiscordLink : Library
    {
        /// <summary>
        /// Linking Plugin
        /// </summary>
        public IDiscordLinkPlugin Link { get; private set; }

        private readonly IPlayerManager _players;

        private Hash<string, Snowflake> _steamIdToDiscordId;
        private Hash<Snowflake, string> _discordIdToSteamId;

        private Plugin _linkPlugin;

        private Plugin LinkPlugin
        {
            get => _linkPlugin;
            set
            {
                _linkPlugin = value;
                Link = (IDiscordLinkPlugin) value;
            }
        }

        /// <summary>
        /// Constructor for discord link
        /// </summary>
        public DiscordLink()
        {
            _players = Interface.Oxide.GetLibrary<Covalence>().Players;
        }

        /// <summary>
        /// Returns if there is a registered link plugin
        /// </summary>
        /// <returns></returns>
        [LibraryFunction(nameof(IsEnabled))]
        public bool IsEnabled()
        {
            return Link != null;
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
                Interface.Oxide.LogWarning("[Discord Link] Tried to register a Discord Link Plugin that does not inherit from ILinkPlugin: {0}", plugin.Title);
                return;
            }

            if (LinkPlugin != null)
            {
                Interface.Oxide.LogWarning("[Discord Link] Plugin has been overriden by {0}, Previously {1}", plugin.Title, LinkPlugin.Title);
            }

            LinkPlugin = plugin;
            LinkPlugin.OnRemovedFromManager.Add(RemovePlugin);
            Link.RegisterEvents(OnLinked, OnUnlinked);
            _steamIdToDiscordId = Link.GetSteamToDiscordIds();
            _discordIdToSteamId = Link.GetDiscordToSteamIds();
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
        /// <param name="id">ID of the steam or discord ID. Valid Types are String, Ulong, Snowflake</param>
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

            return _players.FindPlayerById(id);
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
            return GetSteamToDiscordIds()?[player.Id];
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
        [LibraryFunction(nameof(GetLinkedSteamIds))]
        public List<string> GetLinkedSteamIds()
        {
            return GetSteamToDiscordIds()?.Keys.ToList() ?? new List<string>();
        }

        /// <summary>
        /// Returns Discord ID's for all linked players
        /// </summary>
        /// <returns></returns>
        [LibraryFunction(nameof(GetLinkedDiscordIds))]
        public List<Snowflake> GetLinkedDiscordIds()
        {
            return GetDiscordToSteamIds()?.Keys.ToList() ?? new List<Snowflake>();
        }

        /// <summary>
        /// Returns a Hash with a Steam ID key and Discord ID value
        /// </summary>
        /// <returns></returns>
        [LibraryFunction(nameof(GetSteamToDiscordIds))]
        public Hash<string, Snowflake> GetSteamToDiscordIds()
        {
            return _steamIdToDiscordId ?? (_steamIdToDiscordId = Link?.GetSteamToDiscordIds());
        }

        /// <summary>
        /// Returns a Hash with a Discord ID key and Steam ID value
        /// </summary>
        /// <returns></returns>
        [LibraryFunction(nameof(GetDiscordToSteamIds))]
        public Hash<Snowflake, string> GetDiscordToSteamIds()
        {
            return _discordIdToSteamId ?? (_discordIdToSteamId = Link?.GetDiscordToSteamIds());
        }

        private void OnLinked(IPlayer player, DiscordUser discord)
        {
            _discordIdToSteamId[discord.Id] = player.Id;
            _steamIdToDiscordId[player.Id] = discord.Id;
            DiscordClient.GlobalCallHook("Discord_OnLinked", player, discord);
        }

        private void OnUnlinked(IPlayer player, DiscordUser discord)
        {
            _discordIdToSteamId.Remove(discord.Id);
            _steamIdToDiscordId.Remove(player.Id);
            DiscordClient.GlobalCallHook("Discord_OnUnlinked", player, discord);
        }
    }
}