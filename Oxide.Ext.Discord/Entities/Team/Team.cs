using System.Collections.Generic;
using Newtonsoft.Json;

namespace Oxide.Ext.Discord.Entities.Team
{
    public class Team
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        
        [JsonProperty("icon")]
        public string Icon { get; set; }
        
        [JsonProperty("members")]
        public List<TeamMember> Members { get; set; }
        
        [JsonProperty("owner_user_id")]
        public string OwnerUserId { get; set; } 
    }
}