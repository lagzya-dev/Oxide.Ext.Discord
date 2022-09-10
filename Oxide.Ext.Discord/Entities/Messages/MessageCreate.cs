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
        /// Can be used to verify a message was sent (up to 25 characters).
        /// Value will appear in the Message Create event.
        /// </summary>
        [JsonProperty("content")]
        public string Nonce { get; set; }
        
        /// <summary>
        /// Include to make your message a reply
        /// </summary>
        [JsonProperty("message_reference")]
        public MessageReference MessageReference { get; set; }
    }
}