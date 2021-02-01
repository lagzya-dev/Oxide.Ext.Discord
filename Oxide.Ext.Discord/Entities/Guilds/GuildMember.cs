using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Oxide.Ext.Discord.Entities.Users;

namespace Oxide.Ext.Discord.Entities.Guilds
{
    /// <summary>
    /// Represents <a href="https://discord.com/developers/docs/resources/guild#guild-member-object-guild-member-structure">Guild Member Structure</a>
    /// </summary>
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class GuildMember
    {
        /// <summary>
        /// The user this guild member represents
        /// </summary>
        [JsonProperty("user")]
        public DiscordUser User { get; set; }

        /// <summary>
        /// This users guild nickname
        /// </summary>
        [JsonProperty("nick")]
        public string Nick { get; set; }

        /// <summary>
        /// List of member roles
        /// </summary>
        [JsonProperty("roles")]
        public List<string> Roles { get; set; }

        /// <summary>
        /// When the user joined the guild
        /// </summary>
        [JsonProperty("joined_at")]
        public DateTime JoinedAt { get; set; }

        /// <summary>
        /// When the user started boosting the guild
        /// </summary>
        [JsonProperty("premium_since")]
        public DateTime? PremiumSince { get; set; }
        
        /// <summary>
        /// Whether the user is deafened in voice channels
        /// </summary>
        [JsonProperty("deaf")]
        public bool Deaf { get; set; }

        /// <summary>
        /// Whether the user is muted in voice channels
        /// </summary>
        [JsonProperty("mute")]
        public bool Mute { get; set; }
        
        
        /// <summary>
        /// Whether the user has not yet passed the guild's Membership Screening requirements
        /// </summary>
        [JsonProperty("pending")]
        public bool? Pending { get; set; }
    }
}
