using Newtonsoft.Json;

namespace Oxide.Ext.Discord.DiscordObjects
{
    public class MessageCreate
    {
        public string content { get; set; }
        
        public string nonce { get; set; }
        
        public bool tts { get; set; }
        
        public Embed embed { get; set; }
        
        [JsonProperty("allowed_mentions")]
        public AllowedMentions AllowedMentions { get; set; }

        [JsonProperty("message_reference")]
        public MessageReference MessageReference { get; set; }
    }
}