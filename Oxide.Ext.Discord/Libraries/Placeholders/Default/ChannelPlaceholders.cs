using System.Text;
using Oxide.Ext.Discord.Entities.Channels;
using Oxide.Ext.Discord.Libraries.Placeholders.Types;

namespace Oxide.Ext.Discord.Libraries.Placeholders.Default
{
    internal class ChannelPlaceholders : PlaceholderCollection<DiscordChannel>
    {
        private void Id(StringBuilder builder, DiscordChannel channel, PlaceholderMatch match) => PlaceholderFormatting.Replace(builder, match, channel.Id);
        private void Name(StringBuilder builder, DiscordChannel channel, PlaceholderMatch match) => PlaceholderFormatting.Replace(builder, match, channel.Name);
        private void Mention(StringBuilder builder, DiscordChannel channel, PlaceholderMatch match) => PlaceholderFormatting.Replace(builder, match, channel.Mention);

        public override void RegisterPlaceholders(DiscordPlaceholders placeholders)
        {
            placeholders.RegisterPlaceholderInternal<DiscordChannel>("channel.id", GetDataKey(), Id);
            placeholders.RegisterPlaceholderInternal<DiscordChannel>("channel.name", GetDataKey(), Name);
            placeholders.RegisterPlaceholderInternal<DiscordChannel>("channel.mention", GetDataKey(), Mention);
        }
    }
}