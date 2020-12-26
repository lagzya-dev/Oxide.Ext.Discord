using System.Collections.Generic;
using Newtonsoft.Json;
using Oxide.Ext.Discord.Entities.Channels;
using Oxide.Ext.Discord.Entities.Roles;

namespace Oxide.Ext.Discord.Entities.Guilds
{
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class GuildCreate
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        
        [JsonProperty("region")]
        public string Region { get; set; }
        
        [JsonProperty("icon")]        
        public string Icon { get; set; }
                
        [JsonProperty("verification_level")]
        public GuildVerificationLevel? VerificationLevel { get; set; }
                
        [JsonProperty("default_message_notifications")]
        public DefaultMessageNotificationLevel? DefaultMessageNotifications { get; set; }
                
        [JsonProperty("explicit_content_filter")]
        public ExplicitContentFilterLevel? ExplicitContentFilter { get; set; }
                
        [JsonProperty("roles")]
        public List<Role> Roles { get; set; }
                
        [JsonProperty("channels")]
        public List<Channel> Channels { get; set; }
                
        [JsonProperty("afk_channel_id")]
        public Snowflake AfkChannelId { get; set; }
                
        [JsonProperty("afk_timeout")]
        public int? AfkTimeout { get; set; }
                
        [JsonProperty("system_channel_id")]
        public Snowflake SystemChannelId { get; set; }
    }
}