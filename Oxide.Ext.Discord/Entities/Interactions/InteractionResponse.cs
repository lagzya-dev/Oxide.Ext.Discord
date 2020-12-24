using Newtonsoft.Json;

namespace Oxide.Ext.Discord.Entities.Interactions
{
    public class InteractionResponse
    {
        [JsonProperty("type")]
        public InteractionResponseType Type { get; set; }
        
        [JsonProperty("data")]
        public InteractionApplicationCommandCallbackData Data { get; set; }
    }
}