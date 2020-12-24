using System.Collections.Generic;
using Newtonsoft.Json;
using Oxide.Ext.Discord.Entities.Guilds.Integrations;

namespace Oxide.Ext.Discord.Entities.Users.Connections
{
    public class Connection
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("revoked")]
        public bool? Revoked { get; set; }

        [JsonProperty("integrations")]
        public List<Integration> Integrations { get; set; }
        
        [JsonProperty("verified")]
        public bool Verified { get; set; }      
        
        [JsonProperty("friend_sync")]
        public bool FriendSync { get; set; }        
        
        [JsonProperty("show_activity")]
        public bool ShowActivity { get; set; }        
        
        [JsonProperty("visibility")]
        public ConnectionVisibilityType Visibility { get; set; }
    }
}
