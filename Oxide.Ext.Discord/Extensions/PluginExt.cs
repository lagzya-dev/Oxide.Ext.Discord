using System;
using System.Runtime.CompilerServices;
using Oxide.Core.Plugins;
using Oxide.Ext.Discord.Libraries.Pooling;
using Oxide.Ext.Discord.Pooling;
using Oxide.Plugins;

namespace Oxide.Ext.Discord.Extensions
{
    internal static class PluginExt
    {
        private static readonly Hash<string, string> FullNameCache = new Hash<string, string>();
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static string Id(this Plugin plugin)
        {
            return plugin?.Name ?? throw new ArgumentNullException(nameof(plugin));
        }
        
        internal static string FullName(this Plugin plugin)
        {
            if (plugin == null) throw new ArgumentNullException(nameof(plugin));
            string name = FullNameCache[plugin.Name];
            if (name == null)
            {
                name = CreatePluginFullName(plugin);
                FullNameCache[plugin.Name] = name;
            }

            return name;
        }

        internal static string GetFullName(string pluginName)
        {
            if (string.IsNullOrEmpty(pluginName)) throw new ArgumentNullException(nameof(pluginName));
            string name = FullNameCache[pluginName];
            return name ?? pluginName;
        }

        internal static void OnPluginLoaded(Plugin plugin)
        {
            FullNameCache[plugin.Name] = CreatePluginFullName(plugin);
        }

        public static DiscordPluginPool GetPool(this Plugin plugin) => DiscordPool.Instance.GetOrCreate(plugin);

        internal static void OnPluginUnloaded(Plugin plugin)
        {
            FullNameCache.Remove(plugin.Name);
        }

        private static string CreatePluginFullName(Plugin plugin)
        {
            return $"{plugin.Name} by {plugin.Author} v{plugin.Version}";
        }
    }
}