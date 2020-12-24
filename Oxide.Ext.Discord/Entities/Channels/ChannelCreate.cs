using System.Collections.Generic;
using Newtonsoft.Json;

namespace Oxide.Ext.Discord.Entities.Channels
{
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class ChannelCreate
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        
        [JsonProperty("type")]
        public ChannelType? Type { get; set; }
        
        [JsonProperty("topic")]        
        public string Topic { get; set; }
                
        [JsonProperty("bitrate")]
        public int? Bitrate { get; set; }
                
        [JsonProperty("user_limit")]
        public int? UserLimit { get; set; }
                
        [JsonProperty("rate_limit_per_user")]
        public int? RateLimitPerUser { get; set; }
                
        [JsonProperty("position")]
        public int? Position { get; set; }
                
        [JsonProperty("permission_overwrites")]
        public List<Overwrite> PermissionOverwrites { get; set; }
                
        [JsonProperty("parent_id")]
        public string ParentId { get; set; }
        
        [JsonProperty("nsfw")]
        public bool? Nsfw { get; set; }
    }
}