using Oxide.Plugins;

namespace Oxide.Ext.Discord.Pooling
{
    /// <summary>
    /// Represents a pool for Hash&lt;TKey, TValue&gt;
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    internal class HashPool<TKey, TValue> : BasePool<HashPool<TKey, TValue>, Hash<TKey, TValue>>
    {
        public HashPool() : base(512) { }
        
        protected override Hash<TKey, TValue> CreateNew() => new Hash<TKey, TValue>();
        
        ///<inheritdoc/>
        protected override bool OnFreeItem(ref Hash<TKey, TValue> item)
        {
            item.Clear();
            return true;
        }
    }
}