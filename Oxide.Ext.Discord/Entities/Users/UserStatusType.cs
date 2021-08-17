using System.ComponentModel;
using System.Runtime.Serialization;

namespace Oxide.Ext.Discord.Entities.Users
{
    /// <summary>
    /// Represents Discord User <a href="https://discord.com/developers/docs/topics/gateway#update-status-status-types">Status Types</a> 
    /// </summary>
    public enum UserStatusType
    {
        /// <summary>
        /// User is online
        /// </summary>
        [EnumMember(Value = "online")] Online,
        
        /// <summary>
        /// User has Do Not Disturb
        /// </summary>
        [EnumMember(Value = "dnd")] DND,
        
        /// <summary>
        /// User is idle
        /// </summary>
        [EnumMember(Value = "idle")] Idle,
        
        /// <summary>
        /// User is invisible
        /// </summary>
        [EnumMember(Value = "invisible")] Invisible,
        
        /// <summary>
        /// User is offline
        /// </summary>
        [EnumMember(Value = "offline")] Offline
    }
}