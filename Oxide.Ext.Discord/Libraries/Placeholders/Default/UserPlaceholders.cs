using System.Text;
using Oxide.Ext.Discord.Entities.Users;
using Oxide.Ext.Discord.Libraries.Placeholders.Types;

namespace Oxide.Ext.Discord.Libraries.Placeholders.Default
{
    internal class UserPlaceholders : PlaceholderCollection<DiscordUser>
    {
        private void Id(StringBuilder builder, DiscordUser user, PlaceholderMatch match) => PlaceholderFormatting.Replace(builder, match, user.Id);
        private void UserName(StringBuilder builder, DiscordUser user, PlaceholderMatch match) => PlaceholderFormatting.Replace(builder, match, user.Username);
        private void Discriminator(StringBuilder builder, DiscordUser user, PlaceholderMatch match) => PlaceholderFormatting.Replace(builder, match, user.Discriminator);
        private void FullName(StringBuilder builder, DiscordUser user, PlaceholderMatch match) => PlaceholderFormatting.Replace(builder, match, user.GetFullUserName);
        private void AvatarUrl(StringBuilder builder, DiscordUser user, PlaceholderMatch match) => PlaceholderFormatting.Replace(builder, match, user.GetAvatarUrl);
        private void BannerUrl(StringBuilder builder, DiscordUser user, PlaceholderMatch match) => PlaceholderFormatting.Replace(builder, match, user.GetBannerUrl);
        private void Mention(StringBuilder builder, DiscordUser user, PlaceholderMatch match) => PlaceholderFormatting.Replace(builder, match, user.Mention);

        public override void RegisterPlaceholders(DiscordPlaceholders placeholders)
        {
            placeholders.RegisterPlaceholderInternal<DiscordUser>("user.id", GetDataKey(), Id);
            placeholders.RegisterPlaceholderInternal<DiscordUser>("user.username", GetDataKey(), UserName);
            placeholders.RegisterPlaceholderInternal<DiscordUser>("user.discriminator", GetDataKey(), Discriminator);
            placeholders.RegisterPlaceholderInternal<DiscordUser>("user.fullname", GetDataKey(), FullName);
            placeholders.RegisterPlaceholderInternal<DiscordUser>("user.avatar.url", GetDataKey(), AvatarUrl);
            placeholders.RegisterPlaceholderInternal<DiscordUser>("user.banner.url", GetDataKey(), BannerUrl);
            placeholders.RegisterPlaceholderInternal<DiscordUser>("user.mention", GetDataKey(), Mention);
        }
    }
}