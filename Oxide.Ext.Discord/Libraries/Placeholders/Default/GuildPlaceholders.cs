using System.Text;
using Oxide.Ext.Discord.Entities.Guilds;
using Oxide.Ext.Discord.Libraries.Placeholders.Types;

namespace Oxide.Ext.Discord.Libraries.Placeholders.Default
{
    internal class GuildPlaceholders : PlaceholderCollection<DiscordGuild>
    {
        private void Id(StringBuilder builder, DiscordGuild guild, PlaceholderMatch match) => PlaceholderFormatting.Replace(builder, match, guild.Id);
        private void Name(StringBuilder builder, DiscordGuild guild, PlaceholderMatch match) => PlaceholderFormatting.Replace(builder, match, guild.Name);
        private void Description(StringBuilder builder, DiscordGuild guild, PlaceholderMatch match) => PlaceholderFormatting.Replace(builder, match, guild.Description);
        private void MemberCount(StringBuilder builder, DiscordGuild guild, PlaceholderMatch match) => PlaceholderFormatting.Replace(builder, match, guild.Members.Count);

        public override void RegisterPlaceholders(DiscordPlaceholders placeholders)
        {
            placeholders.RegisterPlaceholderInternal<DiscordGuild>("guild.id", GetDataKey(), Id);
            placeholders.RegisterPlaceholderInternal<DiscordGuild>("guild.name", GetDataKey(), Name);
            placeholders.RegisterPlaceholderInternal<DiscordGuild>("guild.description", GetDataKey(), Description);
            placeholders.RegisterPlaceholderInternal<DiscordGuild>("guild.members.count", GetDataKey(), MemberCount);
        }
    }
}