using Newtonsoft.Json;

namespace Oxide.Ext.Discord.Entities.AuditLogs.Change
{
    public class AuditLogChangeIntegration
    {
        [JsonProperty("enable_emoticons")]
        public bool EnableEmoticons { get; set; }
            
        [JsonProperty("expire_behavior")]
        public int ExpireBehavior { get; set; }

        [JsonProperty("expire_grace_period")]
        public int ExpireGracePeriod { get; set; }
    }
}