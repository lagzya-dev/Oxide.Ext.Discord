using System.IO;

namespace Oxide.Ext.Discord.Pooling
{
    /// <summary>
    /// Represents a pool for MemoryStream
    /// </summary>
    internal class MemoryStreamPool : BasePool<MemoryStream>
    {
        internal static readonly IPool<MemoryStream> Instance;
        
        static MemoryStreamPool()
        {
            Instance = new MemoryStreamPool();
        }

        private MemoryStreamPool() : base(128) { }

        protected override MemoryStream CreateNew() => new MemoryStream();

        ///<inheritdoc/>
        protected override bool OnFreeItem(ref MemoryStream item)
        {
            item.Position = 0;
            item.SetLength(0);
            return true;
        }
    }
}