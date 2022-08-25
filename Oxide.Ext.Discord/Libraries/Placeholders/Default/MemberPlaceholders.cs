using System.Text;
using Oxide.Ext.Discord.Entities.Guilds;

namespace Oxide.Ext.Discord.Libraries.Placeholders.Default
{
    internal static class MemberPlaceholders
    {
        private static void Id(StringBuilder builder, PlaceholderState state, GuildMember member) => PlaceholderFormatting.Replace(builder, state, member.Id);
        private static void Name(StringBuilder builder, PlaceholderState state, GuildMember member) => PlaceholderFormatting.Replace(builder, state, member.DisplayName);
        private static void Mention(StringBuilder builder, PlaceholderState state, GuildMember member) => PlaceholderFormatting.Replace(builder, state, member.User.Mention);

        public static void RegisterPlaceholders(DiscordPlaceholders placeholders)
        {
            placeholders.RegisterInternalPlaceholder<GuildMember>("member.id", Id);
            placeholders.RegisterInternalPlaceholder<GuildMember>("member.name", Name);
            placeholders.RegisterInternalPlaceholder<GuildMember>("member.mention", Mention);
        }
    }
}