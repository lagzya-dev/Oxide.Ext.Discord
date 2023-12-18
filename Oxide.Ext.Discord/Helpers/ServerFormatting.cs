using Oxide.Ext.Discord.Entities;
using Oxide.Ext.Discord.Extensions;
using Oxide.Ext.Discord.Libraries;

namespace Oxide.Ext.Discord.Helpers
{
    /// <summary>
    /// Server Text Formatting
    /// </summary>
    public static class ServerFormatting
    {
        /// <summary>
        /// Color Text with the given color
        /// </summary>
        /// <param name="text">Text to color</param>
        /// <param name="color">Color of the text</param>
        /// <returns></returns>
        public static string Color(string text, DiscordColor color) => $"[{color.ToHex()}]{text}[/#]";
        
        /// <summary>
        /// Color the placeholder text with a given color
        /// </summary>
        /// <param name="key">PlaceholderKey to color</param>
        /// <param name="color">Color of the text</param>
        /// <returns></returns>
        public static string Color(this PlaceholderKey key, DiscordColor color) => $"[{color.ToHex()}]{key.ToString()}[/#]";
    }
}