using System.ComponentModel;
using Newtonsoft.Json;
using Oxide.Ext.Discord.Json.Converters;

namespace Oxide.Ext.Discord.Entities.Interactions.MessageComponents.SelectMenus
{
    /// <summary>
    /// Represents a <a href="https://discord.com/developers/docs/interactions/message-components#select-menu-object-select-default-value-structure">Select Menus Default Value Type</a> within discord.
    /// </summary>
    [JsonConverter(typeof(DiscordEnumConverter))]
    public enum SelectMenuDefaultValueType
    {
        /// <summary>
        /// Select Menu Default Value Type is User
        /// </summary>
        [Description("user")]
        User,
        
        /// <summary>
        /// Select Menu Default Value Type is Role
        /// </summary>
        [Description("role")]
        Role,
        
        /// <summary>
        /// Select Menu Default Value Type is Channel
        /// </summary>
        [Description("channel")]
        Channel
    }
}