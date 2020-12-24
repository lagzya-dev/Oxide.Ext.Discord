using Newtonsoft.Json;

namespace Oxide.Ext.Discord.Entities.AuditLogs.Change
{
    public class AuditLogChangeRole
    {
        [JsonProperty("permissions")]
        public string Permissions { get; set; }
            
        [JsonProperty("color")]
        public int Color { get; set; }
            
        [JsonProperty("hoist")]
        public bool Hoist { get; set; }
            
        [JsonProperty("mentionable")]
        public bool Mentionable { get; set; }
        
        [JsonProperty("allow")]
        public int? Allow { get; set; }
        
        [JsonProperty("deny")]
        public int? Deny { get; set; }
    }
}