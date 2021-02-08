using System.Collections.Generic;
using Newtonsoft.Json;
using Oxide.Ext.Discord.Entities.Channels;

namespace Oxide.Ext.Discord.Entities.AuditLogs.Change
{
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class AuditLogChangeChannel
    {
        [JsonProperty("position")]
        public int? Position { get; set; }

        [JsonProperty("topic")]
        public string Topic { get; set; }

        [JsonProperty("bitrate")]
        public int? Bitrate { get; set; }

        [JsonProperty("permission_overwrites")]
        public List<Overwrite> PermissionOverwrites { get; set; }

        [JsonProperty("nsfw")]
        public bool Nsfw { get; set; }

        [JsonProperty("application_id")]
        public Snowflake ApplicationId { get; set; }
            
        [JsonProperty("rate_limit_per_user")]
        public int? RateLimitPerUser { get; set; }
    }
}