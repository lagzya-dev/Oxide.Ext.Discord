using System.Text;
using Oxide.Core.Plugins;
using Oxide.Ext.Discord.Entities.Users;
using Oxide.Ext.Discord.Extensions;
using Oxide.Ext.Discord.Plugins.Core;

namespace Oxide.Ext.Discord.Libraries.Placeholders.Default
{
    public static class UserPlaceholders
    {
        public static void Id(StringBuilder builder, PlaceholderState state, DiscordUser user) => PlaceholderFormatting.Replace(builder, state, user.Id);
        public static void UserName(StringBuilder builder, PlaceholderState state, DiscordUser user) => PlaceholderFormatting.Replace(builder, state, user.Username);
        public static void Discriminator(StringBuilder builder, PlaceholderState state, DiscordUser user) => PlaceholderFormatting.Replace(builder, state, user.Discriminator);
        public static void FullName(StringBuilder builder, PlaceholderState state, DiscordUser user) => PlaceholderFormatting.Replace(builder, state, user.GetFullUserName);
        public static void AvatarUrl(StringBuilder builder, PlaceholderState state, DiscordUser user) => PlaceholderFormatting.Replace(builder, state, user.GetAvatarUrl);
        public static void BannerUrl(StringBuilder builder, PlaceholderState state, DiscordUser user) => PlaceholderFormatting.Replace(builder, state, user.GetBannerUrl);
        public static void Mention(StringBuilder builder, PlaceholderState state, DiscordUser user) => PlaceholderFormatting.Replace(builder, state, user.Mention);
        public static void IsLinked(StringBuilder builder, PlaceholderState state, DiscordUser user) => PlaceholderFormatting.Replace(builder, state, user.IsLinked());

        internal static void RegisterPlaceholders()
        {
            RegisterPlaceholders(DiscordExtensionCore.Instance, "user", nameof(DiscordUser));
        }
        
        public static void RegisterPlaceholders(Plugin plugin, string placeholderPrefix, string dataKey)
        {
            DiscordPlaceholders placeholders = DiscordExtension.DiscordPlaceholders;
            placeholders.RegisterPlaceholder<DiscordUser>(plugin, $"{placeholderPrefix}.id", dataKey, Id);
            placeholders.RegisterPlaceholder<DiscordUser>(plugin, $"{placeholderPrefix}.username", dataKey, UserName);
            placeholders.RegisterPlaceholder<DiscordUser>(plugin, $"{placeholderPrefix}.discriminator", dataKey, Discriminator);
            placeholders.RegisterPlaceholder<DiscordUser>(plugin, $"{placeholderPrefix}.fullname", dataKey, FullName);
            placeholders.RegisterPlaceholder<DiscordUser>(plugin, $"{placeholderPrefix}.avatar.url", dataKey, AvatarUrl);
            placeholders.RegisterPlaceholder<DiscordUser>(plugin, $"{placeholderPrefix}.banner.url", dataKey, BannerUrl);
            placeholders.RegisterPlaceholder<DiscordUser>(plugin, $"{placeholderPrefix}.mention", dataKey, Mention);
            placeholders.RegisterPlaceholder<DiscordUser>(plugin, $"{placeholderPrefix}.islinked", dataKey, IsLinked);
        }
    }
}