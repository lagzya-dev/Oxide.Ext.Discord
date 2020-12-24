using Newtonsoft.Json;
using Oxide.Ext.Discord.Entities.AuditLogs.Change;

namespace Oxide.Ext.Discord.Entities.AuditLogs
{
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class AuditLogChange
    {
        [JsonProperty("new_value")]
        public AuditLogChangeBase NewValue { get; set; }

        [JsonProperty("old_value")]
        public AuditLogChangeBase OldValue { get; set; }

        [JsonProperty("key")]
        public string Key { get; set; }
    }
}
