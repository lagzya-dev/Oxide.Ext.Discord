
namespace Oxide.Ext.Discord.Exceptions.Entities.Channels
{
    /// <summary>
    /// Represents an exception for channel threads
    /// </summary>
    public class InvalidForumTagException : BaseDiscordException
    {
        private InvalidForumTagException(string message) : base(message) { }

        internal static void ThrowIfInvalidName(string name)
        {
            if (!string.IsNullOrEmpty(name) && name.Length > 20)
            {
                throw new InvalidForumTagException("Forum Tag Name cannot be more than 20 characters");
            }
        }
    }
}