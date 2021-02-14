using System;
using Oxide.Core.Plugins;
using Oxide.Ext.Discord.Entities.Channels;
using Oxide.Ext.Discord.Entities.Messages;

namespace Oxide.Ext.Discord.Libraries.Command
{
    internal class DirectMessageCommand : BaseCommand
    {
        internal DirectMessageCommand(string name, Plugin plugin, Action<Message, string, string[]> callback) : base(name, plugin, callback)
        {
            
        }
        
        public override bool CanHandle(Message message, Channel channel)
        {
            return !message.GuildId.HasValue;
        }
    }
}