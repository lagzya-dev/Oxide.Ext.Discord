using Oxide.Ext.Discord.Entities.Gateway;
using Oxide.Ext.Discord.Logging;

namespace Oxide.Ext.Discord.Connections
{
    public class ClientSettings : BaseConnectSettings
    {
        public ClientSettings() { }

        public ClientSettings(string apiToken, GatewayIntents intents = GatewayIntents.None, DiscordLogLevel logLevel = DiscordLogLevel.Info) : base(apiToken, intents, logLevel) {}
    }
}