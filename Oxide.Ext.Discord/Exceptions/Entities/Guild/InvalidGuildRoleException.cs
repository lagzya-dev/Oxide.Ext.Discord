namespace Oxide.Ext.Discord.Exceptions.Entities.Guild
{
    public class InvalidGuildRoleException : BaseDiscordException
    {
        private InvalidGuildRoleException(string message) : base(message) { }

        internal static void ThrowIfInvalidRoleName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new InvalidGuildRoleException("Role name cannot be null or empty");
            }
            
            if (name.Length > 100)
            {
                throw new InvalidGuildRoleException("Role name cannot be more than 100 characters");
            }
        }
    }
}