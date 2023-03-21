using System.Collections.Generic;

namespace Oxide.Ext.Discord.Pooling
{
    /// <summary>
    /// Represents a pool for Hash&lt;TKey, TValue&gt;
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal class HashSetPool<T> : BasePool<HashSetPool<T>, HashSet<T>>
    {
        public HashSetPool() : base(256) { }
        
        protected override HashSet<T> CreateNew() => new HashSet<T>();
        
        ///<inheritdoc/>
        protected override bool OnFreeItem(ref HashSet<T> item)
        {
            item.Clear();
            return true;
        }
    }
}