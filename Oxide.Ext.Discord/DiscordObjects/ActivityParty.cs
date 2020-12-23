using System.Collections.Generic;
using Newtonsoft.Json;

namespace Oxide.Ext.Discord.DiscordObjects
{
    public class ActivityParty
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        
        [JsonProperty("size")]
        public List<int> size { get; set; }
    }
}