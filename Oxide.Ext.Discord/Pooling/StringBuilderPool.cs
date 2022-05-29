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
        }

        private StringBuilderPool() : base(128) { }
        
        ///<inheritdoc/>
        protected override bool OnFreeItem(ref StringBuilder item)
        {
            item.Length = 0;
            return true;
        }
    }
}