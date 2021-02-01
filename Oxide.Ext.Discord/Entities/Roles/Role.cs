using Newtonsoft.Json;
using Oxide.Ext.Discord.Entities.Channels;

namespace Oxide.Ext.Discord.Entities.Roles
{
    /// <summary>
    /// Represents <a href="https://discord.com/developers/docs/topics/permissions#role-object">Role Structure</a>
    /// </summary>
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class Role
    {
        /// <summary>
        /// Role id
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }

        /// <summary>
        /// Role name
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Role Color
        /// </summary>
        [JsonProperty("color")]
        public DiscordColor Color { get; set; }

        /// <summary>
        /// If this role is pinned in the user listing
        /// </summary>
        [JsonProperty("hoist")]
        public bool? Hoist { get; set; }

        /// <summary>
        /// Position of this role
        /// </summary>
        [JsonProperty("position")]
        public int Position { get; set; }

        /// <summary>
        /// Role Permissions
        /// </summary>
        [JsonProperty("permissions")]
        public PermissionFlags Permissions { get; set; }

        /// <summary>
        /// Whether this role is managed by an integration
        /// </summary>
        [JsonProperty("managed")]
        public bool? Managed { get; set; }

        /// <summary>
        /// Whether this role is mentionable
        /// </summary>
        [JsonProperty("mentionable")]
        public bool? Mentionable { get; set; }
        
        /// <summary>
        /// The tags this role has
        /// </summary>
        [JsonProperty("tags")]
        public RoleTags Tags { get; set; }
    }
}
