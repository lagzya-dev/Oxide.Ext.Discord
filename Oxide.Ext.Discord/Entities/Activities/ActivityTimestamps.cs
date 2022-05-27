using System;
using Newtonsoft.Json;
using Oxide.Ext.Discord.Helpers;

namespace Oxide.Ext.Discord.Entities.Activities
{
    /// <summary>
    /// Represents <a href="https://discord.com/developers/docs/topics/gateway#activity-object-activity-timestamps">Activity Timestamps</a>
    /// </summary>
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class ActivityTimestamps
    {
        /// <summary>
        /// Unix time (in milliseconds) of when the activity started
        /// </summary>
        [JsonProperty("start")]
        public long Start { get; set; }
        
        /// <summary>
        /// Unix time (in milliseconds) of when the activity ends
        /// </summary>
        [JsonProperty("end")]
        public long End { get; set; }
        
        /// <summary>
        /// DateTimeOffset when the activity starts
        /// </summary>
        public DateTimeOffset StartDateTime => Start.ToDateTimeOffsetFromMilliseconds();

        /// <summary>
        /// DateTime when the activity ends
        /// </summary>
        public DateTimeOffset EndDateTime => End.ToDateTimeOffsetFromMilliseconds();
    }
}