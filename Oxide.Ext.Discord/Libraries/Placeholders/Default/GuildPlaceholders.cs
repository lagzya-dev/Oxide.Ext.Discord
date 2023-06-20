using Oxide.Core.Plugins;
using Oxide.Ext.Discord.Entities;
using Oxide.Ext.Discord.Entities.Guilds;
using Oxide.Ext.Discord.Plugins.Core;

namespace Oxide.Ext.Discord.Libraries.Placeholders.Default
{
    /// <summary>
    /// <see cref="DiscordGuild"/> placeholders
    /// </summary>
    public static class GuildPlaceholders
    {
        /// <summary>
        /// <see cref="DiscordGuild.Id"/> placeholder
        /// </summary>
        public static Snowflake Id(DiscordGuild guild) => guild.Id;
        
        /// <summary>
        /// <see cref="DiscordGuild.Name"/> placeholder
        /// </summary>
        public static string Name(DiscordGuild guild) => guild.Name;
        
        /// <summary>
        /// <see cref="DiscordGuild.Description"/> placeholder
        /// </summary>
        public static string Description(DiscordGuild guild) => guild.Description;
        
        /// <summary>
        /// <see cref="DiscordGuild.IconUrl"/> placeholder
        /// </summary>
        public static string Icon(DiscordGuild guild) => guild.IconUrl;
        
        /// <summary>
        /// <see cref="DiscordGuild.BannerUrl"/> placeholder
        /// </summary>
        public static string Banner(DiscordGuild guild) => guild.BannerUrl;
        
        /// <summary>
        /// <see cref="DiscordGuild.Members"/> count placeholder
        /// </summary>
        public static int MemberCount(DiscordGuild guild) => guild.Members.Count;

        internal static void RegisterPlaceholders()
        {
            RegisterPlaceholders(DiscordExtensionCore.Instance, "guild");
        }
        
        /// <summary>
        /// Registers placeholders for the given plugin. 
        /// </summary>
        /// <param name="plugin">Plugin to register placeholders for</param>
        /// <param name="placeholderPrefix">Prefix to use for the placeholders</param>
        /// <param name="dataKey">Data key in <see cref="PlaceholderData"/></param>
        public static void RegisterPlaceholders(Plugin plugin, string placeholderPrefix, string dataKey = nameof(DiscordGuild))
        {
            DiscordPlaceholders placeholders = DiscordPlaceholders.Instance;
            placeholders.RegisterPlaceholder<DiscordGuild, Snowflake>(plugin, $"{placeholderPrefix}.id", dataKey, Id);
            placeholders.RegisterPlaceholder<DiscordGuild, string>(plugin, $"{placeholderPrefix}.name", dataKey, Name);
            placeholders.RegisterPlaceholder<DiscordGuild, string>(plugin, $"{placeholderPrefix}.description", dataKey, Description);
            placeholders.RegisterPlaceholder<DiscordGuild, string>(plugin, $"{placeholderPrefix}.icon", dataKey, Icon);
            placeholders.RegisterPlaceholder<DiscordGuild, string>(plugin, $"{placeholderPrefix}.banner", dataKey, Banner);
            placeholders.RegisterPlaceholder<DiscordGuild, int>(plugin, $"{placeholderPrefix}.members.count", dataKey, MemberCount);
        }
    }
}