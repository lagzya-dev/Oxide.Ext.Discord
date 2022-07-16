using System;
using System.Threading;

namespace Oxide.Ext.Discord.Threading
{
    internal class AdjustableSemaphore
    {
        private readonly object _syncRoot = new object();
        private int _maximum;
        public int Available { get; private set; }

        public AdjustableSemaphore(int maximumCount)
        {
            MaximumCount = maximumCount;
        }

        public int MaximumCount
        {
            get
            {
                lock (_syncRoot)
                {
                    return _maximum;
                }
            }
            set
            {
                lock (_syncRoot)
                {
                    if (value < 0)
                    {
                        throw new ArgumentException("Must be greater than or equal to 0.", nameof(MaximumCount));
                    }
                    
                    Available += value - _maximum;
                    _maximum = value;
                    Monitor.PulseAll(_syncRoot);
                }
            }
        }

        public void WaitOne()
        {
            lock (_syncRoot)
            {
                while (Available <= 0)
                {
                    Monitor.Wait(_syncRoot);
                }
                Available--;
            }
        }

        public void Release()
        {
            lock (_syncRoot)
            {
                if (Available + 1 <= _maximum)
                {
                    Available += 1;
                    Monitor.Pulse(_syncRoot);
                }
                else
                {
                    throw new Exception($"Adding 1 the given count to the semaphore would cause it to exceed its maximum count of {_maximum}.");
                }
            }
        }

        public void AllowAllThrough()
        {
            MaximumCount = int.MaxValue;
        }
    }
}