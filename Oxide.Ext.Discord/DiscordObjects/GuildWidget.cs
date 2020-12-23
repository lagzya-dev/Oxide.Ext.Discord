using System.Collections.Generic;
using Newtonsoft.Json;

namespace Oxide.Ext.Discord.DiscordObjects
{
    public class GuildWidget
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        
        [JsonProperty("name")]
        public string Name { get; set; }
        
        [JsonProperty("instant_invite")]
        public string InstantInvite { get; set; }
        
        [JsonProperty("channels")]
        public List<Channel> Channels { get; set; }
        
        [JsonProperty("members")]
        public List<User> Members { get; set; }
        
        [JsonProperty("presence_count")]
        public int PresenceCount { get; set; }
    }
}