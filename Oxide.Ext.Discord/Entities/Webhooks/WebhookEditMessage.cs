using System.Collections.Generic;
using Newtonsoft.Json;
using Oxide.Ext.Discord.Entities.Messages.AllowedMentions;
using Oxide.Ext.Discord.Entities.Messages.Embeds;

namespace Oxide.Ext.Discord.Entities.Webhooks
{
    public class WebhookEditMessage
    {
        [JsonProperty("content")]
        public string Content { get; set; }
        
        [JsonProperty("embeds")]
        public List<Embed> Embeds { get; set; }
        
        [JsonProperty("allowed_mentions")]
        public AllowedMentions AllowedMentions { get; set; }
    }
}