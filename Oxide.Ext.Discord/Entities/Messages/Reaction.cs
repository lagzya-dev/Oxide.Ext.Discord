using Newtonsoft.Json;
using Oxide.Ext.Discord.Entities.Emojis;

namespace Oxide.Ext.Discord.Entities.Messages
{
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class Reaction
    {
        [JsonProperty("count")]
        public int Count { get; set; }

        [JsonProperty("me")]
        public bool Me { get; set; }

        [JsonProperty("emoji")]
        public Emoji Emoji { get; set; }
    }
}
