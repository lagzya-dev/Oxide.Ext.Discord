namespace Oxide.Ext.Discord.Gateway
{
    using Newtonsoft.Json;

    public class SPayload
    {
        [JsonProperty("op")]
        public SendOpCode OpCode;

        [JsonProperty("d")]
        public object Payload;
    }
}
