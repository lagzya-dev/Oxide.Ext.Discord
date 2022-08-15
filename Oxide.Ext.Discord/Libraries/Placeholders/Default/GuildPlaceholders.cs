using System.Text;
using Oxide.Ext.Discord.Entities.Guilds;

namespace Oxide.Ext.Discord.Libraries.Placeholders.Default
{
    internal static class GuildPlaceholders
    {
        private static void Id(StringBuilder builder, PlaceholderMatch match, DiscordGuild guild) => PlaceholderFormatting.Replace(builder, match, guild.Id);
        private static void Name(StringBuilder builder, PlaceholderMatch match, DiscordGuild guild) => PlaceholderFormatting.Replace(builder, match, guild.Name);
        private static void Description(StringBuilder builder, PlaceholderMatch match, DiscordGuild guild) => PlaceholderFormatting.Replace(builder, match, guild.Description);
        private static void Icon(StringBuilder builder, PlaceholderMatch match, DiscordGuild guild) => PlaceholderFormatting.Replace(builder, match, guild.IconUrl);
        private static void Banner(StringBuilder builder, PlaceholderMatch match, DiscordGuild guild) => PlaceholderFormatting.Replace(builder, match, guild.BannerUrl);
        private static void MemberCount(StringBuilder builder, PlaceholderMatch match, DiscordGuild guild) => PlaceholderFormatting.Replace(builder, match, guild.Members.Count);

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