using System.Text;
using Oxide.Ext.Discord.Entities.Guilds;

namespace Oxide.Ext.Discord.Libraries.Placeholders.Default
{
    internal class MemberPlaceholders
    {
        private static void Id(StringBuilder builder, PlaceholderMatch match, GuildMember member) => PlaceholderFormatting.Replace(builder, match, member.Id);
        private static void Name(StringBuilder builder, PlaceholderMatch match, GuildMember member) => PlaceholderFormatting.Replace(builder, match, member.DisplayName);
        private static void Mention(StringBuilder builder, PlaceholderMatch match, GuildMember member) => PlaceholderFormatting.Replace(builder, match, member.User.Mention);

        public static void RegisterPlaceholders(DiscordPlaceholders placeholders)
        {
            placeholders.RegisterInternalPlaceholder<GuildMember>("member.id", Id);
            placeholders.RegisterInternalPlaceholder<GuildMember>("member.name", Name);
            placeholders.RegisterInternalPlaceholder<GuildMember>("member.mention", Mention);
        }
    }
}