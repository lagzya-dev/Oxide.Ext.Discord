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
#pragma warning disable CS0649
        // ReSharper disable once InconsistentNaming
        private Plugin PlaceholderAPI;
#pragma warning restore CS0649
        
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