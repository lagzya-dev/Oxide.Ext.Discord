using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Oxide.Ext.Discord.Entities.Gatway.Events;
using Oxide.Ext.Discord.Entities.Users;
using Oxide.Ext.Discord.Helpers.Interfaces;

namespace Oxide.Ext.Discord.Entities.Guilds
{
    /// <summary>
    /// Represents <a href="https://discord.com/developers/docs/resources/guild#guild-member-object-guild-member-structure">Guild Member Structure</a>
    /// </summary>
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class GuildMember : IGetEntityId
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
        /// Total permissions of the member in the channel, including overrides, returned when in the interaction object
        /// </summary>
        [JsonProperty("permissions")]
        public string Permissions { get; set; }
        
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

        internal void Update(GuildMemberUpdateEvent update)
        {
            if (update.User != null)
            {
                User.Update(update.User);
            }
                    
            if (update.Nick != null)
            {
                Nick = update.Nick;
            }
                    
            if (update.Roles != null)
            {
                Roles = update.Roles;
            }
                    
            if (update.PremiumSince != null)
            {
                PremiumSince = update.PremiumSince;
            }

            if (update.Pending != null)
            {
                Pending = update.Pending;
            }
        }
        
        /// <summary>
        /// Returns the ID for this entity
        /// </summary>
        /// <returns>ID for this entity</returns>
        public Snowflake GetEntityId()
        {
            return User.Id;
        }
    }
}
