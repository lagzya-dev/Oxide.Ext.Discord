using System.Collections.Generic;
using Newtonsoft.Json;

namespace Oxide.Ext.Discord.Entities.Messages
{
    /// <summary>
    /// Represents a <a href="https://discord.com/developers/docs/resources/channel#create-message-jsonform-params">Message Create Structure</a> to be created in discord
    /// </summary>
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class MessageCreate : BaseMessageCreate
    {
        /// <summary>
        /// Include to make your message a reply
        /// </summary>
        [JsonProperty("message_reference")]
        public MessageReference MessageReference { get; set; }

        /// <summary>
        /// IDs of up to 3 stickers in the server to send in the message
        /// </summary>
        [JsonProperty("sticker_ids")]
        public List<Snowflake> StickerIds { get; set; }
    }
}