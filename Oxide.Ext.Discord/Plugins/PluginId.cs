using System;
using Oxide.Core.Plugins;
using Oxide.Ext.Discord.Extensions;
using Oxide.Ext.Discord.Interfaces.Logging;
using Oxide.Ext.Discord.Logging;

namespace Oxide.Ext.Discord.Plugins
{
    public struct PluginId : IEquatable<PluginId>, IDebugLoggable
    {
        public readonly int Id;
        public bool IsValid => Id != 0;

        internal PluginId(Plugin plugin)
        {
            Id = plugin?.Name.GetHashCode() ?? throw new ArgumentNullException(nameof(plugin));
        }
        
        internal PluginId(string id)
        {
            Id = id?.GetHashCode() ?? throw new ArgumentNullException(nameof(id));
        }

        public bool Equals(PluginId other) => Id == other.Id;

        public override bool Equals(object obj) => obj is PluginId other && Equals(other);

        public override int GetHashCode() => Id;

        public static bool operator == (PluginId left, PluginId right) => left.Equals(right);
        public static bool operator !=(PluginId left, PluginId right) => !(left == right);

        public override string ToString()
        {
            return this.PluginName();
        }
        
        public void LogDebug(DebugLogger logger)
        {
            logger.AppendField("ID", Id);
            logger.AppendField("Name", this.PluginName());
        }
    }
}