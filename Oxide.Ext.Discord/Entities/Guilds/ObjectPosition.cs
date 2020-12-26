using Newtonsoft.Json;

namespace Oxide.Ext.Discord.Entities.Guilds
{
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class ObjectPosition
    {
        [JsonProperty("id")]
        public Snowflake Id { get; set; }

        [JsonProperty("position")]
        public int Position { get; set; }
    }
}
