namespace Oxide.Ext.Discord.Pooling
{
    public class PoolSettings
    {
        public int HashPoolSize { get; set; } = 64;
        public int HashSetPoolSize { get; set; } = 64;
        public int ListPoolSize { get; set; } = 64;
        public int MemoryStreamPoolSize { get; set; } = 64;
        public int ObjectPoolSize { get; set; } = 64;
        public int PlaceholderDataPoolSize { get; set; } = 64;
        public int StringBuilderPoolSize { get; set; } = 64;
    }
}