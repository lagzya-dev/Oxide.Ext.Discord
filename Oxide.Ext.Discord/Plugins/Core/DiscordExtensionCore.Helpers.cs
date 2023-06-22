using Oxide.Core.Plugins;

namespace Oxide.Ext.Discord.Plugins.Core
{
    internal partial class DiscordExtensionCore
    {
        public bool IsLoaded(Plugin plugin) => plugin != null && plugin.IsLoaded;
    }
}