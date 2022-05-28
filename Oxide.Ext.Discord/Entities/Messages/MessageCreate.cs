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
    }
}