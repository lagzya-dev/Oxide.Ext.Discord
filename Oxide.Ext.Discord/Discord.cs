﻿using System.Collections.Generic;
using System.Linq;
using Oxide.Core;
using Oxide.Ext.Discord.WebSockets;

namespace Oxide.Ext.Discord
{
    public class Discord
    {
        public static List<DiscordClient> Clients { get; private set; } = new List<DiscordClient>();

        public static DiscordClient GetClient(string apiKey)
        {
            var search = Clients.Where(x => x.Settings.ApiToken == apiKey);
            if (search.Count() > 1)
            {
                Interface.Oxide.LogWarning("[Discord Ext] Multiple DiscordClient's found for one APIKey, destroying...");
                search.ToList().ForEach(x =>
                {
                    x.Disconnect();
                    Clients.Remove(x);
                });
            }

            if (search.Count() == 1)
            {
                var client = search.First();
                if (!client.IsAlive())
                {
                    client.Connect();
                }

                return client;
            }

            var newClient = new DiscordClient();
            Clients.Add(newClient);
            newClient.Initialize(apiKey);
            return newClient;
        }

        public static void CloseClient(DiscordClient client)
        {
            Interface.Oxide.LogInfo("CloseClient has been called");
            if (client == null)
            {
                Interface.Oxide.LogInfo("client is null");
                return;
            }
            if (client.IsClosed())
            {
                Interface.Oxide.LogInfo("client is closed");
                return;
            }
            client.Disconnect();
            Interface.Oxide.LogInfo("called client.Disconnect");
            Clients.Remove(client);
        }
    }
}