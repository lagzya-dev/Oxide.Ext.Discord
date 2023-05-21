using System;
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
        private readonly ConcurrentDictionary<string, IPlayer> _cache = new ConcurrentDictionary<string, IPlayer>();
        private readonly Func<string, IPlayer> _valueFactory = id => Players.FindPlayerById(id) ?? new DiscordDummyPlayer(id);

        /// <summary>
        /// Readonly Cache of <see cref="IPlayer"/>
        /// </summary>
        public readonly IReadOnlyDictionary<string, IPlayer> Cache;

        private static readonly IPlayerManager Players = Interface.Oxide.GetLibrary<Covalence>().Players;

        private ServerPlayerCache()
        {
            Cache = new ReadOnlyDictionary<string, IPlayer>(_cache);
        }
        
        /// <summary>
        /// Returns the <see cref="IPlayer"/> for the given ID
        /// </summary>
        /// <param name="id">ID of the player</param>
        /// <returns><see cref="IPlayer"/></returns>
        public IPlayer GetPlayer(string id) => _cache.GetOrAdd(id, _valueFactory);

        internal void SetPlayer(IPlayer player)
        {
            if (!_cache.TryGetValue(player.Id, out IPlayer cached) || cached.IsDummyPlayer())
            {
                _cache[player.Id] = player;
            }
        }
    }
}