using System;
using Oxide.Core.Plugins;
using Oxide.Plugins;

namespace Oxide.Ext.Discord.Extensions
{
    internal static class PluginExt
    {
        private static readonly Hash<string, string> FullNameCache = new Hash<string, string>();
        
        internal static string Id(this Plugin plugin)
        {
            if (plugin == null) throw new ArgumentNullException(nameof(plugin));
            return plugin.Name;
        }
        
        internal static string FullName(this Plugin plugin)
        {
            if (plugin == null) throw new ArgumentNullException(nameof(plugin));
            string name = FullNameCache[plugin.Name];
            if (name == null)
            {
                name = $"{plugin.Name} by {plugin.Author} v{plugin.Version}";
                FullNameCache[plugin.Name] = name;
            }

            return name;
        }

        internal static void OnPluginUnloaded(Plugin plugin)
        {
            FullNameCache.Remove(plugin.Name);
        }
    }
}