using System.Runtime.Serialization;

namespace Oxide.Ext.Discord.Entities.Guilds
{
    /// <summary>
    /// Represents <a href="https://discord.com/developers/docs/resources/guild#guild-object-mfa-level">MFA Level</a>
    /// </summary>
    public enum GuildMFALevel
    {
        /// <summary>
        /// Guild does not require MFA
        /// </summary>
        [EnumMember(Value = "NONE")]
        None = 0,
        
        /// <summary>
        /// Guild requires elevated MFA
        /// </summary>
        [EnumMember(Value = "ELEVATED")]
        Elevated = 1
    }
}
