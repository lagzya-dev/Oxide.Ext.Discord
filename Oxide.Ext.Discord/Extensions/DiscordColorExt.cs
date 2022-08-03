using Oxide.Ext.Discord.Entities.Permissions;

namespace Oxide.Ext.Discord.Extensions
{
    /// <summary>
    /// Discord Color Extension Methods
    /// </summary>
    public static class DiscordColorExt
    {
        /// <summary>
        /// Converter the Discord Color to a string hex color
        /// </summary>
        /// <param name="color">Color to convert</param>
        /// <returns>Hex string color of the <see cref="DiscordColor"/></returns>
        public static string ToHex(this DiscordColor color)
        {
            return $"#{color.Color:X6}";
        }
    }
}