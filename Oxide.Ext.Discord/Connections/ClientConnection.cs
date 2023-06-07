using Oxide.Ext.Discord.Entities.Gateway;
using Oxide.Ext.Discord.Logging;

namespace Oxide.Ext.Discord.Connections
{
    public class ClientConnection : BaseConnection
    {
        public ClientConnection() { }

        public ClientConnection(string apiToken, GatewayIntents intents = GatewayIntents.None, DiscordLogLevel logLevel = DiscordLogLevel.Info) : base(apiToken, intents, logLevel) {}
    }
}