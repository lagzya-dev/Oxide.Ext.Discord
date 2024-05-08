using System;

namespace Oxide.Ext.Discord.Extensions
{
    /// <summary>
    /// <see cref="Span{T}"/> Extension Methods
    /// </summary>
    public static class SpanExt
    {
        /// <summary>
        /// Parses the next string from the input splitting on the token
        /// </summary>
        /// <param name="input">Input string</param>
        /// <param name="token">Token to split on</param>
        /// <param name="remaining">Remaining text of the span</param>
        /// <param name="parsed">The parsed string</param>
        /// <returns>True if successfully parsed; false otherwise</returns>
        public static bool TryParseNextString(this ReadOnlySpan<char> input, ReadOnlySpan<char> token, out ReadOnlySpan<char> remaining, out ReadOnlySpan<char> parsed)
        {
            if (input.Length == 0)
            {
                remaining = ReadOnlySpan<char>.Empty;
                parsed = ReadOnlySpan<char>.Empty;
                return false;
            }

            int end = input.IndexOf(token);
            if (end == -1)
            {
                remaining = ReadOnlySpan<char>.Empty;
                parsed = input;
                return true;
            }

            remaining = input.Slice(end + token.Length);
            parsed = input.Slice(0, end);
            return true;
        }
    }
}