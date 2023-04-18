using System.Text;

namespace Oxide.Ext.Discord.Pooling.Pools
{
    /// <summary>
    /// Pool for StringBuilders
    /// </summary>
    internal class StringBuilderPool : BasePool<StringBuilder>
    {
        public static StringBuilderPool ForPlugin(DiscordPluginPool pluginPool) => ForPlugin<StringBuilderPool>(pluginPool);
        
        protected override int GetPoolSize(PoolSettings settings) => settings.StringBuilderPoolSize;
        
        protected override StringBuilder CreateNew() => new StringBuilder();

        ///<inheritdoc/>
        protected override bool OnFreeItem(ref StringBuilder item)
        {
            item.Clear();
            return true;
        }
    }
}