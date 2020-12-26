using Newtonsoft.Json;

namespace Oxide.Ext.Discord.Entities.Channels
{
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]

    public class Overwrite
    {
        [JsonProperty("id")]
        public Snowflake Id { get; set; }
        
        [JsonProperty("allow")]
        public PermissionFlags? Allow { get; set; }
        
        [JsonProperty("deny")]
        public PermissionFlags? Deny { get; set; }
        
        [JsonProperty("type")]
        public PermissionType Type { get; set; }
    }
}
