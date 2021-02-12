using System;
using Oxide.Core.Libraries.Covalence;
using Oxide.Ext.Discord.Entities;
using Oxide.Ext.Discord.Entities.Users;
using Oxide.Plugins;

namespace Oxide.Ext.Discord.Libraries.Linking
{
    /// <summary>
    /// Represents a plugin that supports Discord Link library
    /// </summary>
    public interface IDiscordLinkPlugin
    {
        /// <summary>
        /// Returns a Hash of Steam ID's to Discord ID's
        /// </summary>
        /// <returns></returns>
        Hash<string, Snowflake> GetSteamToDiscordIds();
        
        /// <summary>
        /// Returns a Hash of Discord ID's to Steam ID's
        /// </summary>
        /// <returns></returns>
        Hash<Snowflake, string> GetDiscordToSteamIds();

        /// <summary>
        /// Called on a plugin to register events to be called when that plugin links or unlinks a Steam ID and Discord ID
        /// </summary>
        /// <param name="onLinked"></param>
        /// <param name="onUnlinked"></param>
        void RegisterEvents(Action<IPlayer, DiscordUser> onLinked, Action<IPlayer, DiscordUser> onUnlinked);
    }
}