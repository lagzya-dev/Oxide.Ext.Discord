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
        /// <summary>
        /// Delay until the DateTimeOffset with 25ms - 50ms padding
        /// </summary>
        /// <param name="time"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public static async ValueTask DelayUntil(this DateTimeOffset time, CancellationToken token) => await Task.Delay(time - DateTimeOffset.UtcNow + TimeSpan.FromMilliseconds(Core.Random.Range(25, 40)), token).ConfigureAwait(false);
        
        public static async ValueTask DelayUntil(this DateTimeOffset time, int additionalMs, CancellationToken token) => await Task.Delay(time - DateTimeOffset.UtcNow + TimeSpan.FromMilliseconds(additionalMs), token).ConfigureAwait(false);
    }
}