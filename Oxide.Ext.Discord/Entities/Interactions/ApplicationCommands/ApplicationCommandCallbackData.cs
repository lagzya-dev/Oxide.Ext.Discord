using System.Collections.Generic;
using Newtonsoft.Json;
using Oxide.Ext.Discord.Entities.Messages.Embeds;

namespace Oxide.Ext.Discord.Entities.Interactions
{
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class ApplicationCommandCallbackData
    {
        [JsonProperty("tts")]
        public bool? Tts { get; set; } 
        
        [JsonProperty("content")]
        public string Content { get; set; } 
        
        [JsonProperty("embeds")]
        public List<Embed> Embeds { get; set; } 
        
        [JsonProperty("allowed_mentions")]
        public bool AllowedMentions { get; set; }
    }
}