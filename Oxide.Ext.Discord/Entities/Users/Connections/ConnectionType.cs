using System.ComponentModel;
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
        [Description("twitch")] Twitch,
        
        /// <summary>
        /// Connection type is Youtube
        /// </summary>
        [Description("youtube")] Youtube,
    }
}