using Oxide.Ext.Discord.Entities.Permissions;

namespace Oxide.Ext.Discord.Exceptions.Entities.Guild
{
    /// <summary>
    /// Represents an exception for invalid guild roles
    /// </summary>
    public class InvalidGuildRoleException : BaseDiscordException
    {
        private InvalidGuildRoleException(string message) : base(message) { }

        internal static void ThrowIfInvalidRoleName(string name)
        {
            const int maxLength = 100;
            
            if (string.IsNullOrEmpty(name))
            {
                throw new InvalidGuildRoleException($"{nameof(DiscordRole)}.{nameof(DiscordRole.Name)} cannot be null or empty");
            }
            
            if (name.Length > maxLength)
            {
                throw new InvalidGuildRoleException($"{nameof(DiscordRole)}.{nameof(DiscordRole.Name)} cannot be more than {maxLength} characters");
            }
        }
    }
}