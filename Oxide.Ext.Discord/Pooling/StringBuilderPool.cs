using System.Text;
namespace Oxide.Ext.Discord.Pooling
{
    /// <summary>
    /// Pool for StringBuilders
    /// </summary>
    internal class StringBuilderPool : BasePool<StringBuilder>
    {
        internal static readonly IPool<StringBuilder> Instance = new StringBuilderPool();
        
        static StringBuilderPool()
        {
            DiscordPool.Pools.Add(Instance);
        }

        private StringBuilderPool() : base(256) { }

        protected override StringBuilder CreateNew() => new StringBuilder();

        ///<inheritdoc/>
        protected override bool OnFreeItem(ref StringBuilder item)
        {
            item.Clear();
            return true;
        }
    }
}