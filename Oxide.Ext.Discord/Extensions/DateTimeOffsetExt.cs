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
        public static ValueTask DelayUntil(this DateTimeOffset time, CancellationToken token) => time.DelayUntil(Core.Random.Range(25, 40), token);
        
        public static async ValueTask DelayUntil(this DateTimeOffset time, int additionalMs, CancellationToken token)
        {
            TimeSpan duration = time >= DateTimeOffset.UtcNow ? time - DateTimeOffset.UtcNow : TimeSpan.Zero;
            await Task.Delay(duration + TimeSpan.FromMilliseconds(additionalMs), token).ConfigureAwait(false);
        }
    }
}