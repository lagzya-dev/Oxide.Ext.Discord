using Oxide.Ext.Discord.Libraries.Placeholders;

namespace Oxide.Ext.Discord.Pooling
{
    internal class PlaceholderDataPool : BasePool<PlaceholderData>
    {
        internal static readonly IPool<PlaceholderData> Instance = new PlaceholderDataPool();
        
        static PlaceholderDataPool()
        {
            DiscordPool.Pools.Add(Instance);
        }

        private PlaceholderDataPool() : base(1024) { }

        protected override bool OnFreeItem(ref PlaceholderData item)
        {
            item.EnterPool();
            return base.OnFreeItem(ref item);
        }

        protected override PlaceholderData CreateNew()
        {
            return new PlaceholderData();
        }
    }
}