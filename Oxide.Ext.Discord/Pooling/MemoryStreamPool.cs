using System.IO;

namespace Oxide.Ext.Discord.Pooling
{
    /// <summary>
    /// Represents a pool for MemoryStream
    /// </summary>
    internal class MemoryStreamPool : BasePool<MemoryStream>
    {
        public MemoryStreamPool() : base(256) { }

        protected override MemoryStream CreateNew() => new MemoryStream();

        ///<inheritdoc/>
        protected override bool OnFreeItem(ref MemoryStream item)
        {
            item.SetLength(0);
            return true;
        }
    }
}