using System.ComponentModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Oxide.Ext.Discord.Entities.Users.Connections
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum ConnectionType
    {
        [Description("twitch")] Twitch,
        [Description("youtube")] Youtube,
    }
}