using Newtonsoft.Json;
using Oxide.Ext.Discord.Entities.Guilds;

namespace Oxide.Ext.Discord.Entities.Gatway.Events
{
    public class GuildMemberAdd : GuildMember
    {
        [JsonProperty("guild_id")]
        public string GuildId { get; set; }
    }
}
