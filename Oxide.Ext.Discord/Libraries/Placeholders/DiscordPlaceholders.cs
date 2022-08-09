using System.Text;
using System.Text.RegularExpressions;
using Oxide.Core;
using Oxide.Core.Libraries;
using Oxide.Core.Libraries.Covalence;
using Oxide.Core.Plugins;
using Oxide.Ext.Discord.Extensions;
using Oxide.Ext.Discord.Libraries.Placeholders.Types;
using Oxide.Ext.Discord.Logging;
using Oxide.Ext.Discord.Plugins.Core;
using Oxide.Ext.Discord.Pooling;
using Oxide.Plugins;

namespace Oxide.Ext.Discord.Libraries.Placeholders
{
    public class DiscordPlaceholders : Library
    {
        private readonly Regex _placeholderRegex = new Regex(@"{([^!:{}""]+)(?::([^!{}""]+))*?}", RegexOptions.Compiled);
        private readonly Hash<string, BasePlaceholders> _placeholders = new Hash<string, BasePlaceholders>();
        private readonly Covalence _covalence = Interface.Oxide.GetLibrary<Covalence>();
        private readonly ILogger _logger;
        
        public DiscordPlaceholders(ILogger logger)
        {
            _logger = logger;
            RegisterPlaceholders(new ChannelPlaceholders());
            RegisterPlaceholders(new GuildPlaceholders());
            RegisterPlaceholders(new ServerPlaceholders());
            RegisterPlaceholders(new MemberPlaceholders());
            RegisterPlaceholders(new RolePlaceholders());
            RegisterPlaceholders(new TimestampPlaceholders());
            RegisterPlaceholders(new UserPlaceholders());
            RegisterPlaceholders(new ApplicationCommandPlaceholders());
        }
        
        public string ProcessPlaceholders(string text, PlaceholderData data)
        {
            if (string.IsNullOrEmpty(text))
            {
                return text;
            }
            
            MatchCollection matches = _placeholderRegex.Matches(text);
            if (matches.Count != 0)
            {
                StringBuilder builder = DiscordPool.GetStringBuilder();
                builder.Append(text);
                bool nonMatchedPlaceholder = false;
                for (int index = matches.Count - 1; index >= 0; index--)
                {
                    Match match = matches[index];
                    PlaceholderMatch placeholderMatch = new PlaceholderMatch(match);
                    BasePlaceholders placeholder = _placeholders[placeholderMatch.Name];
                    if (placeholder == null)
                    {
                        nonMatchedPlaceholder = true;
                        continue;
                    }

                    placeholder.Invoke(builder, placeholderMatch, data);
                }

                if (nonMatchedPlaceholder)
                {
                    DiscordExtensionCore.Instance.GetReplacer()?.Invoke(data.Get<IPlayer>(nameof(IPlayer)), builder, true);
                }

                text = builder.ToString();
                DiscordPool.FreeStringBuilder(ref builder);
            }

            if (data.ShouldPool)
            {
                DiscordPool.FreePlaceholderData(ref data);
            }
            
            return text;
        }

        public PlaceholderData CreateData()
        {
            PlaceholderData data = DiscordPool.GetPlaceholderData();
            data.AddServer(_covalence.Server);
            return data;
        }

        public void RegisterPlaceholders(BasePlaceholders placeholders)
        {
            foreach (string key in placeholders.GetPlaceholders())
            {
                _placeholders[key] = placeholders;
            }
        }

        internal void OnPluginUnloaded(Plugin plugin)
        {
            _placeholders.RemoveAll(p => p.IsForPlugin(plugin));
        }
    }
}