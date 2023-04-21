using Oxide.Ext.Discord.Entities.Permissions;

namespace Oxide.Ext.Discord.Extensions
{
    /// <summary>
    /// Extensions for Discord Color
    /// </summary>
    public static class DiscordColorExt
    {
        /// <summary>
        /// Returns the color as a hex color code
        /// </summary>
        /// <returns></returns>
        public static string ToHex(this DiscordColor color) => $"#{color.Color.ToString("X6")}";
    }
}