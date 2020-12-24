using System;
using Newtonsoft.Json;
using Oxide.Ext.Discord.Helpers;

namespace Oxide.Ext.Discord.Entities.Activities
{
    public class ActivityTimestamps
    {
        [JsonProperty("start")]
        public int Start { get; set; }

        [JsonIgnore]
        public DateTime StartDateTime => Start.ToDateTime();
        
        [JsonProperty("end")]
        public int End { get; set; }

        [JsonIgnore]
        public DateTime EndDateTime => Start.ToDateTime();
    }
}