using System.Text;
using Oxide.Ext.Discord.Entities.Guilds;
using Oxide.Ext.Discord.Libraries.Placeholders.Types;

namespace Oxide.Ext.Discord.Libraries.Placeholders.Default
{
    internal class MemberPlaceholders : PlaceholderCollection<GuildMember>
    {
        private void Id(StringBuilder builder, GuildMember member, PlaceholderMatch match) => PlaceholderFormatting.Replace(builder, match, member.Id);
        private void Name(StringBuilder builder, GuildMember member, PlaceholderMatch match) => PlaceholderFormatting.Replace(builder, match, member.DisplayName);
        private void Mention(StringBuilder builder, GuildMember member, PlaceholderMatch match) => PlaceholderFormatting.Replace(builder, match, member.User.Mention);

        public override void RegisterPlaceholders(DiscordPlaceholders placeholders)
        {
            placeholders.RegisterPlaceholderInternal<GuildMember>("member.id", GetDataKey(), Id);
            placeholders.RegisterPlaceholderInternal<GuildMember>("member.name", GetDataKey(), Name);
            placeholders.RegisterPlaceholderInternal<GuildMember>("member.mention", GetDataKey(), Mention);
        }
    }
}