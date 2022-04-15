using Oxide.Core.Plugins;
using Oxide.Ext.Discord.Entities.Channels;
using Oxide.Ext.Discord.Entities.Messages;

namespace Oxide.Ext.Discord.Libraries.Command
{
    internal class DirectMessageCommand : BaseCommand
    {
        internal DirectMessageCommand(Plugin plugin, string name, string hook) : base(plugin, name, hook)
        {
            
        }
        
        public override bool CanHandle(DiscordMessage message, DiscordChannel channel)
        {
            return !message.GuildId.HasValue;
        }
    }
}