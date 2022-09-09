using System.Text;
using Oxide.Core.Plugins;
using Oxide.Ext.Discord.Entities.Permissions;
using Oxide.Ext.Discord.Plugins.Core;

namespace Oxide.Ext.Discord.Libraries.Placeholders.Default
{
    public static class RolePlaceholders
    {
        public static void Id(StringBuilder builder, PlaceholderState state, DiscordRole role) => PlaceholderFormatting.Replace(builder, state, role.Id);
        public static void Name(StringBuilder builder, PlaceholderState state, DiscordRole role) => PlaceholderFormatting.Replace(builder, state, role.Name);
        public static void Mention(StringBuilder builder, PlaceholderState state, DiscordRole role) => PlaceholderFormatting.Replace(builder, state, role.Mention);
        public static void Icon(StringBuilder builder, PlaceholderState state, DiscordRole role) => PlaceholderFormatting.Replace(builder, state, role.Icon);

        internal static void RegisterPlaceholders()
        {
            RegisterPlaceholders(DiscordExtensionCore.Instance, "role", nameof(DiscordRole));
        }
        
        public static void RegisterPlaceholders(Plugin plugin, string placeholderPrefix, string dataKey)
        {
            DiscordPlaceholders placeholders = DiscordExtension.DiscordPlaceholders;
            placeholders.RegisterPlaceholder<DiscordRole>(plugin, $"{placeholderPrefix}.id", dataKey, Id);
            placeholders.RegisterPlaceholder<DiscordRole>(plugin, $"{placeholderPrefix}.name", dataKey, Name);
            placeholders.RegisterPlaceholder<DiscordRole>(plugin, $"{placeholderPrefix}.mention", dataKey, Mention);
            placeholders.RegisterPlaceholder<DiscordRole>(plugin, $"{placeholderPrefix}.icon", dataKey, Icon);
        }
    }
}