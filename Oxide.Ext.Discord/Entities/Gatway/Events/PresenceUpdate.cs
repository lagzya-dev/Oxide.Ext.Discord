using System.Collections.Generic;
using Newtonsoft.Json;
using Oxide.Ext.Discord.Entities.Activities;
using Oxide.Ext.Discord.Entities.Gatway.Commands;
using Oxide.Ext.Discord.Entities.Users;

namespace Oxide.Ext.Discord.Entities.Gatway.Events
{
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class PresenceUpdate
    {
        [JsonProperty("user")]
        public DiscordUser User { get; set; }

        [JsonProperty("guild_id")]
        public string GuildId { get; set; }
        
        [JsonProperty("status")]
        public PresenceStatus Status { get; set; }
        
        [JsonProperty("activities")]
        public List<Activity> Activities { get; set; }
        
        [JsonProperty("client_status")]
        public ClientStatus ClientStatus { get; set; }
    }
}
