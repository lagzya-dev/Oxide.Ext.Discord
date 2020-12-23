using System.Collections.Generic;

namespace Oxide.Ext.Discord.DiscordObjects
{
    public class GuildCreate
    {
        public string name { get; set; }
        
        public string region { get; set; }
        
        public string icon { get; set; }
        
        public GuildVerificationLevel? verification_level { get; set; }
        
        public DefaultMessageNotificationLevel? default_message_notifications { get; set; }
        
        public ExplicitContentFilterLevel? explicit_content_filter { get; set; }
        
        public List<Role> roles { get; set; }
        
        public List<Channel> channels { get; set; }
        
        public string afk_channel_id { get; set; }
        
        public int? afk_timeout { get; set; }
        
        public string system_channel_id { get; set; }
    }
}