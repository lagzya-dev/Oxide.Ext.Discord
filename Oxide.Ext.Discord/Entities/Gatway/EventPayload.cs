using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Oxide.Ext.Discord.Entities.Gatway.Events;

namespace Oxide.Ext.Discord.Entities.Gatway
{
    /// <summary>
    /// Represents <a href="https://discord.com/developers/docs/topics/gateway#payloads">Gateway Payload Structure</a>
    /// </summary>
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class EventPayload
    {
        /// <summary>
        /// Op Code for the payload
        /// </summary>
        [JsonProperty("op")]
        public GatewayEventCode OpCode { get; set; }
        
        /// <summary>
        /// The event name for this payload
        /// </summary>
        [JsonProperty("t")]
        public string EventName { get; set; }

        /// <summary>
        /// Event data
        /// </summary>
        [JsonProperty("d")]
        public object Data { get; set; }

        /// <summary>
        /// Sequence number, used for resuming sessions and heartbeats
        /// </summary>
        [JsonProperty("s")]
        public int? Sequence { get; set; }
        
        /// <summary>
        /// Data as JObject
        /// </summary>
        public JObject EventData => Data as JObject;
        
        /// <summary>
        /// Data as JToken
        /// </summary>
        public JToken TokenData => Data as JToken;
    }
}
