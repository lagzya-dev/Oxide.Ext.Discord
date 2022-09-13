using System.Text;
using Oxide.Core.Plugins;
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
        public static void Id(StringBuilder builder, PlaceholderState state, DiscordGuild guild) => PlaceholderFormatting.Replace(builder, state, guild.Id);
        
        /// <summary>
        /// <see cref="DiscordGuild.Name"/> placeholder
        /// </summary>
        public static void Name(StringBuilder builder, PlaceholderState state, DiscordGuild guild) => PlaceholderFormatting.Replace(builder, state, guild.Name);
        
        /// <summary>
        /// <see cref="DiscordGuild.Description"/> placeholder
        /// </summary>
        public static void Description(StringBuilder builder, PlaceholderState state, DiscordGuild guild) => PlaceholderFormatting.Replace(builder, state, guild.Description);
        
        /// <summary>
        /// <see cref="DiscordGuild.IconUrl"/> placeholder
        /// </summary>
        public static void Icon(StringBuilder builder, PlaceholderState state, DiscordGuild guild) => PlaceholderFormatting.Replace(builder, state, guild.IconUrl);
        
        /// <summary>
        /// <see cref="DiscordGuild.BannerUrl"/> placeholder
        /// </summary>
        public static void Banner(StringBuilder builder, PlaceholderState state, DiscordGuild guild) => PlaceholderFormatting.Replace(builder, state, guild.BannerUrl);
        
        /// <summary>
        /// <see cref="DiscordGuild.Members"/> count placeholder
        /// </summary>
        public static void MemberCount(StringBuilder builder, PlaceholderState state, DiscordGuild guild) => PlaceholderFormatting.Replace(builder, state, guild.Members.Count);

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
            DiscordPlaceholders placeholders = DiscordExtension.DiscordPlaceholders;
            placeholders.RegisterPlaceholder<DiscordGuild>(plugin, $"{placeholderPrefix}.id", dataKey, Id);
            placeholders.RegisterPlaceholder<DiscordGuild>(plugin, $"{placeholderPrefix}.name", dataKey, Name);
            placeholders.RegisterPlaceholder<DiscordGuild>(plugin, $"{placeholderPrefix}.description", dataKey, Description);
            placeholders.RegisterPlaceholder<DiscordGuild>(plugin, $"{placeholderPrefix}.icon", dataKey, Icon);
            placeholders.RegisterPlaceholder<DiscordGuild>(plugin, $"{placeholderPrefix}.banner", dataKey, Banner);
            placeholders.RegisterPlaceholder<DiscordGuild>(plugin, $"{placeholderPrefix}.members.count", dataKey, MemberCount);
        }
    }
}