using Newtonsoft.Json;
using Oxide.Ext.Discord.Entities.Gatway.Events;
using Oxide.Ext.Discord.Json.Converters;
using Oxide.Ext.Discord.Pooling;
using Oxide.Ext.Discord.WebSockets.Handlers;

namespace Oxide.Ext.Discord.Entities.Gatway
{
    /// <summary>
    /// Represents <a href="https://discord.com/developers/docs/topics/gateway#payloads">Gateway Payload Structure</a>
    /// </summary>
    [JsonConverter(typeof(EventPayloadConverter))]
    public class EventPayload : BasePoolable
    {
        /// <summary>
        /// Op Code for the payload
        /// </summary>
        public GatewayEventCode OpCode { get; internal set; }

        /// <summary>
        /// The event name for this payload
        /// </summary>
        public DiscordDispatchCode DispatchCode { get; internal set; }

        /// <summary>
        /// Event data
        /// </summary>
        public object Data { get; internal set; }

        /// <summary>
        /// Sequence number, used for resuming sessions and heartbeats
        /// </summary>
        public int? Sequence { get; internal set; }

        /// <summary>
        /// Returns the Data as {T}
        /// </summary>
        /// <typeparam name="T">Type to convert Data to</typeparam>
        /// <returns>Data converted to {T}</returns>
        public T GetData<T>()
        {
            return (T)Data;
        }
        
        /// <inheritdoc/>
        protected override void EnterPool()
        {
            OpCode = default(GatewayEventCode);
            DispatchCode = default(DiscordDispatchCode);
            Data = null;
            Sequence = null;
        }
        
        ///<inheritdoc/>
        protected override void DisposeInternal()
        {
            DiscordPool.Free(this);
        }
    }
}