using System.Runtime.Serialization;
using Newtonsoft.Json;
using Oxide.Ext.Discord.Helpers.Converters;

namespace Oxide.Ext.Discord.Entities.Users.Connections
{
    /// <summary>
    /// Represents a <a href="https://discord.com/developers/docs/resources/user#connection-object-connection-structure">Connection Type</a> for a connection
    /// </summary>
    [JsonConverter(typeof(DiscordEnumConverter<ConnectionType>), Unknown)]
    public enum ConnectionType
    {
        /// <summary>
        /// Discord Extension doesn't currently support this connection type
        /// </summary>
        Unknown,
        
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