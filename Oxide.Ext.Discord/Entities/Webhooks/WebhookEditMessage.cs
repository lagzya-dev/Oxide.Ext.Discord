using System.Collections.Generic;
using Newtonsoft.Json;
using Oxide.Ext.Discord.Entities.Messages.AllowedMentions;
using Oxide.Ext.Discord.Entities.Messages.Embeds;

namespace Oxide.Ext.Discord.Entities.Webhooks
{
    /// <summary>
    /// Represents <a href="https://discord.com/developers/docs/resources/webhook#edit-webhook-message-jsonform-params">Webhook Edit Message Structure</a>
    /// </summary>
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class WebhookEditMessage
    {
        /// <summary>
        /// The message contents (up to 2000 characters)
        /// </summary>
        [JsonProperty("content")]
        public string Content { get; set; }
        
        /// <summary>
        /// Embedded rich content (Up to 10 embeds)
        /// </summary>
        [JsonProperty("embeds")]
        public List<Embed> Embeds { get; set; }
        
        /// <summary>
        /// Allowed mentions for the message
        /// </summary>
        [JsonProperty("allowed_mentions")]
        public AllowedMention AllowedMention { get; set; }
    }
}