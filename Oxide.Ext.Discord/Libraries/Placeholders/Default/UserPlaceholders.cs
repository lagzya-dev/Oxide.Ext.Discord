using System.Text;
using Oxide.Ext.Discord.Entities.Users;

namespace Oxide.Ext.Discord.Libraries.Placeholders.Default
{
    internal static class UserPlaceholders
    {
        private static void Id(StringBuilder builder, PlaceholderState state, DiscordUser user) => PlaceholderFormatting.Replace(builder, state, user.Id);
        private static void UserName(StringBuilder builder, PlaceholderState state, DiscordUser user) => PlaceholderFormatting.Replace(builder, state, user.Username);
        private static void Discriminator(StringBuilder builder, PlaceholderState state, DiscordUser user) => PlaceholderFormatting.Replace(builder, state, user.Discriminator);
        private static void FullName(StringBuilder builder, PlaceholderState state, DiscordUser user) => PlaceholderFormatting.Replace(builder, state, user.GetFullUserName);
        private static void AvatarUrl(StringBuilder builder, PlaceholderState state, DiscordUser user) => PlaceholderFormatting.Replace(builder, state, user.GetAvatarUrl);
        private static void BannerUrl(StringBuilder builder, PlaceholderState state, DiscordUser user) => PlaceholderFormatting.Replace(builder, state, user.GetBannerUrl);
        private static void Mention(StringBuilder builder, PlaceholderState state, DiscordUser user) => PlaceholderFormatting.Replace(builder, state, user.Mention);

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