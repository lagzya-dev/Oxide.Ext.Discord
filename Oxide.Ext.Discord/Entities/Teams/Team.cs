using System.Collections.Generic;
using Newtonsoft.Json;
using Oxide.Ext.Discord.Helpers.Cdn;

namespace Oxide.Ext.Discord.Entities.Teams
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

        public string GetTeamIconUrl => DiscordCdn.GetTeamIconUrl(Id, Icon);
    }
}