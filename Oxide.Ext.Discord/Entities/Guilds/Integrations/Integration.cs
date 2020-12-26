using Newtonsoft.Json;
using Oxide.Ext.Discord.Entities.Users;

namespace Oxide.Ext.Discord.Entities.Guilds.Integrations
{
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class Integration
    {
        [JsonProperty("id")]
        public Snowflake Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("type")]
        public IntegrationType Type { get; set; }

        [JsonProperty("enabled")]
        public bool Enabled { get; set; }

        [JsonProperty("syncing")]
        public bool? Syncing { get; set; }

        [JsonProperty("role_id")]
        public string RoleId { get; set; }
        
        [JsonProperty("enable_emoticons")]
        public bool? EnableEmoticons { get; set; } 

        [JsonProperty("expire_behaviour")]
        public int? ExpireBehaviour { get; set; }

        [JsonProperty("expire_grace_period")]
        public int? ExpireGracePeriod { get; set; }

        [JsonProperty("user")]
        public DiscordUser User { get; set; }

        [JsonProperty("account")]
        public Account Account { get; set; }

        [JsonProperty("synced_at")]
        public string SyncedAt { get; set; }
        
        [JsonProperty("subscriber_count")]
        public int? SubscriberCount { get; set; }
        
        [JsonProperty("revoked")]
        public bool? Revoked { get; set; }
        
        [JsonProperty("application")]
        public IntegrationApplication Application { get; set; }
    }
}
