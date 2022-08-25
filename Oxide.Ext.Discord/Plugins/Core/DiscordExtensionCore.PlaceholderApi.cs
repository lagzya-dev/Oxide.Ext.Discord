using System;
using System.Text;
using Oxide.Core.Libraries.Covalence;
using Oxide.Core.Plugins;
using Oxide.Plugins;

namespace Oxide.Ext.Discord.Plugins.Core
{
    internal partial class DiscordExtensionCore
    {
        [PluginReference("PlaceholderAPI")]
#pragma warning disable CS0649
        private Plugin _placeholderApi;
#pragma warning restore CS0649
        
        private Action<IPlayer, StringBuilder, bool> _replacer;
        
        public Action<IPlayer, StringBuilder, bool> GetReplacer()
        {
            if (!IsPlaceholderApiLoaded())
            {
                return _replacer;
            }
            
            return _replacer ?? (_replacer = _placeholderApi.Call("GetProcessPlaceholders", 1) as Action<IPlayer, StringBuilder, bool>);
        }

        private bool IsPlaceholderApiLoaded() => _placeholderApi != null && _placeholderApi.IsLoaded;

        private void HandlePlaceholderApiUnloaded() => _replacer = null;
    }
}