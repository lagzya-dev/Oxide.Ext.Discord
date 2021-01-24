using Newtonsoft.Json;

namespace Oxide.Ext.Discord.Entities.Interactions
{
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class InteractionResponse
    {
        [JsonProperty("type")]
        public InteractionResponseType Type { get; set; }
        
        [JsonProperty("data")]
        public ApplicationCommandCallbackData Data { get; set; }
    }
}