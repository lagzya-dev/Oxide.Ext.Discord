using System;
using System.Text;
using Oxide.Core.Libraries.Covalence;
using Oxide.Core.Plugins;
using Oxide.Plugins;

namespace Oxide.Ext.Discord.Plugins.Core
{
    internal partial class DiscordExtensionCore
    {
        [PluginReference]
        private Plugin PlaceholderAPI;
        
        private Action<IPlayer, StringBuilder, bool> _replacer;
        
        public Action<IPlayer, StringBuilder, bool> GetReplacer()
        {
            if (!IsPlaceholderApiLoaded())
            {
                return _replacer;
            }
            
            return _replacer ?? (_replacer = PlaceholderAPI.Call("GetProcessPlaceholders", 1) as Action<IPlayer, StringBuilder, bool>);
        }

        private bool IsPlaceholderApiLoaded() => PlaceholderAPI != null && PlaceholderAPI.IsLoaded;

        private void HandlePlaceholderApiUnloaded() => _replacer = null;
    }
}