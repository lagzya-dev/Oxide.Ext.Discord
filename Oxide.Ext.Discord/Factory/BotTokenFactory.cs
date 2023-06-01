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

        internal BotToken CreateFromToken(string token, string pluginName)
        {
            BotToken existingToken = _tokens[token];
            if (existingToken != null)
            {
                return existingToken;
            }
            
            string hiddenToken = GenerateHiddenToken(token);
            if (!_tokenValidator.IsMatch(token))
            {
                DiscordExtension.GlobalLogger.Warning("API Token does not appear to be a valid discord bot token: {0} for plugin {1}. " +
                                      "Please confirm you are using the correct bot token. " +
                                      "If the token is correct and this message is showing please let the Discord Extension Developers know.", hiddenToken, pluginName);
            }

            BotToken botToken;
            if (!ParseToken(token, out string base64AppId, out string base64CreationDate))
            {
                DiscordExtension.GlobalLogger.Error("Failed to parse token {0} for plugin {1}", hiddenToken, pluginName);
                botToken = new BotToken(token, hiddenToken, default(Snowflake), default(DateTimeOffset));
                _tokens[token] = botToken;
                return botToken;
            }

            if (!TryParseApplicationId(base64AppId, out Snowflake applicationId))
            {
                DiscordExtension.GlobalLogger.Error("Failed to parse application ID from bot token. Bot token is invalid. Token: {0}", hiddenToken);
            }
            
            if (!TryParseCreationDate(base64CreationDate, out DateTimeOffset creationDate))
            {
                DiscordExtension.GlobalLogger.Error($"Failed to parse Token Creation Date from bot token. Bot token is invalid. Token: {hiddenToken}");
            }
            
            botToken = new BotToken(token, hiddenToken, applicationId, creationDate);
            _tokens[token] = botToken;
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
        
        private bool ParseToken(string token, out string base64AppId, out string base64CreationDate)
        {
            string[] args = token.Split(_splitArgs);
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