using Oxide.Ext.Discord.Libraries.Placeholders;

namespace Oxide.Ext.Discord.Exceptions.Libraries.Placeholders
{
    /// <summary>
    /// Exception thrown if <see cref="PlaceholderDataKey"/> is not valid
    /// </summary>
    public class InvalidPlaceholderDataException : BaseDiscordException
    {
        private InvalidPlaceholderDataException(string message) : base(message) { }

        internal static void ThrowIfInvalid(PlaceholderDataKey key)
        {
            if (!key.IsValid)
            {
                throw new InvalidPlaceholderDataException("PlaceholderDataKey is not valid");
            }
        }
    }
}