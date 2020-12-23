using System.Collections.Generic;

namespace Oxide.Ext.Discord.DiscordObjects
{
    public class ChannelCreate
    {
        public string name { get; set; }
        
        public ChannelType? type { get; set; }
        
        public string topic { get; set; }
        
        public int? bitrate { get; set; }
        
        public int? user_limit { get; set; }
        
        public int? rate_limit_per_user { get; set; }
        
        public int? position { get; set; }
        
        public List<Overwrite> permission_overwrites { get; set; }
        
        public string parent_id { get; set; }

        public bool? nsfw { get; set; }
    }
}