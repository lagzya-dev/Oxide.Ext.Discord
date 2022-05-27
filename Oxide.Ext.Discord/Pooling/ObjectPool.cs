namespace Oxide.Ext.Discord.Pooling
{
    internal class ObjectPool<T> : BasePool<T> where T : BasePoolable, new()
    {
        internal static readonly IPool<T> Instance;
        
        static ObjectPool()
        {
            Instance = new ObjectPool<T>();
        }

        private ObjectPool() : base(256) { }

        protected override void OnGetItem(T item)
        {
            item.LeavePoolInternal();
        }
        
        protected override bool OnFreeItem(ref T item)
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