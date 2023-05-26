using System.Collections.Generic;

namespace Oxide.Ext.Discord.Pooling.Pools
{
    /// <summary>
    /// Represents a pool for list&lt;T&gt;
    /// </summary>
    /// <typeparam name="T">Type that will be in the list</typeparam>
    internal class ListPool<T> : BasePool<List<T>>
    {
        public static ListPool<T> ForPlugin(DiscordPluginPool pluginPool) => ForPlugin<ListPool<T>>(pluginPool);
        
        protected override PoolSize GetPoolSize(PoolSettings settings) => settings.ListPoolSize;
        
        protected override List<T> CreateNew() => new List<T>();
        
        ///<inheritdoc/>
        protected override bool OnFreeItem(ref List<T> item)
        {
            item.Clear();
            return true;
        }
    }
}