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
            if (!string.IsNullOrEmpty(nickname) && nickname.Length > 32)
            {
                throw new InvalidGuildMemberException($"Nickname '{nickname}' cannot be more than 32 characters");
            }
        }
    }
}