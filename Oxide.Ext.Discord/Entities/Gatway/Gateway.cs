using System;
using Newtonsoft.Json;

namespace Oxide.Ext.Discord.Entities.Gatway
{
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    class Gateway
    {
        [JsonProperty("url")]
        public string URL { get; private set; }

        public static void GetGateway(DiscordClient client, Action<Gateway> callback)
        {
            client.REST.DoRequest("/gateway", REST.RequestMethod.GET, null, callback);
        }
    }
}
