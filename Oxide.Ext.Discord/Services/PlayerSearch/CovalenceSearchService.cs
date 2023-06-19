using System;
using System.Collections.Generic;
using Oxide.Core;
using Oxide.Core.Libraries.Covalence;

namespace Oxide.Ext.Discord.Services.PlayerSearch
{
    internal class CovalenceSearchService : IPlayerSearchService
    {
        private readonly Covalence _covalence = Interface.Oxide.GetLibrary<Covalence>();

        public IEnumerable<IPlayer> GetOnlinePlayers(string name)
        {
            foreach (IPlayer player in _covalence.Players.Connected)
            {
                if (IsMatch(player, name))
                {
                    yield return player;
                }
            }
        }

        public IEnumerable<IPlayer> GetAllPlayers(string name)
        {
            foreach (IPlayer player in _covalence.Players.All)
            {
                if (IsMatch(player, name))
                {
                    yield return player;
                }
            }
        }
        
        private static bool IsMatch(IPlayer player, string name) => !string.IsNullOrWhiteSpace(name) && player.Name.IndexOf(name, StringComparison.OrdinalIgnoreCase) != -1;

        public void OnUserConnected(IPlayer player) { }

        public void OnUserDisconnected(IPlayer player) { }

        public void OnUserNameUpdated(IPlayer player, string oldName, string newName) {}
    }
}