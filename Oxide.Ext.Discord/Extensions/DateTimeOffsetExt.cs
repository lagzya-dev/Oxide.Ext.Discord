using System;
using System.Threading;
using System.Threading.Tasks;

namespace Oxide.Ext.Discord.Extensions
{
    /// <summary>
    /// <see cref="DateTimeOffset"/> Extensions
    /// </summary>
    public static class DateTimeOffsetExt
    {
        private static readonly TimeSpan Padding = TimeSpan.FromMilliseconds(25);
        
        /// <summary>
        /// Delay until the DateTimeOffset with 25ms padding
        /// </summary>
        /// <param name="time"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public static async ValueTask DelayUntil(this DateTimeOffset time, CancellationToken token) => await Task.Delay(time - DateTimeOffset.UtcNow + Padding, token).ConfigureAwait(false);
    }
}