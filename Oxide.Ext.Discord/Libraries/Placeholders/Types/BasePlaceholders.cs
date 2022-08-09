using System.Collections.Generic;
using System.Text;
using Oxide.Core.Plugins;

namespace Oxide.Ext.Discord.Libraries.Placeholders.Types
{
    public abstract class BasePlaceholders
    {
        private readonly Plugin _plugin;

        protected BasePlaceholders(Plugin plugin)
        {
            _plugin = plugin;
        }
        
        protected abstract string GetDataKey();
        public abstract void Invoke(StringBuilder builder, PlaceholderMatch match, PlaceholderData data);

        public abstract IEnumerable<string> GetPlaceholders();

        public bool IsForPlugin(Plugin plugin)
        {
            if (_plugin == null)
            {
                return false;
            }

            return plugin.Name == _plugin.Name;
        }
    }
}