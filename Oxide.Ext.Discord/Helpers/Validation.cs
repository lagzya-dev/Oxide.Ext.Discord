using System.Text.RegularExpressions;

namespace Oxide.Ext.Discord.Helpers
{
    /// <summary>
    /// Provides helper methods for validation
    /// </summary>
    public static class Validation
    {
        internal static readonly Regex EmojiValidation = new Regex("^<.+:[0-9]+>$", RegexOptions.Compiled);
        
        /// <summary>
        /// Validates that the emoji string entered is valid.
        /// </summary>
        /// <param name="emoji"></param>
        /// <returns></returns>
        public static string ValidateEmoji(string emoji)
        {
            if (string.IsNullOrEmpty(emoji))
            {
                return "Emoji string cannot be null or empty.";
            }

            if (emoji.Length < 2 || !char.IsSurrogatePair(emoji[0], emoji[1]) && !EmojiValidation.IsMatch(emoji))
            {
                return "Emoji string is not in the correct format. If using a normal emoji please use the unicode character for that emoji. If using a custom emoji the format must be <emojiName:emojiId>";
            }

            return null;
        }
    }
}