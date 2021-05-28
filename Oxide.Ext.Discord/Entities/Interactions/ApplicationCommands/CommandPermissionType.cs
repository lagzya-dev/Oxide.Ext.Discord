using System.ComponentModel;

namespace Oxide.Ext.Discord.Entities.Interactions.ApplicationCommands
{
    /// <summary>
    /// Represents <a href="https://discord.com/developers/docs/interactions/slash-commands#applicationcommandpermissiontype">ApplicationCommandPermissionType</a>
    /// </summary>
    public enum CommandPermissionType
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
        User = 2
    }
}