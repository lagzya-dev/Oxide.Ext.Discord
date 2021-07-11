using Newtonsoft.Json;
using Oxide.Ext.Discord.Entities.Users;
using Oxide.Ext.Discord.Interfaces;

namespace Oxide.Ext.Discord.Entities.Messages
{
    /// <summary>
    /// Represents a <a href="https://discord.com/developers/docs/resources/channel#message-sticker-structure">Message Sticker Structure</a>
    /// </summary>
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class MessageSticker : ISnowflakeEntity
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
        public Snowflake? PackId { get; set; }
        
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
        /// For guild stickers, a unicode emoji representing the sticker's expression.
        /// For nitro stickers, a comma-separated list of related expressions.
        /// </summary>
        [JsonProperty("tags")]
        public string Tags { get; set; }

        /// <summary>
        /// Type of sticker format
        /// <see cref="MessageStickerFormatType"/>
        /// </summary>
        [JsonProperty("format_type")]
        public MessageStickerFormatType FormatType { get; set; }
        
        /// <summary>
        /// Whether or not the sticker is available
        /// </summary>
        [JsonProperty("available")]
        public bool? Available { get; set; }
        
        /// <summary>
        /// Id of the guild that owns this sticker
        /// </summary>
        [JsonProperty("guild_id")]
        public Snowflake? GuildId { get; set; }
        
        /// <summary>
        /// The user that uploaded the sticker
        /// </summary>
        [JsonProperty("user")]
        public DiscordUser User { get; set; }
        
        /// <summary>
        /// A sticker's sort order within a pack
        /// </summary>
        [JsonProperty("sort_value")]
        public int? SortValue { get; set; }
    }
}