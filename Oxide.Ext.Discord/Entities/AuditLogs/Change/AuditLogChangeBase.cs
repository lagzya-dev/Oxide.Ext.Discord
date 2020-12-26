using Newtonsoft.Json;

namespace Oxide.Ext.Discord.Entities.AuditLogs.Change
{
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class AuditLogChangeBase
    {
        [JsonProperty("id")]
        public Snowflake Id { get; set; }

        [JsonProperty("type")]
        public int? Type { get; set; }
    }
}
