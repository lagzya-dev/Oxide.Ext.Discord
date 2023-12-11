using Oxide.Ext.Discord.Types.Pooling.Entities;

namespace Oxide.Ext.Discord.Types.Pooling.Pools
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
            item._pool = PluginPool;
        }

        protected override bool OnFreeItem(ref Boxed<T> item)
        {
            item._pool = null;
            return base.OnFreeItem(ref item);
        }

        protected override Boxed<T> CreateNew() => new Boxed<T>();
    }
}