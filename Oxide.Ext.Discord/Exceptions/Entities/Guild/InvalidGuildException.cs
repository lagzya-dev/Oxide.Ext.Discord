using Oxide.Ext.Discord.Entities.Images;
using Oxide.Ext.Discord.Exceptions.Entities.Emojis;

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
            if (!allowNullOrEmpty && (string.IsNullOrEmpty(name) || name.Length < 2))
            {
                throw new InvalidGuildException("Name cannot be less than 2 character");
            }
            
            if (name.Length > 100)
            {
                throw new InvalidGuildException("Name cannot be more than 100 characters");
            }
        }
        
        internal static void ThrowIfInvalidImageData(DiscordImageData image)
        {
            if (!image.IsValid())
            {
                throw new InvalidEmojiException("ImageData is not a valid image");
            }
        }
        
        internal static void ThrowIfInvalidImageData(DiscordImageData? image)
        {
            if (image.HasValue)
            {
                ThrowIfInvalidImageData(image.Value);
            }
        }
    }
}