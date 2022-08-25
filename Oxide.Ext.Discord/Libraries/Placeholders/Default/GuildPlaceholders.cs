using System.Text;
using Oxide.Ext.Discord.Entities.Guilds;

namespace Oxide.Ext.Discord.Libraries.Placeholders.Default
{
    internal static class GuildPlaceholders
    {
        private static void Id(StringBuilder builder, PlaceholderState state, DiscordGuild guild) => PlaceholderFormatting.Replace(builder, state, guild.Id);
        private static void Name(StringBuilder builder, PlaceholderState state, DiscordGuild guild) => PlaceholderFormatting.Replace(builder, state, guild.Name);
        private static void Description(StringBuilder builder, PlaceholderState state, DiscordGuild guild) => PlaceholderFormatting.Replace(builder, state, guild.Description);
        private static void Icon(StringBuilder builder, PlaceholderState state, DiscordGuild guild) => PlaceholderFormatting.Replace(builder, state, guild.IconUrl);
        private static void Banner(StringBuilder builder, PlaceholderState state, DiscordGuild guild) => PlaceholderFormatting.Replace(builder, state, guild.BannerUrl);
        private static void MemberCount(StringBuilder builder, PlaceholderState state, DiscordGuild guild) => PlaceholderFormatting.Replace(builder, state, guild.Members.Count);

        public static void RegisterPlaceholders(DiscordPlaceholders placeholders)
        {
            placeholders.RegisterInternalPlaceholder<DiscordGuild>("guild.id", Id);
            placeholders.RegisterInternalPlaceholder<DiscordGuild>("guild.name", Name);
            placeholders.RegisterInternalPlaceholder<DiscordGuild>("guild.description", Description);
            placeholders.RegisterInternalPlaceholder<DiscordGuild>("guild.icon", Icon);
            placeholders.RegisterInternalPlaceholder<DiscordGuild>("guild.banner", Banner);
            placeholders.RegisterInternalPlaceholder<DiscordGuild>("guild.members.count", MemberCount);
        }
    }
}