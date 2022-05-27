using System;
using System.Threading;

namespace Oxide.Ext.Discord.Extensions
{
    /// <summary>
    /// Thread Extensions
    /// </summary>
    public static class ThreadExt
    {
        /// <summary>
        /// Sleep the thread until the given <see cref="DateTimeOffset"/>
        /// If sleepTime is in the past no thread sleeping occurs
        /// </summary>
        /// <param name="sleepTime">Time to sleep until</param>
        public static void SleepUntil(DateTimeOffset sleepTime)
        {
            int sleep = (int)(sleepTime - DateTimeOffset.UtcNow).TotalMilliseconds + 1;
            if (sleep > 0)
            {
                Thread.Sleep(sleep);
            }
        }
    }
}