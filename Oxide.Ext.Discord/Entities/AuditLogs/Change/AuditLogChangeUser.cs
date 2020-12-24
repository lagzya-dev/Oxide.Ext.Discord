using Newtonsoft.Json;

namespace Oxide.Ext.Discord.Entities.AuditLogs.Change
{
    public class AuditLogChangeUser
    {
        [JsonProperty("deaf")]
        public bool Deaf { get; set; }

        [JsonProperty("mute")]
        public bool Mute { get; set; }

        [JsonProperty("nick")]
        public string Nick { get; set; }

        [JsonProperty("avatar_hash")]
        public string AvatarHash { get; set; }
    }
}