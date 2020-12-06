using Newtonsoft.Json;

namespace Oxide.Ext.Discord.DiscordObjects
{
    public class EmojiCreate : EmojiUpdate
    {
        [JsonProperty("image")]
        public string ImageData { get; set; }
    }
}