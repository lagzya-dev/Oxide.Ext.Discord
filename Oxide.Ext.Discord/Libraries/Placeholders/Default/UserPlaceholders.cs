using System.Text;
using Oxide.Ext.Discord.Entities.Users;

namespace Oxide.Ext.Discord.Libraries.Placeholders.Default
{
    internal static class UserPlaceholders
    {
        private static void Id(StringBuilder builder, DiscordUser user, PlaceholderMatch match) => PlaceholderFormatting.Replace(builder, match, user.Id);
        private static void UserName(StringBuilder builder, DiscordUser user, PlaceholderMatch match) => PlaceholderFormatting.Replace(builder, match, user.Username);
        private static void Discriminator(StringBuilder builder, DiscordUser user, PlaceholderMatch match) => PlaceholderFormatting.Replace(builder, match, user.Discriminator);
        private static void FullName(StringBuilder builder, DiscordUser user, PlaceholderMatch match) => PlaceholderFormatting.Replace(builder, match, user.GetFullUserName);
        private static void AvatarUrl(StringBuilder builder, DiscordUser user, PlaceholderMatch match) => PlaceholderFormatting.Replace(builder, match, user.GetAvatarUrl);
        private static void BannerUrl(StringBuilder builder, DiscordUser user, PlaceholderMatch match) => PlaceholderFormatting.Replace(builder, match, user.GetBannerUrl);
        private static void Mention(StringBuilder builder, DiscordUser user, PlaceholderMatch match) => PlaceholderFormatting.Replace(builder, match, user.Mention);

        public static void RegisterPlaceholders(DiscordPlaceholders placeholders)
        {
            placeholders.RegisterInternalPlaceholder<DiscordUser>("user.id", GetDataKey(), Id);
            placeholders.RegisterInternalPlaceholder<DiscordUser>("user.username", GetDataKey(), UserName);
            placeholders.RegisterInternalPlaceholder<DiscordUser>("user.discriminator", GetDataKey(), Discriminator);
            placeholders.RegisterInternalPlaceholder<DiscordUser>("user.fullname", GetDataKey(), FullName);
            placeholders.RegisterInternalPlaceholder<DiscordUser>("user.avatar.url", GetDataKey(), AvatarUrl);
            placeholders.RegisterInternalPlaceholder<DiscordUser>("user.banner.url", GetDataKey(), BannerUrl);
            placeholders.RegisterInternalPlaceholder<DiscordUser>("user.mention", GetDataKey(), Mention);
        }
        
        private static string GetDataKey() => nameof(DiscordUser);
    }
}