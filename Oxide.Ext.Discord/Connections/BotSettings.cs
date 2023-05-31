using System;
using System.Text;
using System.Text.RegularExpressions;
using Oxide.Ext.Discord.Entities;
using Oxide.Ext.Discord.Logging;

namespace Oxide.Ext.Discord.Connections
{
    public class BotSettings : BaseConnectSettings
    {
        private static readonly Regex TokenValidator = new Regex(@"^[\w-]+\.[\w-]+\.[\w-]+$", RegexOptions.Compiled);
        
        public readonly string HiddenToken;
        public readonly Snowflake ApplicationId;
        public readonly DateTimeOffset CreationDate;

        private static readonly char[] SplitArgs = {'.'};

        public BotSettings(DiscordClient client) : base(client.Settings.ApiToken, client.Settings.Intents, client.Settings.LogLevel)
        {
            HiddenToken = GenerateHiddenToken();
            if (!TokenValidator.IsMatch(ApiToken))
            {
                client.Logger.Warning("API Token does not appear to be a valid discord bot token: {0} for plugin {1}. " +
                                      "Please confirm you are using the correct bot token. " +
                                      "If the token is correct and this message is showing please let the Discord Extension Developers know.", HiddenToken, client.PluginName);
            }

            if (!ParseToken(out string base64AppId, out string base64CreationDate))
            {
                client.Logger.Error("Failed to parse token {0} for plugin {1}", HiddenToken, client.PluginName);
                return;
            }
            
            TryParseApplicationId(client, base64AppId, out ApplicationId);
            TryParseCreationDate(client, base64CreationDate, out CreationDate);
        }

        private bool ParseToken(out string base64AppId, out string base64CreationDate)
        {
            string[] args = ApiToken.Split(SplitArgs);
            if (args.Length != 3)
            {
                base64AppId = null;
                base64CreationDate = null;
                return false;
            }

            base64AppId = args[0];
            base64CreationDate = args[1];
            return true;
        }

        private bool TryParseApplicationId(DiscordClient client, string base64AppId, out Snowflake id)
        {
            string appId = null;
            try
            {
                appId = Encoding.UTF8.GetString(Convert.FromBase64String(base64AppId));
                id = new Snowflake(appId);
                return true;
            }
            catch (Exception ex)
            {
                client.Logger.Exception("Failed to parse application ID from bot token. Bot token is invalid. Token: {0}. App ID: {1}", HiddenToken, appId, ex);
                id = default(Snowflake);
                return false;
            }
        }
        
        private bool TryParseCreationDate(DiscordClient client, string base64CreationDate, out DateTimeOffset createdDate)
        {
            try
            {
                
                long timestamp = Convert.ToInt64(Convert.FromBase64String(base64CreationDate));
                createdDate = DateTimeOffset.FromUnixTimeSeconds(timestamp);
                return true;
            }
            catch (Exception ex)
            {
                client.Logger.Exception($"Failed to parse Token Creation Date from bot token. Bot token is invalid. Token: {HiddenToken}", ex);
                createdDate = default(DateTimeOffset);
                return false;
            }
        }
    }
}