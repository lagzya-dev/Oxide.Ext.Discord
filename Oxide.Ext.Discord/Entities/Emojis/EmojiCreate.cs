using Newtonsoft.Json;

namespace Oxide.Ext.Discord.Entities.Emojis
{
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]

    public class EmojiCreate : EmojiUpdate
    {
        [JsonProperty("image")]
        public string ImageData { get; set; }
    }
}