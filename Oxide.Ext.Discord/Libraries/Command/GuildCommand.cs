using System;
using System.Collections.Generic;
using Oxide.Core.Plugins;
using Oxide.Ext.Discord.Entities;
using Oxide.Ext.Discord.Entities.Channels;
using Oxide.Ext.Discord.Entities.Messages;

namespace Oxide.Ext.Discord.Libraries.Command
{
    internal class GuildCommand : BaseCommand
    {
        private readonly List<Snowflake> _allowedChannels;

        public GuildCommand(string name, Plugin plugin, List<Snowflake> allowedChannels, Action<Message, string, string[]> callback) : base(name, plugin, callback)
        {
            _allowedChannels = allowedChannels;
        }

        public override bool CanHandle(Message message, Channel channel)
        {
            if (message.GuildId == null)
            {
                return false;
            }

            if (_allowedChannels != null && !_allowedChannels.Contains(channel.Id) && (!channel.ParentId.HasValue || !_allowedChannels.Contains(channel.ParentId.Value)))
            {
                return false;
            }

            return true;
        }
    }
}