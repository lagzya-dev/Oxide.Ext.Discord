using Newtonsoft.Json;

namespace Oxide.Ext.Discord.Entities.Emojis
{
    public class EmojiCreate : EmojiUpdate
    {
        [JsonProperty("image")]
        public string ImageData { get; set; }
    }
}