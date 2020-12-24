using Newtonsoft.Json;

namespace Oxide.Ext.Discord.Entities.Guilds.Integrations
{
    public class Account
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
