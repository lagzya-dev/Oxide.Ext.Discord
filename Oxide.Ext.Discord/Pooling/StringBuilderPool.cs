using System.Text;
namespace Oxide.Ext.Discord.Pooling
{
    /// <summary>
    /// Pool for StringBuilders
    /// </summary>
    internal class StringBuilderPool : BasePool<StringBuilder>
    {
        internal static readonly IPool<StringBuilder> Instance;
        
        static StringBuilderPool()
        {
            Instance = new StringBuilderPool();
            DiscordPool.Pools.Add(Instance);
        }

        private StringBuilderPool() : base(512) { }

        protected override StringBuilder CreateNew() => new StringBuilder();

        ///<inheritdoc/>
        protected override bool OnFreeItem(ref StringBuilder item)
        {
            item.Clear();
            return true;
        }
    }
}