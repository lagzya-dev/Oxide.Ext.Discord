using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Oxide.Ext.Discord.REST;

namespace Oxide.Ext.Discord.Entities.Voice
{
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class VoiceRegion
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("vip")]
        public bool Vip { get; set; }

        [JsonProperty("optimal")]
        public bool Optimal { get; set; }

        [JsonProperty("deprecated")]
        public bool Deprecated { get; set; }

        [JsonProperty("custom")]
        public bool Custom { get; set; }

        public static void ListVoiceRegions(DiscordClient client, Action<List<VoiceRegion>> callback = null)
        {
            client.REST.DoRequest($"/voice/regions", RequestMethod.GET, null, callback);
        }
    }
}
