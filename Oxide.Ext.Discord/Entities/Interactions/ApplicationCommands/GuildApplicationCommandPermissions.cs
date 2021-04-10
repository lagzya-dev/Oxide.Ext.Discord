using System.Collections.Generic;
using Newtonsoft.Json;

namespace Oxide.Ext.Discord.Entities.Interactions.ApplicationCommands
{
    /// <summary>
    /// Represents <a href="https://discord.com/developers/docs/interactions/slash-commands#applicationcommandpermissions">ApplicationCommandPermissions</a>
    /// </summary>
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class GuildApplicationCommandPermissions
    {
        /// <summary>
        /// The ID of the command
        /// </summary>
        [JsonProperty("id")]
        public Snowflake Id { get; set; }
        
        /// <summary>
        /// the ID of the application the command belongs to
        /// </summary>
        [JsonProperty("application_id")]
        public Snowflake ApplicationId { get; set; }
        
        /// <summary>
        /// The ID of the guild
        /// </summary>
        [JsonProperty("guild_id")]
        public Snowflake GuildId { get; set; }
        
        /// <summary>
        /// The permissions for the command in the guild
        /// </summary>
        [JsonProperty("permissions")]
        public List<ApplicationCommandPermissions> Permissions { get; set; }
    }
}