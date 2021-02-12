using Newtonsoft.Json;
using Oxide.Ext.Discord.Entities.Messages.AllowedMentions;
using Oxide.Ext.Discord.Entities.Messages.Embeds;

namespace Oxide.Ext.Discord.Entities.Messages
{
    /// <summary>
    /// Represents a <a href="https://discord.com/developers/docs/resources/channel#create-message-parameters-for-contenttype-applicationjson">Message Create Structure</a> to be created in discord
    /// </summary>
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class MessageCreate
    {
        /// <summary>
        /// Contents of the message
        /// </summary>
        [JsonProperty("content")]
        public string Content { get; set; }
        
        /// <summary>
        /// Used for validating a message was sent
        /// </summary>
        [JsonProperty("nonce")]
        public string Nonce { get; set; }
        
        /// <summary>
        /// Whether this was a TTS message
        /// </summary>
        [JsonProperty("tts")]
        public bool Tts { get; set; }
        
        /// <summary>
        /// Embed for the message
        /// </summary>
        [JsonProperty("embed")]
        public Embed Embed { get; set; }
        
        /// <summary>
        /// Allowed mentions for a message
        /// Allows for more granular control over mentions without various hacks to the message content. 
        /// </summary>
        [JsonProperty("allowed_mentions")]
        public AllowedMention AllowedMention { get; set; }

        /// <summary>
        /// Include to make your message a reply
        /// </summary>
        [JsonProperty("message_reference")]
        public MessageReference MessageReference { get; set; }
    }
}