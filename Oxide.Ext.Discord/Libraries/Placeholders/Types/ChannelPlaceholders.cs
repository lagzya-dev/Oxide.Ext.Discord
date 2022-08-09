using System.Text;
using Oxide.Ext.Discord.Entities.Channels;
using Oxide.Ext.Discord.Entities.Users;

namespace Oxide.Ext.Discord.Libraries.Placeholders.Types
{
    internal class ChannelPlaceholders : BasePlaceholders<DiscordChannel>
    {
        internal ChannelPlaceholders()
        {
            RegisterPlaceholder("channel.id", Id);
            RegisterPlaceholder("channel.name", Name);
            RegisterPlaceholder("channel.mention", Mention);
        }
        
        private void Id(StringBuilder builder, DiscordChannel channel, PlaceholderMatch match) => Replace(builder, match, channel.Id);
        private void Name(StringBuilder builder, DiscordChannel channel, PlaceholderMatch match) => Replace(builder, match, channel.Name);
        private void Mention(StringBuilder builder, DiscordChannel channel, PlaceholderMatch match) => Replace(builder, match, channel.Mention);

        protected override string GetDataKey()
        {
            return nameof(DiscordUser);
        }
    }
}