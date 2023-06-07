using System;
using System.Text;
using System.Text.RegularExpressions;
using Oxide.Ext.Discord.Connections;
using Oxide.Ext.Discord.Entities;
using Oxide.Ext.Discord.Libraries.Pooling;
using Oxide.Ext.Discord.Logging;
using Oxide.Ext.Discord.Singleton;
using Oxide.Plugins;

namespace Oxide.Ext.Discord.Factory
{
    internal class BotTokenFactory : Singleton<BotTokenFactory>
    {
        private readonly Regex _tokenValidator = new Regex(@"^[\w-]+\.[\w-]+\.[\w-]+$", RegexOptions.Compiled);
        private readonly char[] _splitArgs = {'.'};

        private readonly Hash<string, BotToken> _tokens = new Hash<string, BotToken>();
        
        private BotTokenFactory() { }

        internal BotToken CreateFromClient(DiscordClient client)
        {
            string token = client.Connection.ApiToken;
            
            BotToken botToken = _tokens[token];
            if (botToken == null)
            {
                botToken = ParseToken(token, client.PluginName);
                _tokens[token] = botToken;
            }
            
            if (!_tokenValidator.IsMatch(token))
            {
                DiscordExtension.GlobalLogger.Warning("API Token does not appear to be a valid discord bot token: {0} for plugin {1}. " +
                                                      "Please confirm you are using the correct bot token. " +
                                                      "If the token is correct and this message is showing please let the Discord Extension Developers know.", botToken.HiddenToken, client.PluginName);
            }

            return botToken;
        }
        
        private string GenerateHiddenToken(string token)
        {
            StringBuilder sb = DiscordPool.Internal.GetStringBuilder();

            int last = token.LastIndexOf('.') + 1;
            sb.Append(token.Substring(0, last));
            sb.Append('#', token.Length - last);

            return DiscordPool.Internal.FreeStringBuilderToString(sb);
        }
        
        private BotToken ParseToken(string token, string pluginName)
        {
            string hiddenToken = GenerateHiddenToken(token);
            string[] args = token.Split(_splitArgs);
            if (args.Length != 3)
            {
                DiscordExtension.GlobalLogger.Error("Failed to parse token {0} for plugin {1}", hiddenToken, pluginName);
                return new BotToken(token, hiddenToken, default(Snowflake), default(DateTimeOffset));
            }

            if (!TryParseApplicationId(args[0], out Snowflake appId))
            {
                DiscordExtension.GlobalLogger.Error("Failed to parse application ID from bot token. Bot token is invalid. Token: {0}", hiddenToken);
            }
            
            if (!TryParseCreationDate(args[1], out DateTimeOffset createdDate))
            {
                DiscordExtension.GlobalLogger.Error("Failed to parse Token Creation Date from bot token. Bot token is invalid. Token: {0}", hiddenToken);
            }
            
            return new BotToken(token, hiddenToken, appId, createdDate);
        }

        private bool TryParseApplicationId(string base64AppId, out Snowflake id)
        {
            try
            {
                string appId = Encoding.UTF8.GetString(Convert.FromBase64String(base64AppId));
                id = new Snowflake(appId);
                return true;
            }
            catch
            {
                id = default(Snowflake);
                return false;
            }
        }
        
        private bool TryParseCreationDate(string base64CreationDate, out DateTimeOffset createdDate)
        {
            try
            {
                long timestamp = Convert.ToInt64(Convert.FromBase64String(base64CreationDate));
                createdDate = DateTimeOffset.FromUnixTimeSeconds(timestamp);
                return true;
            }
            catch
            {
                createdDate = default(DateTimeOffset);
                return false;
            }
        }
    }
}