using Oxide.Ext.Discord.Exceptions.Pooling;

namespace Oxide.Ext.Discord.Pooling
{
    public struct PoolSize
    {
        public readonly int StartingSize;
        public readonly int MaxSize;
        public bool IsValid => StartingSize > 0 && MaxSize >= StartingSize;
        
        public PoolSize(int startingSize, int maxSize)
        {
            InvalidPoolException.ThrowIfNotPowerOf2(startingSize, nameof(startingSize));
            InvalidPoolException.ThrowIfNotPowerOf2(maxSize, nameof(maxSize));
            StartingSize = startingSize;
            MaxSize = maxSize;
        }

        public bool CanResize(int currentSize) => GetNextSize(currentSize) <= MaxSize;

        public int GetNextSize(int currentSize) => currentSize * 2;
    }
}