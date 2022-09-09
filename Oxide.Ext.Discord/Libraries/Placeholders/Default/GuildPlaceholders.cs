using System.Text;
using Oxide.Core.Plugins;
using Oxide.Ext.Discord.Entities.Guilds;
using Oxide.Ext.Discord.Plugins.Core;

namespace Oxide.Ext.Discord.Libraries.Placeholders.Default
{
    public static class GuildPlaceholders
    {
        public static void Id(StringBuilder builder, PlaceholderState state, DiscordGuild guild) => PlaceholderFormatting.Replace(builder, state, guild.Id);
        public static void Name(StringBuilder builder, PlaceholderState state, DiscordGuild guild) => PlaceholderFormatting.Replace(builder, state, guild.Name);
        public static void Description(StringBuilder builder, PlaceholderState state, DiscordGuild guild) => PlaceholderFormatting.Replace(builder, state, guild.Description);
        public static void Icon(StringBuilder builder, PlaceholderState state, DiscordGuild guild) => PlaceholderFormatting.Replace(builder, state, guild.IconUrl);
        public static void Banner(StringBuilder builder, PlaceholderState state, DiscordGuild guild) => PlaceholderFormatting.Replace(builder, state, guild.BannerUrl);
        public static void MemberCount(StringBuilder builder, PlaceholderState state, DiscordGuild guild) => PlaceholderFormatting.Replace(builder, state, guild.Members.Count);

        internal static void RegisterPlaceholders()
        {
            RegisterPlaceholders(DiscordExtensionCore.Instance, "guild", nameof(DiscordGuild));
        }
        
        public static void RegisterPlaceholders(Plugin plugin, string placeholderPrefix, string dataKey)
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