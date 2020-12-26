using System.Collections.Generic;
using Newtonsoft.Json;

namespace Oxide.Ext.Discord.Entities.Gatway.Events
{
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class MessageDeleteBulk
    {
        [JsonProperty("ids")]
        public List<Snowflake> Ids { get; set; }

        [JsonProperty("channel_id")]
        public Snowflake ChannelId { get; set; }
        
        [JsonProperty("guild_id")]
        public Snowflake GuildId { get; set; }
    }
}
