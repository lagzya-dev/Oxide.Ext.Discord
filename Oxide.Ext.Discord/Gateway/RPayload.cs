namespace Oxide.Ext.Discord.Gateway
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    public class RPayload
    {
        [JsonProperty("op")]
        public ReceiveOpCode OpCode { get; set; }
        
        [JsonProperty("t")]
        public string EventName { get; set; }

        [JsonProperty("d")]
        public object Data { get; set; }

        [JsonProperty("s")]
        public int? Sequence { get; set; }

        [JsonIgnore]
        public JObject EventData => Data as JObject;
    }
}
