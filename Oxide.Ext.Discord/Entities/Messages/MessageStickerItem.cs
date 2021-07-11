using Newtonsoft.Json;

namespace Oxide.Ext.Discord.Entities.Messages
{
    /// <summary>
    /// Represents a <a href="https://discord.com/developers/docs/resources/channel#message-sticker-item-structure">Message Sticker Structure</a>
    /// </summary>
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class MessageStickerItem
    {
        /// <summary>
        /// ID of the sticker
        /// </summary>
        [JsonProperty("id")]
        public Snowflake Id { get; set; }
        
        /// <summary>
        /// Name of the sticker
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}