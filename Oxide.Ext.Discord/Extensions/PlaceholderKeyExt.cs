using Oxide.Ext.Discord.Libraries.Placeholders;

namespace Oxide.Ext.Discord.Extensions
{
    /// <summary>
    /// Extensions for placeholder keys
    /// </summary>
    public static class PlaceholderKeyExt
    {
        /// <summary>
        /// Applies a format to a given <see cref="PlaceholderKey"/>
        /// </summary>
        /// <param name="key"><see cref="PlaceholderKey"/> to apply the format to</param>
        /// <param name="format">Format to be applied</param>
        /// <returns>string placeholder containing the placeholder with the given format</returns>
        public static string WithFormat(this PlaceholderKey key, string format) => $"{{{key.Placeholder}:{format}}}";
    }
}