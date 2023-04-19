using Oxide.Ext.Discord.Pooling.Pools;

namespace Oxide.Ext.Discord.Pooling
{
    /// <summary>
    /// Settings for the pools
    /// </summary>
    public class PoolSettings
    {
        /// <summary>
        /// Size of the <see cref="HashPool{TKey,TValue}"/>
        /// </summary>
        public int HashPoolSize { get; set; } = 64;
        
        /// <summary>
        /// Size of the <see cref="HashSetPool{T}"/>
        /// </summary>
        public int HashSetPoolSize { get; set; } = 64;
        
        /// <summary>
        /// Size of the <see cref="ListPool{T}"/>
        /// </summary>
        public int ListPoolSize { get; set; } = 64;
        
        /// <summary>
        /// Size of the <see cref="MemoryStreamPool"/>
        /// </summary>
        public int MemoryStreamPoolSize { get; set; } = 64;
        
        /// <summary>
        /// Size of the <see cref="ObjectPool{T}"/>
        /// </summary>
        public int ObjectPoolSize { get; set; } = 64;
        
        /// <summary>
        /// Size of the <see cref="PlaceholderDataPool"/>
        /// </summary>
        public int PlaceholderDataPoolSize { get; set; } = 64;
        
        /// <summary>
        /// Size of the <see cref="StringBuilderPool"/>
        /// </summary>
        public int StringBuilderPoolSize { get; set; } = 64;
    }
}