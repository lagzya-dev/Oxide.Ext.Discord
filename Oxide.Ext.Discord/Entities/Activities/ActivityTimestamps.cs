using System;
using Newtonsoft.Json;
using Oxide.Ext.Discord.Helpers;

namespace Oxide.Ext.Discord.Entities.Activities
{
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class ActivityTimestamps
    {
        [JsonProperty("start")]
        public int Start { get; set; }
        
        [JsonProperty("end")]
        public int End { get; set; }
        
        public DateTime StartDateTime => Start.ToDateTime();
        public DateTime EndDateTime => Start.ToDateTime();
    }
}