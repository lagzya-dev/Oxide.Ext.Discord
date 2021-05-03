using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Oxide.Ext.Discord.Entities.Gatway.Events;
using Oxide.Ext.Discord.WebSockets;
using Oxide.Plugins;

namespace Oxide.Ext.Discord.Entities.Gatway
{
    /// <summary>
    /// Represents <a href="https://discord.com/developers/docs/topics/gateway#payloads">Gateway Payload Structure</a>
    /// </summary>
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class EventPayload
    {
        private static readonly Hash<string, DispatchCode> Codes = new Hash<string, DispatchCode>();

        static EventPayload()
        {
            foreach (object enumValue in Enum.GetValues(typeof(DispatchCode)))
            {
                object[] descriptions = enumValue.GetType().GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (descriptions.Length == 0)
                {
                    continue;
                }

                DescriptionAttribute description = (DescriptionAttribute) descriptions[0];
                Codes[description.Description] = (DispatchCode) enumValue;
            }
        }

        /// <summary>
        /// Op Code for the payload
        /// </summary>
        [JsonProperty("op")]
        public GatewayEventCode OpCode { get; }

        /// <summary>
        /// The event name for this payload
        /// </summary>
        [JsonProperty("t")]
        public string EventName { get; }

        /// <summary>
        /// Event data
        /// </summary>
        [JsonProperty("d")]
        public object Data { get; }

        /// <summary>
        /// Sequence number, used for resuming sessions and heartbeats
        /// </summary>
        [JsonProperty("s")]
        public int? Sequence { get; }

        /// <summary>
        /// Returns a DispatchCode enum value for the EventName if we have it; Else the code will be Unknown
        /// </summary>
        public DispatchCode EventCode => Codes.TryGetValue(EventName, out DispatchCode code) ? code : DispatchCode.Unknown;

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