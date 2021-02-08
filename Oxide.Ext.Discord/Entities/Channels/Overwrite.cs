using Newtonsoft.Json;
using Oxide.Ext.Discord.Helpers.Interfaces;

namespace Oxide.Ext.Discord.Entities.Channels
{
    /// <summary>
    /// Represents a <a href="https://discord.com/developers/docs/resources/channel#overwrite-object-overwrite-structure">Overwrite Structure</a>
    /// </summary>
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class Overwrite : IGetEntityId
    {
        /// <summary>
        /// Role or user ID
        /// </summary>
        [JsonProperty("id")]
        public Snowflake Id { get; set; }
        
        /// <summary>
        /// Permissions allowed <see cref="PermissionFlags"/>
        /// </summary>
        [JsonProperty("allow")]
        public PermissionFlags? Allow { get; set; }
        
        /// <summary>
        /// Permissions denied <see cref="PermissionFlags"/>
        /// </summary>
        [JsonProperty("deny")]
        public PermissionFlags? Deny { get; set; }
        
        /// <summary>
        /// Permission Type <see cref="PermissionType"/>
        /// </summary>
        [JsonProperty("type")]
        public PermissionType Type { get; set; }

        /// <summary>
        /// Returns the entity ID for the entity
        /// </summary>
        /// <returns></returns>
        public Snowflake GetEntityId()
        {
            return Id;
        }
    }
}
