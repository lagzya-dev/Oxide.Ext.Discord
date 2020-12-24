using System.Collections.Generic;
using Newtonsoft.Json;
using Oxide.Ext.Discord.Entities.Users;

namespace Oxide.Ext.Discord.Entities.Team
{
    public class TeamMember
    {
        [JsonProperty("membership_state")]
        public TeamMembershipState MembershipState { get; set; }
        
        [JsonProperty("permissions")]
        public List<string> Permissions { get; set; }
        
        [JsonProperty("team_id")]
        public string TeamId { get; set; }
        
        [JsonProperty("user")]
        public DiscordUser User { get; set; } 
    }
}