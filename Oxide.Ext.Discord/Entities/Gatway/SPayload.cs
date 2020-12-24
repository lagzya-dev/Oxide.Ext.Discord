using Newtonsoft.Json;

namespace Oxide.Ext.Discord.Entities.Gatway
{
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class SPayload
    {
        [JsonProperty("op")]
        public SendOpCode OpCode;

        [JsonProperty("d")]
        public object Payload;
    }
}
