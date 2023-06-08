using System.ComponentModel;

namespace Oxide.Ext.Discord.Entities.Interactions.ApplicationCommands
{
    /// <summary>
    /// Represents <a href="https://discord.com/developers/docs/interactions/application-commands#application-command-permissions-object-application-command-permission-type">ApplicationCommandPermissionType</a>
    /// </summary>
    public enum CommandPermissionType : byte
    {
        /// <summary>
        /// This permissions uses Role ID
        /// </summary>
        [Description("ROLE")]
        Role = 1,
        
        /// <summary>
        /// This permission uses User ID
        /// </summary>
        [Description("USER")]
        User = 2,
        
        /// <summary>
        /// This permission uses Channel ID
        /// </summary>
        [Description("USER")]
        Channel = 2
    }
}