using Newtonsoft.Json;

namespace Oxide.Ext.Discord.Entities.Gatway
{
    public class SPayload
    {
        [JsonProperty("op")]
        public OpCodes OP;

        [JsonProperty("d")]
        public object Payload;
    }
}
