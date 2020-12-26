using System.Collections.Generic;
using Newtonsoft.Json;
using Oxide.Ext.Discord.Entities.Channels;
using Oxide.Ext.Discord.Entities.Users;

namespace Oxide.Ext.Discord.Entities.Guilds
{
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class GuildWidget
    {
        [JsonProperty("id")]
        public Snowflake Id { get; set; }
        
        [JsonProperty("name")]
        public string Name { get; set; }
        
        [JsonProperty("instant_invite")]
        public string InstantInvite { get; set; }
        
        [JsonProperty("channels")]
        public List<Channel> Channels { get; set; }
        
        [JsonProperty("members")]
        public List<DiscordUser> Members { get; set; }
        
        [JsonProperty("presence_count")]
        public int PresenceCount { get; set; }
    }
}