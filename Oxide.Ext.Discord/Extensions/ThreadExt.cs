using System;
using System.Threading;

namespace Oxide.Ext.Discord.Extensions
{
    public static class ThreadExt
    {
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