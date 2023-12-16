using System;
using System.Net;
using System.Text;
using Oxide.Core.Libraries.Covalence;
using Oxide.Core.Plugins;
using Oxide.Ext.Discord.Libraries.Pooling;
using Oxide.Plugins;

namespace Oxide.Ext.Discord.Plugins.Core
{
    internal partial class DiscordExtensionCore
    {
        private Action<IPlayer, StringBuilder, bool> _replacer;
        
        public Action<IPlayer, StringBuilder, bool> GetReplacer() => _replacer;

        private readonly Hash<string, string> _flagCache = new Hash<string, string>();

        private void HandlePlaceholderApi(Plugin plugin)
        {
            _replacer = plugin?.Call("GetProcessPlaceholders", 1) as Action<IPlayer, StringBuilder, bool>;
        }

        public string GetCountry(IPlayer player)
        {
            const string Placeholder = "{player.address.data!country.code:lower}";

            if (_replacer == null)
            {
                return null;
            }
            
            StringBuilder sb = DiscordPool.Internal.GetStringBuilder(Placeholder);
            _replacer.Invoke(player, sb, true);
            return DiscordPool.Internal.ToStringAndFree(sb);
        }

        public string GetCountryEmoji(IPlayer player)
        {
            string country = GetCountry(player);
            if (_flagCache.TryGetValue(country, out string flag))
            {
                return flag;
            }

            if (string.IsNullOrEmpty(country) || IPAddress.TryParse(country, out IPAddress _))
            {
                flag = ":signal_strength:";
            }
            else
            {
                flag =  $":flag_{country}:";
            }

            _flagCache[country] = flag;
            return flag;
        }
    }
}