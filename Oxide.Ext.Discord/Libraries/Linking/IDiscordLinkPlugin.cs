using System.Collections.Generic;
using Oxide.Ext.Discord.Entities;
using Oxide.Ext.Discord.Interfaces;

namespace Oxide.Ext.Discord.Libraries.Linking
{
    /// <summary>
    /// Represents a plugin that supports Discord Link library
    /// </summary>
    public interface IDiscordLinkPlugin : IDiscordPlugin
    {
        /// <summary>
        /// Returns a <see cref="IDictionary{TKey,TValue}"/> of Steam ID's to Discord ID's
        /// </summary>
        /// <returns></returns>
        IDictionary<PlayerId, Snowflake> GetPlayerIdToDiscordIds();
    }
}