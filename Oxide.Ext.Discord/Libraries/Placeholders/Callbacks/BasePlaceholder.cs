using System.Text;
using Oxide.Core.Plugins;
using Oxide.Ext.Discord.Extensions;

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

        public bool IsExtensionPlaceholder()
        {
            return Plugin == null;
        }
        
        public bool IsForPlugin(Plugin plugin)
        {
            return !IsExtensionPlaceholder() && plugin.Id() == Plugin.Id();
        }
    }
}