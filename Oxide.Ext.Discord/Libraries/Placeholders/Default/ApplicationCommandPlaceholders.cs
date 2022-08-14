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
            placeholders.RegisterInternalPlaceholder<DiscordApplicationCommand>("command.id", Id);
            placeholders.RegisterInternalPlaceholder<DiscordApplicationCommand>("command.name", Name);
            placeholders.RegisterInternalPlaceholder<DiscordApplicationCommand>("command.mention", Mention);
            placeholders.RegisterInternalPlaceholder<DiscordApplicationCommand>("command.mention.custom", MentionCustom);
        }
    }
}