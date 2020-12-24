using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Oxide.Ext.Discord.Entities.Gatway
{
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
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
        
        public JObject EventData => Data as JObject;
    }
}
