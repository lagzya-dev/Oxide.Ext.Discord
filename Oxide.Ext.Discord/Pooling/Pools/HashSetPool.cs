using System.Collections.Generic;

namespace Oxide.Ext.Discord.Pooling.Pools
{
    /// <summary>
    /// Represents a pool for Hash&lt;TKey, TValue&gt;
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal class HashSetPool<T> : BasePool<HashSet<T>>
    {
        public static HashSetPool<T> ForPlugin(DiscordPluginPool pluginPool) => ForPlugin<HashSetPool<T>>(pluginPool);
        
        protected override int GetPoolSize(PoolSettings settings) => settings.HashSetPoolSize;
        
        protected override HashSet<T> CreateNew() => new HashSet<T>();
        
        ///<inheritdoc/>
        protected override bool OnFreeItem(ref HashSet<T> item)
        {
            item.Clear();
            return true;
        }
    }
}