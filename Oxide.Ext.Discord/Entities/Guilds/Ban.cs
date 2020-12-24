using Newtonsoft.Json;
using Oxide.Ext.Discord.Entities.Users;

namespace Oxide.Ext.Discord.Entities.Guilds
{
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class Ban
    {
        [JsonProperty("reason")]
        public string Reason { get; set; }

        [JsonProperty("user")]
        public DiscordUser User { get; set; }
    }
}
