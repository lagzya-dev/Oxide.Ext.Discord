using System;
using System.Threading;

namespace Oxide.Ext.Discord.Helpers
{
    public class AdjustableSemaphore
    {
        private readonly object _syncRoot = new object();
        private int _available;
        private int _maximum;

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
                    
                    _available += value - _maximum;
                    _maximum = value;
                    Monitor.PulseAll(_syncRoot);
                }
            }
        }

        public void WaitOne()
        {
            lock (_syncRoot)
            {
                while (_available <= 0)
                {
                    Monitor.Wait(_syncRoot);
                }
                _available--;
            }
        }

        public void Release()
        {
            lock (_syncRoot)
            {
                if (_available + 1 < _maximum)
                {
                    _available += 1;
                    Monitor.Pulse(_syncRoot);
                }
                else
                {
                    throw new SemaphoreFullException($"Adding 1 the given count to the semaphore would cause it to exceed its maximum count.");
                }
            }
        }
    }
}