using Oxide.Ext.Discord.Pooling.Entities;

namespace Oxide.Ext.Discord.Pooling
{
    /// <summary>
    /// Represents a pool for <see cref="Boxed{T}"/>;
    /// </summary>
    /// <typeparam name="T">Type that will be in the boxed object</typeparam>
    internal class BoxedPool<T> : BasePool<BoxedPool<T>, Boxed<T>>
    {
        public BoxedPool() : base(512) { }

        protected override void OnGetItem(Boxed<T> item)
        {
            item.LeavePool();
        }

        protected override Boxed<T> CreateNew() => new Boxed<T>();
    }
}