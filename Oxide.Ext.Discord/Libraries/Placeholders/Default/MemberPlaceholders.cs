using System.Text;
using Oxide.Core.Plugins;
using Oxide.Ext.Discord.Entities.Guilds;
using Oxide.Ext.Discord.Plugins.Core;

namespace Oxide.Ext.Discord.Libraries.Placeholders.Default
{
    public static class MemberPlaceholders
    {
        public static void Id(StringBuilder builder, PlaceholderState state, GuildMember member) => PlaceholderFormatting.Replace(builder, state, member.Id);
        public static void Name(StringBuilder builder, PlaceholderState state, GuildMember member) => PlaceholderFormatting.Replace(builder, state, member.DisplayName);
        public static void Mention(StringBuilder builder, PlaceholderState state, GuildMember member) => PlaceholderFormatting.Replace(builder, state, member.User.Mention);

        internal static void RegisterPlaceholders()
        {
            RegisterPlaceholders(DiscordExtensionCore.Instance, "member", nameof(GuildMember));
        }
        
        public static void RegisterPlaceholders(Plugin plugin, string placeholderPrefix, string dataKey)
        {
            DiscordPlaceholders placeholders = DiscordExtension.DiscordPlaceholders;
            placeholders.RegisterPlaceholder<GuildMember>(plugin, $"{placeholderPrefix}.id", dataKey, Id);
            placeholders.RegisterPlaceholder<GuildMember>(plugin, $"{placeholderPrefix}.name", dataKey, Name);
            placeholders.RegisterPlaceholder<GuildMember>(plugin, $"{placeholderPrefix}.mention", dataKey, Mention);
        }
    }
}