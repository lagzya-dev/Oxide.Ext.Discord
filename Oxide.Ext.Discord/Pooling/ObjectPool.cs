
namespace Oxide.Ext.Discord.Pooling
{
    internal class ObjectPool<T> : BasePool<ObjectPool<T>, BasePoolable> where T : BasePoolable, new()
    {
        public ObjectPool() : base(512) { }

        protected override BasePoolable CreateNew()
        {
            T obj = new T();
            obj.OnInit(PluginPool, this);
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