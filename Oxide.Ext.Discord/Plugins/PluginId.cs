using System;
using Oxide.Core.Plugins;
using Oxide.Ext.Discord.Extensions;

namespace Oxide.Ext.Discord.Plugins
{
    public struct PluginId : IEquatable<PluginId>
    {
        public readonly int Id;
        public bool IsValid => Id != 0;

        public PluginId(Plugin plugin)
        {
            if (plugin == null) throw new ArgumentNullException(nameof(plugin));
            Id = plugin.Name.GetHashCode();
        }
        
        public PluginId(string id)
        {
            Id = id.GetHashCode();
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
    }
}