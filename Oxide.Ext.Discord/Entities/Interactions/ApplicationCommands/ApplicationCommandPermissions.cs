using Newtonsoft.Json;

namespace Oxide.Ext.Discord.Entities.Interactions.ApplicationCommands
{
    /// <summary>
    /// Represents <a href="https://discord.com/developers/docs/interactions/slash-commands#guildapplicationcommandpermissions">ApplicationCommandPermissions</a>
    /// </summary>
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class ApplicationCommandPermissions
    {
        /// <summary>
        /// The ID of the role or user
        /// </summary>
        [JsonProperty("id")]
        public Snowflake Id { get; set; }
        
        /// <summary>
        /// The type of permissions
        /// <see cref="ApplicationCommandPermissionType"/>
        /// </summary>
        [JsonProperty("type")]
        public ApplicationCommandPermissionType Type { get; set; }
        
        /// <summary>
        /// True to allow, False to disallow
        /// </summary>
        [JsonProperty("permission")]
        public bool Permission { get; set; } 
    }
}