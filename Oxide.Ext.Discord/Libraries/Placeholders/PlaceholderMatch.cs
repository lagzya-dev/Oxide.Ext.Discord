using System.Text.RegularExpressions;

namespace Oxide.Ext.Discord.Libraries.Placeholders
{
    /// <summary>
    /// Represents a match for a placeholder
    /// </summary>
    public struct PlaceholderMatch
    {
        /// <summary>
        /// Name of the placeholder
        /// </summary>
        public readonly string Name;
        
        /// <summary>
        /// Format specified in the placeholder
        /// </summary>
        public readonly string Format;
        
        /// <summary>
        /// Index in the string of the placeholder
        /// </summary>
        public readonly ushort Index;
        
        /// <summary>
        /// Length of the placeholder
        /// </summary>
        public readonly ushort Length;

        internal PlaceholderMatch(Match match)
        {
            GroupCollection groups = match.Groups;
            Name = groups[1].Value;
            Group formatGroup = groups[2];
            Format = formatGroup.Success ? formatGroup.Value : null;
            Index = (ushort)match.Index;
            Length = (ushort)match.Length;
        }
    }
}