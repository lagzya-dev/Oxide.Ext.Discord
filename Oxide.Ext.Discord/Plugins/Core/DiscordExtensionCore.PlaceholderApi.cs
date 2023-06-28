using System;
using System.Text;
using Oxide.Core.Libraries.Covalence;
using Oxide.Core.Plugins;

namespace Oxide.Ext.Discord.Plugins.Core
{
    internal partial class DiscordExtensionCore
    {
        private Plugin _placeholderApi;

        private Action<IPlayer, StringBuilder, bool> _replacer;
        
        public Action<IPlayer, StringBuilder, bool> GetReplacer() => _replacer;

        private void HandlePlaceholderApi(Plugin plugin)
        {
            _placeholderApi = plugin;
            _replacer = plugin?.Call("GetProcessPlaceholders", 1) as Action<IPlayer, StringBuilder, bool>;
        }
    }
}