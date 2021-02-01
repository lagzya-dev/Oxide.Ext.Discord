using Newtonsoft.Json;
using Oxide.Ext.Discord.Entities.Integrations;

namespace Oxide.Ext.Discord.Entities.Gatway.Events
{
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class IntegrationCreate : Integration
    {
        [JsonProperty("guild_id")]
        public string GuildId { get; set; }
    }
}