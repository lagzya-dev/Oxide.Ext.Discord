using Oxide.Ext.Discord.Entities.Guilds;

namespace Oxide.Ext.Discord.Exceptions.Entities.Guild
{
    /// <summary>
    /// Represents an exception in guild
    /// </summary>
    public class InvalidGuildException : BaseDiscordException
    {
        private InvalidGuildException(string message) : base(message) { }
        
        internal static void ThrowIfInvalidName(string name, bool allowNullOrEmpty)
        {
            const int minLength = 2;
            const int maxLength = 100;
            
            if (!allowNullOrEmpty && (string.IsNullOrEmpty(name) || name.Length < minLength))
            {
                throw new InvalidGuildException($"{nameof(GuildCreate)}.{nameof(GuildCreate.Name)} cannot be less than {minLength} character");
            }
            
            if (name.Length > maxLength)
            {
                throw new InvalidGuildException($"{nameof(GuildCreate)}.{nameof(GuildCreate.Name)} cannot be more than {maxLength} characters");
            }
        }
    }
}