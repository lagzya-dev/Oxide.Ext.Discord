using System.Collections.Generic;
using System.Text;
using Oxide.Core.Libraries.Covalence;
using Oxide.Core.Plugins;
using Oxide.Ext.Discord.Builders.Interactions.AutoComplete;
using Oxide.Plugins;

namespace Oxide.Ext.Discord.Plugins.Core
{
    internal partial class DiscordExtensionCore
    {
        [PluginReference("Clans")]
#pragma warning disable CS0649
        private Plugin _clans;
#pragma warning restore CS0649

        private readonly Hash<PlayerDisplayNameMode, Hash<string, string>> _playerNameCache = new Hash<PlayerDisplayNameMode, Hash<string, string>>();
        private readonly StringBuilder _sb = new StringBuilder();

        // ReSharper disable once UnusedMember.Local
        [HookMethod(nameof(OnClanCreate))]
        private void OnClanCreate()
        {
            ClearClanCache();
        }

        // ReSharper disable once UnusedMember.Local
        [HookMethod(nameof(OnClanUpdate))]
        private void OnClanUpdate()
        {
            ClearClanCache();
        }

        // ReSharper disable once UnusedMember.Local
        [HookMethod(nameof(OnClanDestroy))]
        private void OnClanDestroy()
        {
            ClearClanCache();
        }

        // ReSharper disable once UnusedMember.Local
        [HookMethod(nameof(OnUserNameUpdated))]
        private void OnUserNameUpdated(string playerId, string oldName, string newName)
        {
            if (oldName == newName)
            {
                return;
            }
            
            foreach (KeyValuePair<PlayerDisplayNameMode, Hash<string,string>> cache in _playerNameCache)
            {
                cache.Value.Remove(playerId);
            }
        }

        private void ClearClanCache()
        {
            foreach (KeyValuePair<PlayerDisplayNameMode, Hash<string,string>> cache in _playerNameCache)
            {
                if (HasFlag(cache.Key, PlayerDisplayNameMode.IncludeClanName))
                {
                    cache.Value.Clear();
                }
            }
        }
        
        public string GetPlayerName(IPlayer player, PlayerDisplayNameMode options)
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
            if (_clans != null && _clans.IsLoaded && HasFlag(options, PlayerDisplayNameMode.IncludeClanName))
            {
                _sb.Append('[');
                _sb.Append(_clans.Call<string>("GetClanOf", player.Id));
                _sb.Append("] ");
            }

            _sb.Append(player.Name);

            if (HasFlag(options, PlayerDisplayNameMode.IncludePlayerId))
            {
                _sb.Append(" (");
                _sb.Append(player.Id);
                _sb.Append(')');
            }

            name = _sb.ToString();
            cache[player.Id] = name;
            return name;
        }
        
        private bool HasFlag(PlayerDisplayNameMode options, PlayerDisplayNameMode flag)
        {
            return (options & flag) == flag;
        }
    }
}