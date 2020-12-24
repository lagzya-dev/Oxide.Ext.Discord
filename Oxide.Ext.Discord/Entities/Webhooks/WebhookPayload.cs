using System.Collections.Generic;
using Newtonsoft.Json;
using Oxide.Ext.Discord.Entities.Messages.AllowedMentions;
using Oxide.Ext.Discord.Entities.Messages.Embeds;

namespace Oxide.Ext.Discord.Entities.Webhooks
{
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class WebhookPayload
    {
        [JsonProperty("content")]
        public string Content { get; set; }

        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("avatar_url")]
        public string AvatarUrl { get; set; }

        [JsonProperty("tts")]
        public bool Tts { get; set; }

        [JsonProperty("file")]
        public string File { get; set; }

        [JsonProperty("embeds")]
        public List<Embed> Embeds { get; set; }
        
        [JsonProperty("allowed_mentions")]
        public AllowedMentions AllowedMentions { get; set; }
    }
}
