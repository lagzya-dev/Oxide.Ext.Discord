using Oxide.Core.Plugins;

namespace Oxide.Ext.Discord.Plugins.Core
{
    internal partial class DiscordExtensionCore
    {
        private static bool IsPluginLoaded(Plugin plugin) => plugin != null && plugin.IsLoaded;
    }
}