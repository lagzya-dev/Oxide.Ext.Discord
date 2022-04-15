using System.Text.RegularExpressions;

namespace Oxide.Ext.Discord.Exceptions.Entities.Images
{
    /// <summary>
    /// Represents an exception in discord image data
    /// </summary>
    public class InvalidImageDataException : BaseDiscordException
    {
        internal InvalidImageDataException(string message) : base(message) { }

        internal static void ThrowIfInvalidBase64String(Match match, string image)
        {
            if (!match.Success || match.Groups.Count != 2)
            {
                throw new InvalidImageDataException($"'{image}' is not valid. Please make sure it's in the following format: https://discord.com/developers/docs/reference#image-data");
            }
        }

        internal static void ThrowIfInvalidImageBytes(byte[] image)
        {
            if (image == null || image.Length == 0)
            {
                throw new InvalidImageDataException("Image Byte[] cannot be null or empty");
            }
        }
    }
}