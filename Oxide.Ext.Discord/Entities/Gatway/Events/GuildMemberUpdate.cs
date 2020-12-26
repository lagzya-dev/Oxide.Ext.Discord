using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Oxide.Ext.Discord.Entities.Users;

namespace Oxide.Ext.Discord.Entities.Gatway.Events
{
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class GuildMemberUpdate
    {
        [JsonProperty("guild_id")]
        public Snowflake GuildId { get; set; }
        
        [JsonProperty("roles")]
        public List<string> Roles { get; set; }
        
        [JsonProperty("user")]
        public DiscordUser User { get; set; }
        
        [JsonProperty("nick")]
        public string Nick { get; set; }
        
        [JsonProperty("joined_at")]
        public DateTime JoinedAt { get; set; }
        
        [JsonProperty("premium_since")]
        public DateTime? PremiumSince { get; set; }
    }
}
