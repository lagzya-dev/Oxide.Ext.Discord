using Oxide.Ext.Discord.Pooling.Entities;

namespace Oxide.Ext.Discord.Pooling.Pools
{
    /// <summary>
    /// Represents a pool for <see cref="Boxed{T}"/>;
    /// </summary>
    /// <typeparam name="T">Type that will be in the boxed object</typeparam>
    internal class BoxedPool<T> : BasePool<Boxed<T>, BoxedPool<T>>
    {
        protected override PoolSize GetPoolSize(PoolSettings settings) => new PoolSize(32, 512);
        
        protected override void OnGetItem(Boxed<T> item)
        {
            item.LeavePool();
        }

        protected override Boxed<T> CreateNew() => new Boxed<T>();
    }
}