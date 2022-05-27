using Oxide.Plugins;

namespace Oxide.Ext.Discord.Pooling
{
    /// <summary>
    /// Represents a pool for Hash&lt;TKey, TValue&gt;
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    public class HashPool<TKey, TValue> : BasePool<Hash<TKey, TValue>>
    {
        internal static readonly IPool<Hash<TKey, TValue>> Instance;
        
        static HashPool()
        {
            Instance = new HashPool<TKey, TValue>();
        }

        private HashPool() : base(128) { }
        
        ///<inheritdoc/>
        protected override bool OnFreeItem(ref Hash<TKey, TValue> item)
        {
            item.Clear();
            return true;
        }
    }
}