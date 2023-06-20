using Oxide.Core.Plugins;
using Oxide.Ext.Discord.Entities;
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
        public static Snowflake Id(DiscordUser user) => user.Id;
        
        /// <summary>
        /// <see cref="DiscordUser.Username"/> placeholder
        /// </summary>
        public static string UserName(DiscordUser user) => user.Username;
        
        /// <summary>
        /// <see cref="DiscordUser.Discriminator"/> placeholder
        /// </summary>
        public static string Discriminator(DiscordUser user) => user.Discriminator;
        
        /// <summary>
        /// <see cref="DiscordUser.FullUserName"/> placeholder
        /// </summary>
        public static string FullName(DiscordUser user) => user.FullUserName;
        
        /// <summary>
        /// <see cref="DiscordUser.GetAvatarUrl"/> placeholder
        /// </summary>
        public static string AvatarUrl(DiscordUser user) => user.GetAvatarUrl;
        
        /// <summary>
        /// <see cref="DiscordUser.GetBannerUrl"/> placeholder
        /// </summary>
        public static string BannerUrl(DiscordUser user) => user.GetBannerUrl;
        
        /// <summary>
        /// <see cref="DiscordUser.Mention"/> placeholder
        /// </summary>
        public static string Mention(DiscordUser user) => user.Mention;
        
        /// <summary>
        /// <see cref="DiscordUserExt.IsLinked"/> placeholder
        /// </summary>
        public static bool IsLinked(DiscordUser user) => user.IsLinked();

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
            DiscordPlaceholders placeholders = DiscordPlaceholders.Instance;
            placeholders.RegisterPlaceholder<DiscordUser, Snowflake>(plugin, $"{placeholderPrefix}.id", dataKey, Id);
            placeholders.RegisterPlaceholder<DiscordUser, string>(plugin, $"{placeholderPrefix}.username", dataKey, UserName);
            placeholders.RegisterPlaceholder<DiscordUser, string>(plugin, $"{placeholderPrefix}.discriminator", dataKey, Discriminator);
            placeholders.RegisterPlaceholder<DiscordUser, string>(plugin, $"{placeholderPrefix}.fullname", dataKey, FullName);
            placeholders.RegisterPlaceholder<DiscordUser, string>(plugin, $"{placeholderPrefix}.avatar.url", dataKey, AvatarUrl);
            placeholders.RegisterPlaceholder<DiscordUser, string>(plugin, $"{placeholderPrefix}.banner.url", dataKey, BannerUrl);
            placeholders.RegisterPlaceholder<DiscordUser, string>(plugin, $"{placeholderPrefix}.mention", dataKey, Mention);
            placeholders.RegisterPlaceholder<DiscordUser, bool>(plugin, $"{placeholderPrefix}.islinked", dataKey, IsLinked);
        }
    }
}