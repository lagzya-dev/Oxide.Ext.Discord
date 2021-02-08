using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Oxide.Ext.Discord.Entities.Users;

namespace Oxide.Ext.Discord.Entities.Gatway.Events
{
    /// <summary>
    /// Represents <a href="https://discord.com/developers/docs/topics/gateway#guild-member-update">Guild Member Update</a>
    /// </summary>
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class GuildMemberUpdateEvent
    {
        /// <summary>
        /// The id of the guild
        /// </summary>
        [JsonProperty("guild_id")]
        public Snowflake GuildId { get; set; }
        
        /// <summary>
        /// User role ids
        /// </summary>
        [JsonProperty("roles")]
        public List<string> Roles { get; set; }
        
        /// <summary>
        /// The user
        /// </summary>
        [JsonProperty("user")]
        public DiscordUser User { get; set; }
        
        /// <summary>
        /// Nickname of the user in the guild
        /// </summary>
        [JsonProperty("nick")]
        public string Nick { get; set; }
        
        /// <summary>
        /// When the user joined the guild
        /// </summary>
        [JsonProperty("joined_at")]
        public DateTime JoinedAt { get; set; }
        
        /// <summary>
        /// When the user starting boosting the guild
        /// </summary>
        [JsonProperty("premium_since")]
        public DateTime? PremiumSince { get; set; }
        
        /// <summary>
        /// Whether the user has not yet passed the guild's Membership Screening requirements
        /// </summary>
        [JsonProperty("pending")]
        public bool? Pending { get; set; }
    }
}
