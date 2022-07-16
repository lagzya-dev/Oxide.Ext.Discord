using System;
using System.Threading.Tasks;

namespace Oxide.Ext.Discord.Extensions
{
    /// <summary>
    /// Thread Extensions
    /// </summary>
    public static class TaskExt
    {
        /// <summary>
        /// Sleep the thread until the given <see cref="DateTimeOffset"/>
        /// If sleepTime is in the past no thread sleeping occurs
        /// </summary>
        /// <param name="sleepTime">Time to sleep until</param>
        public static Task SleepUntil(DateTimeOffset sleepTime)
        {
            return sleepTime > DateTimeOffset.UtcNow ? Task.Delay(sleepTime - DateTimeOffset.UtcNow) : Task.CompletedTask;
        }
    }
}