using Newtonsoft.Json;

namespace Oxide.Ext.Discord.Entities.Gatway.Events
{
    public class GuildIntergrationsUpdate
    {
        [JsonProperty("guild_id")]
        public string GuildId { get; set; }
    }
}
