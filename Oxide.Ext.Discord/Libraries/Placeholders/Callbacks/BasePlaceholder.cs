using System.Text;
using Oxide.Core.Plugins;
using Oxide.Ext.Discord.Extensions;
using Oxide.Ext.Discord.Plugins.Core;

namespace Oxide.Ext.Discord.Libraries.Placeholders.Callbacks
{
    internal abstract class BasePlaceholder
    {
        internal readonly Plugin Plugin;

        protected BasePlaceholder(Plugin plugin)
        {
            Plugin = plugin;
        }

        public abstract void Invoke(StringBuilder builder, PlaceholderState state);

        public bool IsExtensionPlaceholder() => Plugin == DiscordExtensionCore.Instance;
        public bool IsForPlugin(Plugin plugin) => !IsExtensionPlaceholder() && plugin.Id() == Plugin.Id();
    }
}