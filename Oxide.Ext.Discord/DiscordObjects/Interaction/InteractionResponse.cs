using Newtonsoft.Json;

namespace Oxide.Ext.Discord.DiscordObjects.Interaction
{
    public class InteractionResponse
    {
        [JsonProperty("type")]
        public InteractionResponseType Type { get; set; }
        
        [JsonProperty("data")]
        public InteractionApplicationCommandCallbackData Data { get; set; }
    }
}