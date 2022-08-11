using System.Text;
using Oxide.Ext.Discord.Entities.Interactions.ApplicationCommands;

namespace Oxide.Ext.Discord.Libraries.Placeholders.Default
{
    internal static class ApplicationCommandPlaceholders
    {
        private static void Id(StringBuilder builder, DiscordApplicationCommand command, PlaceholderMatch match) => PlaceholderFormatting.Replace(builder, match, command.Id);
        private static void Name(StringBuilder builder, DiscordApplicationCommand command, PlaceholderMatch match) => PlaceholderFormatting.Replace(builder, match, command.Name);
        private static void Mention(StringBuilder builder, DiscordApplicationCommand command, PlaceholderMatch match) => PlaceholderFormatting.Replace(builder, match, command.Mention);
        private static void MentionCustom(StringBuilder builder, DiscordApplicationCommand command, PlaceholderMatch match) => PlaceholderFormatting.Replace(builder, match, command.MentionCustom(match.Format));
        
        public static void RegisterPlaceholders(DiscordPlaceholders placeholders)
        {
            placeholders.RegisterInternalPlaceholder<DiscordApplicationCommand>("command.id", GetDataKey(), Id);
            placeholders.RegisterInternalPlaceholder<DiscordApplicationCommand>("command.name", GetDataKey(), Name);
            placeholders.RegisterInternalPlaceholder<DiscordApplicationCommand>("command.mention", GetDataKey(), Mention);
            placeholders.RegisterInternalPlaceholder<DiscordApplicationCommand>("command.mention.custom", GetDataKey(), MentionCustom);
        }

        private static string GetDataKey() => nameof(DiscordApplicationCommand);
    }
}