using System;
using System.Collections.Concurrent;
using Oxide.Ext.Discord.Exceptions.Pooling;
using Oxide.Ext.Discord.Logging;
using Oxide.Ext.Discord.Plugins;

namespace Oxide.Ext.Discord.Types.Pooling
{
    /// <summary>
    /// Represents a BasePool in Discord
    /// </summary>
    /// <typeparam name="TPooled">Type being pooled</typeparam>
    /// <typeparam name="TPool">Type of the pool</typeparam>
    public abstract class BasePool<TPooled, TPool> : IPool<TPooled> 
        where TPooled : class 
        where TPool : BasePool<TPooled, TPool>, new()
    {
        /// <summary>
        /// Plugin Pool for this pool
        /// </summary>
        protected DiscordPluginPool PluginPool;
        
        private TPooled[] _pool;
        private int _index;
        private readonly object _lock = new object();
        private PoolSize _size;
        
        private static readonly ConcurrentDictionary<PluginId, TPool> Pools = new ConcurrentDictionary<PluginId, TPool>();

        private void InitPool(DiscordPluginPool pluginPool)
        {
            lock (_lock)
            {
                _size = GetPoolSize(pluginPool.Settings);
                InvalidPoolException.ThrowIfInvalidPoolSize(_size);
                PluginPool = pluginPool;
                pluginPool.AddPool(this);
                _pool = new TPooled[_size.StartingSize];
            }
        }

        /// <summary>
        /// Returns the pool size from the pool settings for the pool
        /// </summary>
        /// <param name="settings"></param>
        /// <returns></returns>
        protected abstract PoolSize GetPoolSize(PoolSettings settings);

        /// <summary>
        /// Returns a pool for the given plugin pool
        /// </summary>
        /// <param name="pluginPool"><see cref="DiscordPluginPool"/> to get the pool from</param>
        /// <returns></returns>
        public static TPool ForPlugin(DiscordPluginPool pluginPool)
        {
            if (!Pools.TryGetValue(pluginPool.PluginId, out TPool pool))
            {
                pool = new TPool();
                Pools[pluginPool.PluginId] = pool;
                pool.InitPool(pluginPool);
            }

            return pool;
        }

        /// <summary>
        /// Returns an element from the pool if it exists else it creates a new one
        /// </summary>
        /// <returns></returns>
        public TPooled Get()
        {
            TPooled item = null;
            lock (_lock)
            {
                if (_index == _pool.Length && _size.CanResize(_pool.Length))
                {
                    int nextSize = _size.GetNextSize(_pool.Length);
                    DiscordExtension.GlobalLogger.Debug("{0} Resizing Pool {1} Current Size: {2} Next Size: {3}", PluginPool.PluginName, GetType(), _pool.Length, nextSize);
                    Array.Resize(ref _pool, nextSize);
                }

                if (_index < _pool.Length)
                {
                    item = _pool[_index];
                    _pool[_index] = null;
                    _index++;
                }
                else
                {
                    DiscordExtension.GlobalLogger.Warning("{0} Pool {1} is leaking entities!!! {2}/{3}", PluginPool.PluginName, GetType(), _index, _pool.Length);
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
        protected abstract TPooled CreateNew();

        /// <summary>
        /// Frees an item back to the pool
        /// </summary>
        /// <param name="item">Item being freed</param>
        public void Free(TPooled item) => Free(ref item);

        private void Free(ref TPooled item)
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
        
        ///<inheritdoc/>
        public void OnPluginUnloaded(DiscordPluginPool pluginPool)
        {
            Pools.TryRemove(pluginPool.PluginId, out TPool _);
        }

        /// <summary>
        /// Clears the pool of all pooled objects and resets state to when the pool was first created
        /// </summary>
        public void ClearPoolEntities()
        {
            lock (_lock)
            {
                for (int i = _pool.Length - 1; i >= 0; i--)
                {
                    _pool[i] = null;
                }
                _index = 0;
            }
        }

        /// <summary>
        /// Wipes all the pools for this type
        /// </summary>
        public void RemoveAllPools()
        {
            Pools.Clear();
        }

        /// <summary>
        /// Called when an item is retrieved from the pool
        /// </summary>
        /// <param name="item">Item being retrieved</param>
        protected virtual void OnGetItem(TPooled item)
        {
            
        }
        
        /// <summary>
        /// Returns if an item can be freed to the pool
        /// </summary>
        /// <param name="item">Item to be freed</param>
        /// <returns>True if can be freed; false otherwise</returns>
        protected virtual bool OnFreeItem(ref TPooled item)
        {
            return true;
        }
    }
}