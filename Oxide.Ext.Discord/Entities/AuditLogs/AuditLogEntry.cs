using System.Collections.Generic;
using Newtonsoft.Json;

namespace Oxide.Ext.Discord.Entities.AuditLogs
{
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class AuditLogEntry
    {
        [JsonProperty("target_id")]
        public string TargetId { get; set; }

        [JsonProperty("changes")]
        public List<AuditLogChange> Changes { get; set; }

        [JsonProperty("user_id")]
        public string UserId { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("action_type")]
        public int? ActionType { get; set; }

        [JsonProperty("options")]
        public OptionalAuditEntryInfo Options { get; set; }

        [JsonProperty("reason")]
        public string Reason { get; set; }
    }
}
