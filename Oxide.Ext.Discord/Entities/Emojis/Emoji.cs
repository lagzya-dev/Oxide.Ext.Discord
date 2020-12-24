using Newtonsoft.Json;
using Oxide.Ext.Discord.Entities.Users;

namespace Oxide.Ext.Discord.Entities.Emojis
{
    public class Emoji : EmojiUpdate
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("user")]
        public DiscordUser User { get; set; }

        [JsonProperty("require_colons")]
        public bool? RequireColons { get; set; }

        [JsonProperty("managed")]
        public bool? Managed { get; set; }

        [JsonProperty("animated")]
        public bool? Animated { get; set; }
        
        [JsonProperty("available")]
        public bool? Available { get; set; }
    }
}
