using System.Collections.Generic;
using Newtonsoft.Json;

namespace Oxide.Ext.Discord.DiscordObjects.Team
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
        public User User { get; set; } 
    }
}