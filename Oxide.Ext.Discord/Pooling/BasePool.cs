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
    /// <typeparam name="TPool">Type of the pool</typeparam>
    public abstract class BasePool<TPool, TPooled> : IPool<TPooled>
        where TPool : BasePool<TPool, TPooled>, new()
        where TPooled : class
    {
        protected DiscordPluginPool PluginPool;
        
        private readonly TPooled[] _pool;
        private int _index;
        private readonly object _lock = new object();
        private static readonly Hash<string, TPool> Pools = new Hash<string, TPool>();

        /// <summary>
        /// Base Pool Constructor
        /// </summary>
        /// <param name="maxSize">Max Size of the pool</param>
        protected BasePool(int maxSize)
        {
            _pool = new TPooled[maxSize];
        }

        public static TPool ForPlugin(DiscordPluginPool pluginPool)
        {
            Plugin plugin = pluginPool.Plugin;
            string id = plugin.Id();
            TPool pool = Pools[id];
            if (pool == null)
            {
                pool = new TPool
                {
                    PluginPool = pluginPool
                };
                pluginPool.AddPool(pool);
                Pools[id] = pool;
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

        public void OnPluginUnloaded(Plugin plugin)
        {
            Pools.Remove(plugin.Id());
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