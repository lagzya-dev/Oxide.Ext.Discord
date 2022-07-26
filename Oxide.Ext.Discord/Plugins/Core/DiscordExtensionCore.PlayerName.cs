using System.Collections.Generic;
using System.Text;
using Oxide.Core.Libraries.Covalence;
using Oxide.Core.Plugins;
using Oxide.Ext.Discord.Builders.Messages;
using Oxide.Plugins;

namespace Oxide.Ext.Discord.Plugins.Core
{
    internal partial class DiscordExtensionCore
    {
        [PluginReference("Clans")]
#pragma warning disable CS0649
        private Plugin _clans;
#pragma warning restore CS0649

        private readonly Hash<AutoCompletePlayerSearchOptions, Hash<string, string>> _playerNameCache = new Hash<AutoCompletePlayerSearchOptions, Hash<string, string>>();
        private readonly StringBuilder _sb = new StringBuilder();

        [HookMethod(nameof(OnClanCreate))]
        private void OnClanCreate(string tag)
        {
            ClearClanCache();
        }

        [HookMethod(nameof(OnClanUpdate))]
        private void OnClanUpdate(string tag)
        {
            ClearClanCache();
        }

        [HookMethod(nameof(OnClanDestroy))]
        private void OnClanDestroy(string tag)
        {
            ClearClanCache();
        }

        [HookMethod(nameof(OnUserNameUpdated))]
        private void OnUserNameUpdated(string playerId, string oldName, string newName)
        {
            if (oldName == newName)
            {
                return;
            }
            
            foreach (KeyValuePair<AutoCompletePlayerSearchOptions, Hash<string,string>> cache in _playerNameCache)
            {
                cache.Value.Remove(playerId);
            }
        }

        private void ClearClanCache()
        {
            foreach (KeyValuePair<AutoCompletePlayerSearchOptions, Hash<string,string>> cache in _playerNameCache)
            {
                if (HasFlag(cache.Key, AutoCompletePlayerSearchOptions.IncludeClanName))
                {
                    cache.Value.Clear();
                }
            }
        }
        
        public string GetPlayerName(IPlayer player, AutoCompletePlayerSearchOptions options)
        {
            Hash<string, string> cache = _playerNameCache[options];
            if (cache == null)
            {
                cache = new Hash<string, string>();
                _playerNameCache[options] = cache;
            }
            
            string name = cache[player.Id];
            if (!string.IsNullOrEmpty(name))
            {
                return name;
            }

            _sb.Clear();
            if (_clans != null && _clans.IsLoaded && HasFlag(options, AutoCompletePlayerSearchOptions.IncludeClanName))
            {
                _sb.Append('[');
                _sb.Append(_clans.Call<string>("GetClanOf", player.Id));
                _sb.Append("] ");
            }

            _sb.Append(player.Name);

            if (HasFlag(options, AutoCompletePlayerSearchOptions.IncludeSteamId))
            {
                _sb.Append(" (");
                _sb.Append(player.Id);
                _sb.Append(')');
            }

            name = _sb.ToString();
            cache[player.Id] = name;
            return name;
        }
        
        private bool HasFlag(AutoCompletePlayerSearchOptions options, AutoCompletePlayerSearchOptions flag)
        {
            return (options & flag) == flag;
        }
    }
}