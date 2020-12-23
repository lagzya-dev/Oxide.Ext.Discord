using Newtonsoft.Json;

namespace Oxide.Ext.Discord.DiscordEvents
{
    using System.Collections.Generic;
    using Oxide.Ext.Discord.DiscordObjects;

    public class PresenceUpdate
    {
        public User user { get; set; }

        public string guild_id { get; set; }
        
        public PresenceStatus status { get; set; }
        
        [JsonProperty("activities")]
        public List<Activity> Activities { get; set; }
        
        [JsonProperty("client_status")]
        public ClientStatus ClientStatus { get; set; }
    }
}
