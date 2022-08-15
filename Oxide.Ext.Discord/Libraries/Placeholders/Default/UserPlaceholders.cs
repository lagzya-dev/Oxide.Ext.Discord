using System.Text;
using Oxide.Ext.Discord.Entities.Users;

namespace Oxide.Ext.Discord.Libraries.Placeholders.Default
{
    internal static class UserPlaceholders
    {
        private static void Id(StringBuilder builder, PlaceholderMatch match, DiscordUser user) => PlaceholderFormatting.Replace(builder, match, user.Id);
        private static void UserName(StringBuilder builder, PlaceholderMatch match, DiscordUser user) => PlaceholderFormatting.Replace(builder, match, user.Username);
        private static void Discriminator(StringBuilder builder, PlaceholderMatch match, DiscordUser user) => PlaceholderFormatting.Replace(builder, match, user.Discriminator);
        private static void FullName(StringBuilder builder, PlaceholderMatch match, DiscordUser user) => PlaceholderFormatting.Replace(builder, match, user.GetFullUserName);
        private static void AvatarUrl(StringBuilder builder, PlaceholderMatch match, DiscordUser user) => PlaceholderFormatting.Replace(builder, match, user.GetAvatarUrl);
        private static void BannerUrl(StringBuilder builder, PlaceholderMatch match, DiscordUser user) => PlaceholderFormatting.Replace(builder, match, user.GetBannerUrl);
        private static void Mention(StringBuilder builder, PlaceholderMatch match, DiscordUser user) => PlaceholderFormatting.Replace(builder, match, user.Mention);

        public static void RegisterPlaceholders(DiscordPlaceholders placeholders)
        {
            placeholders.RegisterInternalPlaceholder<DiscordUser>("user.id", Id);
            placeholders.RegisterInternalPlaceholder<DiscordUser>("user.username", UserName);
            placeholders.RegisterInternalPlaceholder<DiscordUser>("user.discriminator", Discriminator);
            placeholders.RegisterInternalPlaceholder<DiscordUser>("user.fullname", FullName);
            placeholders.RegisterInternalPlaceholder<DiscordUser>("user.avatar.url", AvatarUrl);
            placeholders.RegisterInternalPlaceholder<DiscordUser>("user.banner.url", BannerUrl);
            placeholders.RegisterInternalPlaceholder<DiscordUser>("user.mention", Mention);
        }
    }
}