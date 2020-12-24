using Newtonsoft.Json;
using Oxide.Ext.Discord.Entities.Users;

namespace Oxide.Ext.Discord.Entities.Gatway.Events
{
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class GuildMemberRemove
    {
        [JsonProperty("guild_id")]
        public string GuildId { get; set; }

        [JsonProperty("user")]
        public DiscordUser User { get; set; }
    }
}
