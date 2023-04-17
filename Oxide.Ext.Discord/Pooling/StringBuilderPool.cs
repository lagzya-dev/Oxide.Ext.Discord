using System.Text;
namespace Oxide.Ext.Discord.Pooling
{
    /// <summary>
    /// Pool for StringBuilders
    /// </summary>
    internal class StringBuilderPool : BasePool<StringBuilder>
    {
        public StringBuilderPool() : base(256) { }

        protected override StringBuilder CreateNew() => new StringBuilder();

        ///<inheritdoc/>
        protected override bool OnFreeItem(ref StringBuilder item)
        {
            item.Clear();
            return true;
        }
    }
}