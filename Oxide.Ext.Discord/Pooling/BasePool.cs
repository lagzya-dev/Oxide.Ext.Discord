using Oxide.Ext.Discord.Logging;

namespace Oxide.Ext.Discord.Pooling
{
    /// <summary>
    /// Represents a BasePool in Discord
    /// </summary>
    /// <typeparam name="T">Type being pooled</typeparam>
    public abstract class BasePool<T> : IPool<T> where T : class
    {
        private readonly T[] _pool;
        private int _index;
        private readonly object _lock = new object();

        /// <summary>
        /// Base Pool Constructor
        /// </summary>
        /// <param name="maxSize">Max Size of the pool</param>
        protected BasePool(int maxSize)
        {
            _pool = new T[maxSize];
        }
        
        /// <summary>
        /// Returns an element from the pool if it exists else it creates a new one
        /// </summary>
        /// <returns></returns>
        public T Get()
        {
            T item = null;
            lock (_lock)
            {
                if (_index < _pool.Length)
                {
                    item = _pool[_index];
                    _pool[_index] = null;
                    _index++;
                }
                else
                {
                    DiscordExtension.GlobalLogger.Warning("Pool {0} is leaking entities!!! {1}/{2}", GetType(), _index, _pool.Length);
                }
            }
            
            if (item == null)
            {
                item = CreateNew();
            }

            OnGetItem(item);
            return item;
        }

        /// <summary>
        /// Creates new type of T
        /// </summary>
        /// <returns>Newly created type of T</returns>
        protected abstract T CreateNew();

        /// <summary>
        /// Frees an item back to the pool
        /// </summary>
        /// <param name="item">Item being freed</param>
        public void Free(ref T item)
        {
            if (item == null)
            {
                return;
            }

            if (!OnFreeItem(ref item))
            {
                //DiscordExtension.GlobalLogger.Debug("Skip Free Item: {0}", typeof(T).Name);
                return;
            }

            lock (_lock)
            {
                if (_index != 0)
                {
                    _index--;
                    _pool[_index] = item;
                }
            }
            
            item = null;
        }

        /// <summary>
        /// Called when an item is retrieved from the pool
        /// </summary>
        /// <param name="item">Item being retrieved</param>
        protected virtual void OnGetItem(T item)
        {
            
        }
        
        /// <summary>
        /// Returns if an item can be freed to the pool
        /// </summary>
        /// <param name="item">Item to be freed</param>
        /// <returns>True if can be freed; false otherwise</returns>
        protected virtual bool OnFreeItem(ref T item)
        {
            return true;
        }
    }
}