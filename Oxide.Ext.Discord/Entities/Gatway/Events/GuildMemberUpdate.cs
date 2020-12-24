using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Oxide.Ext.Discord.Entities.Users;

namespace Oxide.Ext.Discord.Entities.Gatway.Events
{
    public class GuildMemberUpdate
    {
        [JsonProperty("guild_id")]
        public string GuildId { get; set; }
        
        [JsonProperty("roles")]
        public List<string> Roles { get; set; }
        
        [JsonProperty("user")]
        public User User { get; set; }
        
        [JsonProperty("nick")]
        public string Nick { get; set; }
        
        [JsonProperty("joined_at")]
        public DateTime JoinedAt { get; set; }
        
        [JsonProperty("premium_since")]
        public DateTime? PremiumSince { get; set; }
    }
}
