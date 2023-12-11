using Oxide.Ext.Discord.Libraries.Placeholders;

namespace Oxide.Ext.Discord.Types.Pooling.Pools
{
    internal class PlaceholderDataPool : BasePool<BasePoolable, PlaceholderDataPool>
    {
        protected override PoolSize GetPoolSize(PoolSettings settings) => settings.PlaceholderDataPoolSize;
        
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