using Newtonsoft.Json;
using Oxide.Ext.Discord.Entities.Users;

namespace Oxide.Ext.Discord.Entities.Gatway.Events
{
    public class GuildMemberRemove
    {
        [JsonProperty("guild_id")]
        public string GuildId { get; set; }

        [JsonProperty("user")]
        public User User { get; set; }
    }
}
