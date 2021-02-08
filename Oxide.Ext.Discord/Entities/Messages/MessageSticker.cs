using Newtonsoft.Json;
using Oxide.Ext.Discord.Helpers.Interfaces;

namespace Oxide.Ext.Discord.Entities.Messages
{
    /// <summary>
    /// Represents a <a href="https://discord.com/developers/docs/resources/channel#message-object-message-sticker-structure">Message Sticker Structure</a>
    /// </summary>
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class MessageSticker : IGetEntityId
    {
        /// <summary>
        /// ID of the sticker
        /// </summary>
        [JsonProperty("id")]
        public Snowflake Id { get; set; }
        
        /// <summary>
        /// ID of the pack the sticker is from
        /// </summary>
        [JsonProperty("pack_id")]
        public Snowflake PackId { get; set; }
        
        /// <summary>
        /// Name of the sticker
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }
        
        /// <summary>
        /// Description of the sticker
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }
        
        /// <summary>
        /// A comma-separated list of tags for the sticker
        /// </summary>
        [JsonProperty("tags")]
        public string Tags { get; set; }
        
        /// <summary>
        /// Sticker asset hash
        /// </summary>
        [JsonProperty("asset")]
        public string Asset { get; set; }
        
        /// <summary>
        /// Sticker preview asset hash
        /// </summary>
        [JsonProperty("preview_asset")]
        public string PreviewAsset { get; set; }
        
        /// <summary>
        /// Type of sticker format
        /// <see cref="MessageStickerFormatType"/>
        /// </summary>
        [JsonProperty("format_type")]
        public MessageStickerFormatType FormatType { get; set; }

        /// <summary>
        /// Returns the ID for this entity
        /// </summary>
        /// <returns>ID for this entity</returns>
        public Snowflake GetEntityId()
        {
            return Id;
        }
    }
}