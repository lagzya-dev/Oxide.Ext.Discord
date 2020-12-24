using System.Collections.Generic;
using Newtonsoft.Json;

namespace Oxide.Ext.Discord.Entities.Activities
{
    public class ActivityParty
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        
        [JsonProperty("size")]
        public List<int> Size { get; set; }
    }
}