using System.Collections.Generic;
using Newtonsoft.Json;

namespace Oxide.Ext.Discord.DiscordObjects.Interaction
{
    public class InteractionApplicationCommandCallbackData
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