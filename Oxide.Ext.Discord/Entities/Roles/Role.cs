using Newtonsoft.Json;
using Oxide.Ext.Discord.Entities.Channels;

namespace Oxide.Ext.Discord.Entities.Roles
{
    public class Role
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("color")]
        public DiscordColor Color { get; set; }

        [JsonProperty("hoist")]
        public bool? Hoist { get; set; }

        [JsonProperty("position")]
        public PermissionFlags Position { get; set; }

        [JsonProperty("permissions")]
        public int? Permissions { get; set; }

        [JsonProperty("managed")]
        public bool? Managed { get; set; }

        [JsonProperty("mentionable")]
        public bool? Mentionable { get; set; }
    }
}
