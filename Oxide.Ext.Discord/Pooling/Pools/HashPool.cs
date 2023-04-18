using Oxide.Plugins;

namespace Oxide.Ext.Discord.Pooling.Pools
{
    /// <summary>
    /// Represents a pool for Hash&lt;TKey, TValue&gt;
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    internal class HashPool<TKey, TValue> : BasePool<Hash<TKey, TValue>>
    {
        public static HashPool<TKey, TValue> ForPlugin(DiscordPluginPool pluginPool) => ForPlugin<HashPool<TKey, TValue>>(pluginPool);

        protected override int GetPoolSize(PoolSettings settings) => settings.HashPoolSize;

        protected override Hash<TKey, TValue> CreateNew() => new Hash<TKey, TValue>();
        
        ///<inheritdoc/>
        protected override bool OnFreeItem(ref Hash<TKey, TValue> item)
        {
            item.Clear();
            return true;
        }
    }
}