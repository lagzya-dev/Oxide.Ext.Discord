using Newtonsoft.Json;
using Oxide.Ext.Discord.Entities.Gatway.Commands;
using Oxide.Ext.Discord.Pooling;

namespace Oxide.Ext.Discord.Entities.Gatway
{
    /// <summary>
    /// Represents a command payload
    /// </summary>
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class CommandPayload : BasePoolable
    {
        /// <summary>
        /// Command Code for the payload
        /// </summary>
        [JsonProperty("op")]
        public GatewayCommandCode OpCode;

        /// <summary>
        /// Payload data
        /// </summary>
        [JsonProperty("d")]
        public object Payload;

        /// <summary>
        /// Initializes the pooled command payload
        /// </summary>
        /// <param name="opCode">OP Code for the command</param>
        /// <param name="payload">Payload for the command</param>
        public void Init(GatewayCommandCode opCode, object payload)
        {
            OpCode = opCode;
            Payload = payload;
        }

        ///<inheritdoc/>
        protected override void EnterPool()
        {
            OpCode = default(GatewayCommandCode);
            Payload = null;
        }
    }
}
