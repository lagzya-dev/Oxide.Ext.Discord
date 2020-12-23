using Newtonsoft.Json;

namespace Oxide.Ext.Discord.DiscordObjects
{
    public class Integration
    {
        public string id { get; set; }

        public string name { get; set; }

        public string type { get; set; }

        public bool enabled { get; set; }

        public bool? syncing { get; set; }

        public string role_id { get; set; }
        
        [JsonProperty("enable_emoticons")]
        public bool? EnableEmoticons { get; set; } 

        public int? expire_behaviour { get; set; }

        public int? expire_grace_peroid { get; set; }

        public User user { get; set; }

        public Account account { get; set; }

        public string synced_at { get; set; }
        
        [JsonProperty("subscriber_count")]
        public int? SubscriberCount { get; set; }
        
        [JsonProperty("revoked")]
        public bool? Revoked { get; set; }
        
        [JsonProperty("application")]
        public IntegrationApplication Application { get; set; }
    }
}
