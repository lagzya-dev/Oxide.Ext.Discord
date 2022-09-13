using System.Text;
using Oxide.Core.Plugins;
using Oxide.Ext.Discord.Entities.Guilds;
using Oxide.Ext.Discord.Plugins.Core;

namespace Oxide.Ext.Discord.Libraries.Placeholders.Default
{
    /// <summary>
    /// <see cref="GuildMember"/> placeholders
    /// </summary>
    public static class MemberPlaceholders
    {
        /// <summary>
        /// <see cref="GuildMember.Id"/> placeholder
        /// </summary>
        public static void Id(StringBuilder builder, PlaceholderState state, GuildMember member) => PlaceholderFormatting.Replace(builder, state, member.Id);
        
        /// <summary>
        /// <see cref="GuildMember.Id"/> placeholder
        /// </summary>
        public static void Name(StringBuilder builder, PlaceholderState state, GuildMember member) => PlaceholderFormatting.Replace(builder, state, member.DisplayName);
        
        /// <summary>
        /// <see cref="GuildMember.Id"/> placeholder
        /// </summary>
        public static void Mention(StringBuilder builder, PlaceholderState state, GuildMember member) => PlaceholderFormatting.Replace(builder, state, member.User.Mention);

        internal static void RegisterPlaceholders()
        {
            RegisterPlaceholders(DiscordExtensionCore.Instance, "member");
        }
        
        /// <summary>
        /// Registers placeholders for the given plugin. 
        /// </summary>
        /// <param name="plugin">Plugin to register placeholders for</param>
        /// <param name="placeholderPrefix">Prefix to use for the placeholders</param>
        /// <param name="dataKey">Data key in <see cref="PlaceholderData"/></param>
        public static void RegisterPlaceholders(Plugin plugin, string placeholderPrefix, string dataKey = nameof(GuildMember))
        {
            DiscordPlaceholders placeholders = DiscordExtension.DiscordPlaceholders;
            placeholders.RegisterPlaceholder<GuildMember>(plugin, $"{placeholderPrefix}.id", dataKey, Id);
            placeholders.RegisterPlaceholder<GuildMember>(plugin, $"{placeholderPrefix}.name", dataKey, Name);
            placeholders.RegisterPlaceholder<GuildMember>(plugin, $"{placeholderPrefix}.mention", dataKey, Mention);
        }
    }
}