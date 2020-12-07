using Oxide.Ext.Discord.DiscordObjects;

namespace Oxide.Ext.Discord
{
    public class DiscordSettings
    {
        public string ApiToken;

        public bool Debugging = false;

        public BotIntents Intents = BotIntents.All;
    }
}