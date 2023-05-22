using Oxide.Ext.Discord.Entities.Guilds;

namespace Oxide.Ext.Discord.Exceptions.Entities.Guild
{
    /// <summary>
    /// Represents an exception in guild member
    /// </summary>
    public class InvalidGuildMemberException : BaseDiscordException
    {
        private InvalidGuildMemberException(string message) : base(message) { }

        internal static void ThrowIfInvalidNickname(string nickname)
        {
            const int maxLength = 32;
            
            if (!string.IsNullOrEmpty(nickname) && nickname.Length > maxLength)
            {
                throw new InvalidGuildMemberException($"{nameof(GuildMemberUpdate)}.{nameof(GuildMemberUpdate.Nick)} cannot be more than {maxLength} characters");
            }
        }
    }
}