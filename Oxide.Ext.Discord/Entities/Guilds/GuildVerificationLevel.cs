using System.Runtime.Serialization;

namespace Oxide.Ext.Discord.Entities.Guilds
{
    /// <summary>
    /// Represents <a href="https://discord.com/developers/docs/resources/guild#guild-object-verification-level">Verification Level</a>
    /// </summary>
    public enum GuildVerificationLevel
    {
        /// <summary>
        /// Unrestricted
        /// </summary>
        [EnumMember(Value = "NONE")]
        None = 0,
        
        /// <summary>
        /// Must have verified email on account
        /// </summary>
        [EnumMember(Value = "LOW")]
        Low = 1,
        
        /// <summary>
        /// Must be registered on Discord for longer than 5 minutes
        /// </summary>
        [EnumMember(Value = "MEDIUM")]
        Medium = 2,
        
        /// <summary>
        /// Must be a member of the server for longer than 10 minutes
        /// </summary>
        [EnumMember(Value = "HIGH")]
        High = 3,
        
        /// <summary>
        /// Must have a verified phone number
        /// </summary>
        [EnumMember(Value = "VERY_HIGH")]
        VeryHigh = 4
    }
}
