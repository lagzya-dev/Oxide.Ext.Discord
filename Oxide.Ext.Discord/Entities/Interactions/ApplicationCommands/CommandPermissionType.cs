using System.Runtime.Serialization;

namespace Oxide.Ext.Discord.Entities.Interactions.ApplicationCommands
{
    /// <summary>
    /// Represents <a href="https://discord.com/developers/docs/interactions/application-commands#application-command-permissions-object-application-command-permission-type">ApplicationCommandPermissionType</a>
    /// </summary>
    public enum CommandPermissionType
    {
        /// <summary>
        /// This permissions uses Role ID
        /// </summary>
        [EnumMember(Value = "ROLE")]
        Role = 1,
        
        /// <summary>
        /// This permission uses User ID
        /// </summary>
        [EnumMember(Value = "USER")]
        User = 2
    }
}