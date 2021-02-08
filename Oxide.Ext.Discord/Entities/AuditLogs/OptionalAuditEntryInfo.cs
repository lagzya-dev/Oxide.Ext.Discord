using Newtonsoft.Json;

namespace Oxide.Ext.Discord.Entities.AuditLogs
{
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class OptionalAuditEntryInfo
    {
        [JsonProperty("delete_member_days")]
        public string DeleteMemberDays { get; set; }
        
        [JsonProperty("members_removed")]
        public string MembersRemoved { get; set; }
        
        [JsonProperty("channel_id")]
        public Snowflake ChannelId { get; set; }
        
        [JsonProperty("message_id")]
        public Snowflake MessageId { get; set; }
        
        [JsonProperty("count")]
        public string Count { get; set; }
        
        [JsonProperty("id")]
        public Snowflake Id { get; set; }
        
        [JsonProperty("type")]
        public string Type { get; set; }
        
        [JsonProperty("role_name")]
        public string RoleName { get; set; }
    }
}
