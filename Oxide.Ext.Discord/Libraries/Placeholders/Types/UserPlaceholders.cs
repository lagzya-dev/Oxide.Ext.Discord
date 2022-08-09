using System.Text;
using Oxide.Ext.Discord.Entities.Users;

namespace Oxide.Ext.Discord.Libraries.Placeholders.Types
{
    internal class UserPlaceholders : BasePlaceholders<DiscordUser>
    {
        public UserPlaceholders()
        {
            RegisterPlaceholder("user.id", Id);
            RegisterPlaceholder("user.username", UserName);
            RegisterPlaceholder("user.discriminator", Discriminator);
            RegisterPlaceholder("user.fullname", FullName);
            RegisterPlaceholder("user.avatar.url", AvatarUrl);
            RegisterPlaceholder("user.banner.url", BannerUrl);
            RegisterPlaceholder("user.mention", Mention);
        }
        
        private void Id(StringBuilder builder, DiscordUser user, PlaceholderMatch match) => Replace(builder, match, user.Id);
        private void UserName(StringBuilder builder, DiscordUser user, PlaceholderMatch match) => Replace(builder, match, user.Username);
        private void Discriminator(StringBuilder builder, DiscordUser user, PlaceholderMatch match) => Replace(builder, match, user.Discriminator);
        private void FullName(StringBuilder builder, DiscordUser user, PlaceholderMatch match) => Replace(builder, match, user.GetFullUserName);
        private void AvatarUrl(StringBuilder builder, DiscordUser user, PlaceholderMatch match) => Replace(builder, match, user.GetAvatarUrl);
        private void BannerUrl(StringBuilder builder, DiscordUser user, PlaceholderMatch match) => Replace(builder, match, user.GetBannerUrl);
        private void Mention(StringBuilder builder, DiscordUser user, PlaceholderMatch match) => Replace(builder, match, user.Mention);

        protected override string GetDataKey()
        {
            return nameof(DiscordUser);
        }
    }
}