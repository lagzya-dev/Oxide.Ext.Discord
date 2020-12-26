using System.Collections.Generic;
using Newtonsoft.Json;
using Oxide.Ext.Discord.Entities.Emojis;

namespace Oxide.Ext.Discord.Entities.Gatway.Events
{
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]

    public class GuildEmojisUpdate
    {
        [JsonProperty("guild_id")]
        public Snowflake GuildId { get; set; }

        [JsonProperty("emojis")]
        public List<Emoji> Emojis { get; set; }
    }
}
