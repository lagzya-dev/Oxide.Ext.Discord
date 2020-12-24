using Newtonsoft.Json;
using Oxide.Ext.Discord.Entities.Messages.Embeds;

namespace Oxide.Ext.Discord.Entities.Messages
{
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class MessageCreate
    {
        [JsonProperty("content")]
        public string Content { get; set; }
        
        [JsonProperty("nonce")]
        public string Nonce { get; set; }
        
        [JsonProperty("tts")]
        public bool Tts { get; set; }
        
        [JsonProperty("embed")]
        public Embed Embed { get; set; }
        
        [JsonProperty("allowed_mentions")]
        public AllowedMentions.AllowedMentions AllowedMentions { get; set; }

        [JsonProperty("message_reference")]
        public MessageReference MessageReference { get; set; }
    }
}