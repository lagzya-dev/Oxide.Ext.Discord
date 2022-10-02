namespace Oxide.Ext.Discord.Pooling
{
    internal class ObjectPool<T> : BasePool<BasePoolable> where T : BasePoolable, new()
    {
        internal static readonly IPool<BasePoolable> Instance = new ObjectPool<T>();
        
        static ObjectPool()
        {
            DiscordPool.Pools.Add(Instance);
        }

        private ObjectPool() : base(512) { }

        protected override BasePoolable CreateNew()
        {
            T obj = new T();
            obj.OnInit(this);
            return obj;
        }

        protected override void OnGetItem(BasePoolable item)
        {
            item.LeavePoolInternal();
        }
        
        protected override bool OnFreeItem(ref BasePoolable item)
        {
            if (item.Disposed)
            {
                return false;
            }
            
            item.EnterPoolInternal();
            return true;
        }
    }
}