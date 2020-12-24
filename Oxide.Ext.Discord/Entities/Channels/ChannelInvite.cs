using Newtonsoft.Json;

namespace Oxide.Ext.Discord.Entities.Channels
{
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class ChannelInvite
    {
        [JsonProperty("max_age")]
        public int MaxAge { get; set; } = 86400;
        
        [JsonProperty("max_uses")]
        public int MaxUses { get; set; }
        
        [JsonProperty("temporary")]
        public bool Temporary { get; set; }
        
        [JsonProperty("unique")]
        public bool Unique { get; set; }
        
        [JsonProperty("target_user")]
        public string TargetUser { get; set; }
        
        [JsonProperty("target_user_type")]
        public int? TargetUserType { get; set; }
    }
}