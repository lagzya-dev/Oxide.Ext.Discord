using System;
using Newtonsoft.Json;

namespace Oxide.Ext.Discord.Entities.Gatway
{
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    class Gateway
    {
        [JsonProperty("url")]
        public string Url { get; private set; }
        
        public static string WebsocketUrl { get; internal set; }

        public static void GetGateway(DiscordClient client, Action<Gateway> callback)
        {
            client.REST.DoRequest("/gateway", REST.RequestMethod.GET, null, callback);
        }
    }
}
