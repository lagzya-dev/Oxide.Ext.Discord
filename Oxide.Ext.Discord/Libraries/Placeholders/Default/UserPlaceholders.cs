using System.Text;
using Oxide.Core.Plugins;
using Oxide.Ext.Discord.Entities.Users;
using Oxide.Ext.Discord.Extensions;
using Oxide.Ext.Discord.Plugins.Core;

namespace Oxide.Ext.Discord.Libraries.Placeholders.Default
{
    /// <summary>
    /// <see cref="DiscordUser"/> placeholders
    /// </summary>
    public static class UserPlaceholders
    {
        /// <summary>
        /// <see cref="DiscordUser.Id"/> placeholder
        /// </summary>
        public static void Id(StringBuilder builder, PlaceholderState state, DiscordUser user) => PlaceholderFormatting.Replace(builder, state, user.Id);
        
        /// <summary>
        /// <see cref="DiscordUser.Username"/> placeholder
        /// </summary>
        public static void UserName(StringBuilder builder, PlaceholderState state, DiscordUser user) => PlaceholderFormatting.Replace(builder, state, user.Username);
        
        /// <summary>
        /// <see cref="DiscordUser.Discriminator"/> placeholder
        /// </summary>
        public static void Discriminator(StringBuilder builder, PlaceholderState state, DiscordUser user) => PlaceholderFormatting.Replace(builder, state, user.Discriminator);
        
        /// <summary>
        /// <see cref="DiscordUser.GetFullUserName"/> placeholder
        /// </summary>
        public static void FullName(StringBuilder builder, PlaceholderState state, DiscordUser user) => PlaceholderFormatting.Replace(builder, state, user.GetFullUserName);
        
        /// <summary>
        /// <see cref="DiscordUser.GetAvatarUrl"/> placeholder
        /// </summary>
        public static void AvatarUrl(StringBuilder builder, PlaceholderState state, DiscordUser user) => PlaceholderFormatting.Replace(builder, state, user.GetAvatarUrl);
        
        /// <summary>
        /// <see cref="DiscordUser.GetBannerUrl"/> placeholder
        /// </summary>
        public static void BannerUrl(StringBuilder builder, PlaceholderState state, DiscordUser user) => PlaceholderFormatting.Replace(builder, state, user.GetBannerUrl);
        
        /// <summary>
        /// <see cref="DiscordUser.Mention"/> placeholder
        /// </summary>
        public static void Mention(StringBuilder builder, PlaceholderState state, DiscordUser user) => PlaceholderFormatting.Replace(builder, state, user.Mention);
        
        /// <summary>
        /// <see cref="DiscordUserExt.IsLinked"/> placeholder
        /// </summary>
        public static void IsLinked(StringBuilder builder, PlaceholderState state, DiscordUser user) => PlaceholderFormatting.Replace(builder, state, user.IsLinked());

        internal static void RegisterPlaceholders()
        {
            RegisterPlaceholders(DiscordExtensionCore.Instance, "user", nameof(DiscordUser));
        }
        
        /// <summary>
        /// Registers placeholders for the given plugin. 
        /// </summary>
        /// <param name="plugin">Plugin to register placeholders for</param>
        /// <param name="placeholderPrefix">Prefix to use for the placeholders</param>
        /// <param name="dataKey">Data key in <see cref="PlaceholderData"/></param>
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