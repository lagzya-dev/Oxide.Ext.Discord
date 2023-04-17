using Oxide.Ext.Discord.Libraries.Placeholders;

namespace Oxide.Ext.Discord.Pooling
{
    internal class PlaceholderDataPool : BasePool<BasePoolable>
    {
        public PlaceholderDataPool() : base(512) { }

        protected override BasePoolable CreateNew()
        {
            PlaceholderData data = new PlaceholderData();
            data.OnInit(PluginPool, this);
            return data;
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