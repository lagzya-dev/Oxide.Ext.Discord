using Oxide.Core.Plugins;

namespace Oxide.Ext.Discord.Pooling
{
    /// <summary>
    /// Represents a pool
    /// </summary>
    public interface IPool
    {
        void OnPluginUnloaded(Plugin plugin);

        void Clear();

        void Wipe();
    }
}