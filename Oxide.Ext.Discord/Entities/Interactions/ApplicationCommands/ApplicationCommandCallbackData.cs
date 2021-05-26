using System.Collections.Generic;
using Newtonsoft.Json;
using Oxide.Ext.Discord.Entities.Messages.Embeds;

namespace Oxide.Ext.Discord.Entities.Interactions.ApplicationCommands
{
    /// <summary>
    /// Represents <a href="https://discord.com/developers/docs/interactions/slash-commands#interaction-response-interactionapplicationcommandcallbackdata">Interaction Application Command Callback Data Structure</a>
    /// </summary>
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class ApplicationCommandCallbackData
    {
        /// <summary>
        /// Is the response TTS
        /// </summary>
        [JsonProperty("tts")]
        public bool? Tts { get; set; } 
        
        /// <summary>
        /// Message content
        /// </summary>
        [JsonProperty("content")]
        public string Content { get; set; } 
        
        /// <summary>
        /// List of embeds
        /// Supports up to 10 embeds
        /// </summary>
        [JsonProperty("embeds")]
        public List<DiscordEmbed> Embeds { get; set; } 
        
        /// <summary>
        /// Allowed mentions 
        /// </summary>
        [JsonProperty("allowed_mentions")]
        public bool AllowedMentions { get; set; }
    }
}