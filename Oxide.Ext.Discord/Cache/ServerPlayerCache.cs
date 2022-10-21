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
        private readonly ConcurrentDictionary<string, IPlayer> InternalCache = new ConcurrentDictionary<string, IPlayer>();

        /// <summary>
        /// Readonly Cache of <see cref="IPlayer"/>
        /// </summary>
        public readonly IReadOnlyDictionary<string, IPlayer> Cache;

        private readonly IPlayerManager Players = Interface.Oxide.GetLibrary<Covalence>().Players;

        private ServerPlayerCache()
        {
            Cache = new ReadOnlyDictionary<string, IPlayer>(InternalCache);
        }
        
        /// <summary>
        /// Returns the <see cref="IPlayer"/> for the given ID
        /// </summary>
        /// <param name="id">ID of the player</param>
        /// <returns><see cref="IPlayer"/></returns>
        public IPlayer GetPlayer(string id)
        {
            if (InternalCache.ContainsKey(id))
            {
                return InternalCache[id];
            }

            IPlayer player = Players.FindPlayerById(id);
            if (player == null)
            {
                player = new DiscordDummyPlayer(id);
            }

            InternalCache[id] = player;
            return player;
        }

        internal void SetPlayer(IPlayer player)
        {
            if (!InternalCache.TryGetValue(player.Id, out IPlayer cached) || cached.IsDummyPlayer())
            {
                InternalCache[player.Id] = player;
            }
        }
    }
}