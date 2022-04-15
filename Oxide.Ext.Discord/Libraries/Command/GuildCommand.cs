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

        public GuildCommand(Plugin plugin, string name, string hook, List<Snowflake> allowedChannels) : base(plugin, name, hook)
        {
            _allowedChannels = allowedChannels;
        }

        public override bool CanHandle(DiscordMessage message, DiscordChannel channel)
        {
            if (!message.GuildId.HasValue)
            {
                return false;
            }

            if (_allowedChannels == null || _allowedChannels.Count == 0 || _allowedChannels.Contains(channel.Id))
            {
                return true;
            }

            if (channel.ParentId.HasValue && _allowedChannels.Contains(channel.ParentId.Value))
            {
                return true;
            }
            
            return false;
        }
    }
}