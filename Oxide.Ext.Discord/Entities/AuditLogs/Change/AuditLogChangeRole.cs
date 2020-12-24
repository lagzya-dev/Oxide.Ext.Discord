using Newtonsoft.Json;
using Oxide.Ext.Discord.Entities.Roles;

namespace Oxide.Ext.Discord.Entities.AuditLogs.Change
{
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class AuditLogChangeRole
    {
        [JsonProperty("permissions")]
        public string Permissions { get; set; }
            
        [JsonProperty("color")]
        public DiscordColor Color { get; set; }
            
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