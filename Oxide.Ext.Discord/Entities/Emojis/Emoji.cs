using Newtonsoft.Json;
using Oxide.Ext.Discord.Entities.Users;
using Oxide.Ext.Discord.Helpers.Cdn;
using Oxide.Ext.Discord.Helpers.Interfaces;

namespace Oxide.Ext.Discord.Entities.Emojis
{
    /// <summary>
    /// Represents <a href="https://discord.com/developers/docs/resources/emoji#emoji-object">Emoji Structure</a>
    /// </summary>
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class Emoji : EmojiUpdate, IGetEntityId
    {
        /// <summary>
        /// Emoji id
        /// </summary>
        [JsonProperty("id")]
        public Snowflake Id { get; set; }

        /// <summary>
        /// User that created this emoji
        /// </summary>
        [JsonProperty("user")]
        public DiscordUser User { get; set; }

        /// <summary>
        /// Whether this emoji must be wrapped in colons
        /// </summary>
        [JsonProperty("require_colons")]
        public bool? RequireColons { get; set; }

        /// <summary>
        /// Whether this emoji is managed
        /// </summary>
        [JsonProperty("managed")]
        public bool? Managed { get; set; }

        /// <summary>
        /// Whether this emoji is animated
        /// </summary>
        [JsonProperty("animated")]
        public bool? Animated { get; set; }
        
        /// <summary>
        /// Whether this emoji can be used, may be false due to loss of Server Boosts
        /// </summary>
        [JsonProperty("available")]
        public bool? Available { get; set; }
        
        /// <summary>
        /// Url to the emoji image
        /// </summary>
        public string Url => DiscordCdn.GetCustomEmojiUrl(Id, Animated.HasValue && Animated.Value ? ImageFormat.Gif : ImageFormat.Png);

        public Snowflake GetEntityId()
        {
            return Id;
        }
    }
}
