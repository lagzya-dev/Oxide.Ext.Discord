using System.Collections.Generic;
using Newtonsoft.Json;
using Oxide.Ext.Discord.Entities.Channels;
using Oxide.Ext.Discord.Entities.Users;

namespace Oxide.Ext.Discord.Entities.Guilds
{
    /// <summary>
    /// Represents <a href=""></a>
    /// </summary>
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class GuildWidget
    {
        /// <summary>
        /// ID of the guild
        /// </summary>
        [JsonProperty("id")]
        public Snowflake Id { get; set; }
        
        /// <summary>
        /// Name of the guild
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }
        
        /// <summary>
        /// Instant invite line for the guild
        /// </summary>
        [JsonProperty("instant_invite")]
        public string InstantInvite { get; set; }
        
        /// <summary>
        /// List of guild channels
        /// </summary>
        [JsonProperty("channels")]
        public List<Channel> Channels { get; set; }
        
        /// <summary>
        /// List of guild members
        /// </summary>
        [JsonProperty("members")]
        public List<DiscordUser> Members { get; set; }
        
        /// <summary>
        /// The count of the presences
        /// </summary>
        [JsonProperty("presence_count")]
        public int PresenceCount { get; set; }
    }
}