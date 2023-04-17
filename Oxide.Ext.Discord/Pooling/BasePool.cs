using System;
using Oxide.Core.Plugins;
using Oxide.Ext.Discord.Extensions;
using Oxide.Ext.Discord.Logging;
using Oxide.Plugins;

namespace Oxide.Ext.Discord.Pooling
{
    /// <summary>
    /// Represents a BasePool in Discord
    /// </summary>
    /// <typeparam name="TPooled">Type being pooled</typeparam>
    public abstract class BasePool<TPooled> : IPool<TPooled>
        where TPooled : class
    {
        /// <summary>
        /// Plugin Pool for this pool
        /// </summary>
        protected DiscordPluginPool PluginPool;
        
        private TPooled[] _pool;
        private int _index;
        private readonly object _lock = new object();
        private static readonly Hash<Plugin, BasePool<TPooled>> Pools = new Hash<Plugin, BasePool<TPooled>>();

        /// <summary>
        /// Base Pool Constructor
        /// </summary>
        /// <param name="size">Max Size of the pool</param>
        protected BasePool(int size)
        {
            _pool = new TPooled[size];
        }

        private void InitPool(DiscordPluginPool pluginPool)
        {
            PluginPool = pluginPool;
            pluginPool.AddPool(this);
        }

        /// <summary>
        /// Returns a pool for the given plugin pool
        /// </summary>
        /// <param name="pluginPool"><see cref="DiscordPluginPool"/> to get the pool from</param>
        /// <returns></returns>
        public static BasePool<TPooled> ForPlugin<T>(DiscordPluginPool pluginPool) where T : BasePool<TPooled>, new()
        {
            Plugin plugin = pluginPool.Plugin;
            BasePool<TPooled> pool = Pools[plugin];
            if (pool == null)
            {
                pool = new T();
                pool.InitPool(pluginPool);
                Pools[plugin] = pool;
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
                if (_index < _pool.Length)
                {
                    item = _pool[_index];
                    _pool[_index] = null;
                    _index++;
                }
                else
                {
                    DiscordExtension.GlobalLogger.Warning("{0} Pool {1} is leaking entities!!! {2}/{3}", PluginPool.Plugin.FullName(), GetType(), _index, _pool.Length);
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

        public void Resize(int newSize)
        {
            if (_index > newSize)
            {
                throw new ArgumentOutOfRangeException($"newSize: {newSize} is less than the current index: {_index}");
            }
            
            Array.Resize(ref _pool, newSize);
        }

        ///<inheritdoc/>
        public void OnPluginUnloaded(Plugin plugin)
        {
            Pools.Remove(plugin);
        }

        /// <summary>
        /// Clears the pool of all pooled objects and resets state to when the pool was first created
        /// </summary>
        public void Clear()
        {
            lock (_lock)
            {
                for (int i = _index; i >= 0; i--)
                {
                    _pool[i] = null;
                }
                _index = 0;
            }
        }

        /// <summary>
        /// Wipes all the pools for this type
        /// </summary>
        public void Wipe()
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