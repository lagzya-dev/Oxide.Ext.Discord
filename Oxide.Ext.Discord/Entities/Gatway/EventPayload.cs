using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Oxide.Ext.Discord.Entities.Gatway.Events;
using Oxide.Ext.Discord.Pooling;
using Oxide.Ext.Discord.WebSockets;
namespace Oxide.Ext.Discord.Entities.Gatway
{
    /// <summary>
    /// Represents <a href="https://discord.com/developers/docs/topics/gateway#payloads">Gateway Payload Structure</a>
    /// </summary>
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class EventPayload : BasePoolable
    {
        /// <summary>
        /// Op Code for the payload
        /// </summary>
        [JsonProperty("op")]
        public GatewayEventCode OpCode { get; private set; }

        /// <summary>
        /// The event name for this payload
        /// </summary>
        [JsonProperty("t")]
        public JToken EventName { get; private set; }

        /// <summary>
        /// Event data
        /// </summary>
        [JsonProperty("d")]
        public object Data { get; private set;}

        /// <summary>
        /// Sequence number, used for resuming sessions and heartbeats
        /// </summary>
        [JsonProperty("s")]
        public int? Sequence { get; private set; }

        /// <summary>
        /// Returns a DispatchCode enum value for the EventName if the extension supports it; Else the code will be Unknown
        /// </summary>
        public DiscordDispatchCode EventCode => EventName?.ToObject<DiscordDispatchCode>() ?? DiscordDispatchCode.Unknown;

        /// <summary>
        /// Data as JObject
        /// </summary>
        public JObject EventData => Data as JObject;

        /// <summary>
        /// Data as JToken
        /// </summary>
        public JToken TokenData => Data as JToken;

        ///<inheritdoc/>
        protected override void DisposeInternal()
        {
            DiscordPool.Free(this);
        }
        
        /// <inheritdoc/>
        protected override void EnterPool()
        {
            OpCode = default(GatewayEventCode);
            EventName = null;
            Data = null;
            Sequence = null;
        }
    }
}