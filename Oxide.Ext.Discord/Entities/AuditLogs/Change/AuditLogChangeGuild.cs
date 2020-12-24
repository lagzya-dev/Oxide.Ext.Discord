using System.Collections.Generic;
using Newtonsoft.Json;
using Oxide.Ext.Discord.Entities.Guilds.Roles;

namespace Oxide.Ext.Discord.Entities.AuditLogs.Change
{
    public class AuditLogChangeGuild
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("icon_hash")]
        public string IconHash { get; set; }

        [JsonProperty("splash_hash")]
        public string SplashHash { get; set; }

        [JsonProperty("owner_id")]
        public string OwnerId { get; set; }

        [JsonProperty("region")]
        public string Region { get; set; }

        [JsonProperty("afk_channel_id")]
        public string AfkChannelId { get; set; }

        [JsonProperty("afk_timeout")]
        public int? AfkTimeout { get; set; }

        [JsonProperty("mfa_level")]
        public int? MfaLevel { get; set; }

        [JsonProperty("verification_level")]
        public int? VerificationLevel { get; set; }

        [JsonProperty("explicit_content_filter")]
        public int? ExplicitContentFilter { get; set; }

        [JsonProperty("default_message_notifications")]
        public int? DefaultMessageNotifications { get; set; }

        [JsonProperty("vanity_url_code")]
        public string VanityUrlCode { get; set; }

        [JsonProperty("$add")]
        public List<Role> Add { get; set; }

        [JsonProperty("$remove")]
        public List<Role> Remove { get; set; }

        [JsonProperty("prune_delete_days")]
        public int? PruneDeleteDays { get; set; }

        [JsonProperty("widget_enabled")]
        public bool WidgetEnabled { get; set; }

        [JsonProperty("widget_channel_id")]
        public string WidgetChannelId { get; set; }
            
        [JsonProperty("system_channel_id")]
        public string SystemChannelId { get; set; }
    }
}