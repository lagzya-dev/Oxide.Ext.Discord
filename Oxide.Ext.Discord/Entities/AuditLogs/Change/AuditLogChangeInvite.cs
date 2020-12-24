using Newtonsoft.Json;

namespace Oxide.Ext.Discord.Entities.AuditLogs.Change
{
    public class AuditLogChangeInvite
    {
        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("channel_id")]
        public string ChannelId { get; set; }

        [JsonProperty("inviter_id")]
        public string InviterId { get; set; }

        [JsonProperty("max_uses")]
        public int? MaxUses { get; set; }

        [JsonProperty("uses")]
        public int? Uses { get; set; }

        [JsonProperty("max_age")]
        public int? MaxAge { get; set; }

        [JsonProperty("temporary")]
        public bool Temporary { get; set; }
    }
}