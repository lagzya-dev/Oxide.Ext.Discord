using System;
using Newtonsoft.Json;

namespace Oxide.Ext.Discord.Entities.Invites
{
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class InviteMetadata : Invite
    {
        [JsonProperty("uses")]
        public int Uses { get; set; }
        
        [JsonProperty("max_uses")]
        public int MaxUses { get; set; }
        
        [JsonProperty("max_age")]
        public int MaxAge { get; set; }
        
        [JsonProperty("temporary")]
        public bool Temporary { get; set; }
        
        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }
    }
}