using System.Text;
using Oxide.Ext.Discord.Entities.Permissions;

namespace Oxide.Ext.Discord.Libraries.Placeholders.Default
{
    internal static class RolePlaceholders
    {
        private static void Id(StringBuilder builder, DiscordRole role, PlaceholderMatch match) => PlaceholderFormatting.Replace(builder, match, role.Id);
        private static void Name(StringBuilder builder, DiscordRole role, PlaceholderMatch match) => PlaceholderFormatting.Replace(builder, match, role.Name);
        private static void Mention(StringBuilder builder, DiscordRole role, PlaceholderMatch match) => PlaceholderFormatting.Replace(builder, match, role.Mention);
        private static void Icon(StringBuilder builder, DiscordRole role, PlaceholderMatch match) => PlaceholderFormatting.Replace(builder, match, role.Icon);

        public static void RegisterPlaceholders(DiscordPlaceholders placeholders)
        {
            placeholders.RegisterInternalPlaceholder<DiscordRole>("role.id", Id);
            placeholders.RegisterInternalPlaceholder<DiscordRole>("role.name", Name);
            placeholders.RegisterInternalPlaceholder<DiscordRole>("role.mention", Mention);
            placeholders.RegisterInternalPlaceholder<DiscordRole>("role.icon", Icon);
        }
    }
}