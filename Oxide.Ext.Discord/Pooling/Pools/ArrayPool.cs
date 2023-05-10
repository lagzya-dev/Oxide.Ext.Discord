using System;
using Oxide.Ext.Discord.Logging;
using Oxide.Ext.Discord.Singleton;

namespace Oxide.Ext.Discord.Pooling.Pools
{
    internal class ArrayPool<TPooled> : Singleton<ArrayPool<TPooled>>
    {
        private const int MaxArraySize = 64;
        private readonly InternalArrayPool[] _pool = new InternalArrayPool[MaxArraySize];
        
        private ArrayPool() { }

        public TPooled[] Get(int size)
        {
            if (size < 0) throw new ArgumentOutOfRangeException(nameof(size), "Cannot be less than 0");
            if (size == 0)
            {
                return Array.Empty<TPooled>();
            }

            if (size > MaxArraySize)
            {
                throw new ArgumentOutOfRangeException(nameof(size), $"Cannot be greater than {MaxArraySize}");
            }
            
            InternalArrayPool pool = _pool[--size];
            if (pool == null)
            {
                pool = new InternalArrayPool(size);
                _pool[size] = pool;
            }

            return pool.Get();
        }

        public void Free(ref TPooled[] array)
        {
            int size = array.Length;
            if (size == 0)
            {
                return;
            }
            
            if (size > MaxArraySize)
            {
                throw new ArgumentOutOfRangeException(nameof(array), $"Array length cannot be greater than {MaxArraySize}");
            }
            
            InternalArrayPool pool = _pool[--size];
            pool?.Free(ref array);
        }

        private class InternalArrayPool
        {
            private int _index;
            private readonly TPooled[][] _pool = new TPooled[MaxArraySize][];
            private readonly object _lock = new object();
            private readonly int _arraySize;

            public InternalArrayPool(int arraySize)
            {
                _arraySize = arraySize;
            }
            
            public TPooled[] Get()
            {
                TPooled[] array = null;
                lock (_lock)
                {
                    if (_index < _pool.Length)
                    {
                        array = _pool[_index];
                        _pool[_index++] = null;
                    }
                    else
                    {
                        DiscordExtension.GlobalLogger.Warning("Pool {0} is leaking entities!!! {1}/{2}", GetType(), _index, _pool.Length);
                    }
                }

                return array ?? new TPooled[_arraySize];
            }
            
            public void Free(ref TPooled[] item)
            {
                if (item == null)
                {
                    return;
                }

                for (int index = 0; index < item.Length; ++index)
                {
                    item[index] = default(TPooled);
                }

                lock (_lock)
                {
                    if (_index != 0)
                    {
                        _pool[--_index] = item;
                    }
                }
            
                item = null;
            }
        }
    }
}