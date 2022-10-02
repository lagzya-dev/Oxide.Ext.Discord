using System.Collections.Generic;

namespace Oxide.Ext.Discord.Pooling
{
    /// <summary>
    /// Represents a pool for list&lt;T&gt;
    /// </summary>
    /// <typeparam name="T">Type that will be in the list</typeparam>
    internal class ListPool<T> : BasePool<List<T>>
    {
        internal static readonly IPool<List<T>> Instance = new ListPool<T>();
        
        static ListPool()
        {
            DiscordPool.Pools.Add(Instance);
        }

        private ListPool() : base(512) { }
        
        protected override List<T> CreateNew() => new List<T>();
        
        ///<inheritdoc/>
        protected override bool OnFreeItem(ref List<T> item)
        {
            item.Clear();
            return true;
        }
    }
}