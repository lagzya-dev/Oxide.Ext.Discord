using System.Collections.Generic;
using Newtonsoft.Json;

namespace Oxide.Ext.Discord.Entities.Gatway.Events
{
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class MessageDeleteBulk
    {
        [JsonProperty("ids")]
        public List<string> Ids { get; set; }

        [JsonProperty("channel_id")]
        public string ChannelId { get; set; }
        
        [JsonProperty("guild_id")]
        public string GuildId { get; set; }
    }
}
