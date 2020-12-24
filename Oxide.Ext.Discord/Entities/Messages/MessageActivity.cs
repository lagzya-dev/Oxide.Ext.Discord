using Newtonsoft.Json;

namespace Oxide.Ext.Discord.Entities.Messages
{
    public class MessageActivity
    {
        [JsonProperty("type")]
        public MessageActivityType Type { get; set; }
        
        [JsonProperty("party_id")]
        public string PartyId { get; set; }
    }
}