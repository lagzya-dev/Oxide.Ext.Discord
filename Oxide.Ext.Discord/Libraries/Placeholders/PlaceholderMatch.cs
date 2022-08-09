using System.Text.RegularExpressions;

namespace Oxide.Ext.Discord.Libraries.Placeholders
{
    public struct PlaceholderMatch
    {
        public readonly string Name;
        public readonly string Format;
        public readonly ushort Index;
        public readonly ushort Length;

        public PlaceholderMatch(Match match)
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