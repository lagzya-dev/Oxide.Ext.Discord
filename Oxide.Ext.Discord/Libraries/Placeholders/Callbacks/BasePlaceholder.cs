using System.Text;
using Oxide.Core.Plugins;
using Oxide.Ext.Discord.Extensions;
using Oxide.Ext.Discord.Plugins;
using Oxide.Ext.Discord.Plugins.Core;

namespace Oxide.Ext.Discord.Libraries.Placeholders.Callbacks
{
    internal abstract class BasePlaceholder
    {
        internal readonly string PluginName;
        private readonly PluginId _pluginId;
        internal readonly bool IsExtensionPlaceholder;

        protected BasePlaceholder(Plugin plugin)
        {
            _pluginId = plugin.Id();
            PluginName = plugin.FullName();
            IsExtensionPlaceholder = plugin is DiscordExtensionCore;
        }

        public abstract void Invoke(StringBuilder builder, PlaceholderState state);
        public bool IsForPlugin(Plugin plugin) => !IsExtensionPlaceholder && plugin.Id() == _pluginId;
    }
}