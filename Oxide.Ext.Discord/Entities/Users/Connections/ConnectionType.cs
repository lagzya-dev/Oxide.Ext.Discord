using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Oxide.Ext.Discord.Entities.Users.Connections
{
    /// <summary>
    /// Represents a <a href="https://discord.com/developers/docs/resources/user#connection-object-connection-structure">Connection Type</a> for a connection
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum ConnectionType
    {
        /// <summary>
        /// Connection type is Twitch
        /// </summary>
        [EnumMember(Value = "twitch")] Twitch,
        
        /// <summary>
        /// Connection type is Youtube
        /// </summary>
        [EnumMember(Value = "youtube")] Youtube,
    }
}