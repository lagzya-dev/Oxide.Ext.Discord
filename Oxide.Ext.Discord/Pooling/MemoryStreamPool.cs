using System.IO;

namespace Oxide.Ext.Discord.Pooling
{
    /// <summary>
    /// Represents a pool for MemoryStream
    /// </summary>
    public class MemoryStreamPool : BasePool<MemoryStream>
    {
        internal MemoryStreamPool() : base(32) { }
        
        ///<inheritdoc/>
        protected override bool OnFreeItem(ref MemoryStream item)
        {
            item.Position = 0;
            item.SetLength(0);
            return true;
        }
    }
}