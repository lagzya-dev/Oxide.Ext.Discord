using Oxide.Ext.Discord.Libraries.Placeholders;

namespace Oxide.Ext.Discord.Pooling
{
    public class PlaceholderDataPool : BasePool<PlaceholderData>
    {
        internal static readonly IPool<PlaceholderData> Instance;
        
        static PlaceholderDataPool()
        {
            Instance = new PlaceholderDataPool();
        }
        
        public PlaceholderDataPool() : base(64) { }

        protected override PlaceholderData CreateNew()
        {
            return new PlaceholderData();
        }
    }
}