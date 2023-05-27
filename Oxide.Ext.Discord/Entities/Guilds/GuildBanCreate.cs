using System;
using Newtonsoft.Json;
using Oxide.Ext.Discord.Exceptions.Entities.Guild;
using Oxide.Ext.Discord.Interfaces;

namespace Oxide.Ext.Discord.Entities.Guilds
{
    /// <summary>
    /// Represents <a href="https://discord.com/developers/docs/resources/guild#create-guild-ban">Guild Ban Create Structure</a>
    /// </summary>
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class GuildBanCreate : IDiscordValidation
    {
        /// <summary>
        /// Number of days to delete messages for (0-7)
        /// </summary>
        [Obsolete("This has been deprecated by discord and will be removed in a future version. Please use DeleteMessageSeconds field instead.")]
        [JsonProperty("delete_message_days")]
        public int? DeleteMessageDays { get; set; }
        
        /// <summary>
        /// Number of seconds to delete messages for, between 0 and 604800 (7 days)
        /// </summary>
        [JsonProperty("delete_message_seconds")]
        public int? DeleteMessageSeconds { get; set; }

        ///<inheritdoc/>
        public void Validate()
        {
            InvalidGuildBanException.ThrowIfInvalidDeleteMessageDays(DeleteMessageDays);
            InvalidGuildBanException.ThrowIfInvalidDeleteMessageSeconds(DeleteMessageSeconds);
        }
    }
}