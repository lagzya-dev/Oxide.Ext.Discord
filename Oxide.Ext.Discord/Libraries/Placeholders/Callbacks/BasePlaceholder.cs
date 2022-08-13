using System.Text;
using Oxide.Core.Plugins;
using Oxide.Ext.Discord.Extensions;

namespace Oxide.Ext.Discord.Libraries.Placeholders.Callbacks
{
    public abstract class BasePlaceholder
    {
        internal readonly Plugin Plugin;

        protected BasePlaceholder(Plugin plugin)
        {
            Plugin = plugin;
        }

        public abstract void Invoke(StringBuilder builder, PlaceholderMatch match, PlaceholderData data);

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