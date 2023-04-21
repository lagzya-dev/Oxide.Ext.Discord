using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Oxide.Core;
using Oxide.Core.Libraries.Covalence;
using Oxide.Ext.Discord.Extensions;
using Oxide.Ext.Discord.Plugins;
using Oxide.Ext.Discord.Singleton;

namespace Oxide.Ext.Discord.Cache
{
    /// <summary>
    /// Cache for server <see cref="IPlayer"/>
    /// </summary>
    public class ServerPlayerCache : Singleton<ServerPlayerCache>
    {
        private readonly ConcurrentDictionary<string, IPlayer> _internalCache = new ConcurrentDictionary<string, IPlayer>();

        /// <summary>
        /// Readonly Cache of <see cref="IPlayer"/>
        /// </summary>
        public readonly IReadOnlyDictionary<string, IPlayer> Cache;

        private readonly IPlayerManager _players = Interface.Oxide.GetLibrary<Covalence>().Players;

        private ServerPlayerCache()
        {
            Cache = new ReadOnlyDictionary<string, IPlayer>(_internalCache);
        }
        
        /// <summary>
        /// Returns the <see cref="IPlayer"/> for the given ID
        /// </summary>
        /// <param name="id">ID of the player</param>
        /// <returns><see cref="IPlayer"/></returns>
        public IPlayer GetPlayer(string id)
        {
            if (_internalCache.TryGetValue(id, out IPlayer player))
            {
                return player;
            }

            player = _players.FindPlayerById(id);
            if (player == null)
            {
                player = new DiscordDummyPlayer(id);
            }

            _internalCache[id] = player;
            return player;
        }

        internal void SetPlayer(IPlayer player)
        {
            if (!_internalCache.TryGetValue(player.Id, out IPlayer cached) || cached.IsDummyPlayer())
            {
                _internalCache[player.Id] = player;
            }
        }
    }
}