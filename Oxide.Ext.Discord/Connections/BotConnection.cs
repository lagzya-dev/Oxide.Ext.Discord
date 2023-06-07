using System;
using Oxide.Ext.Discord.Entities;
using Oxide.Ext.Discord.Factory;

namespace Oxide.Ext.Discord.Connections
{
    public class BotConnection : BaseConnection
    {
        public readonly string HiddenToken;
        
        public readonly Snowflake ApplicationId;
        
        internal readonly DateTimeOffset CreationDate;

        public BotConnection(DiscordClient client) : base(client.Connection)
        {
            BotToken token = BotTokenFactory.Instance.CreateFromClient(client);
            HiddenToken = token.HiddenToken;
            ApplicationId = token.ApplicationId;
            CreationDate = token.CreationDate;
        }
    }
}