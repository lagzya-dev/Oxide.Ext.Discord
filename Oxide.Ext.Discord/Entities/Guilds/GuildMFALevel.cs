using System.ComponentModel;

namespace Oxide.Ext.Discord.Entities.Guilds
{
    /// <summary>
    /// Represents <a href="https://discord.com/developers/docs/resources/guild#guild-object-mfa-level">MFA Level</a>
    /// </summary>
    public enum GuildMfaLevel : byte
    {
        /// <summary>
        /// Guild does not require MFA
        /// </summary>
        [Description("NONE")]
        None = 0,
        
        /// <summary>
        /// Guild requires elevated MFA
        /// </summary>
        [Description("ELEVATED")]
        Elevated = 1
    }
}
