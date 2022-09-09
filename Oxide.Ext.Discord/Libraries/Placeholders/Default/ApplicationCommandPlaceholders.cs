using System.Text;
using Oxide.Core.Plugins;
using Oxide.Ext.Discord.Entities.Interactions.ApplicationCommands;
using Oxide.Ext.Discord.Plugins.Core;

namespace Oxide.Ext.Discord.Libraries.Placeholders.Default
{
    public static class ApplicationCommandPlaceholders
    {
        public static void Id(StringBuilder builder, PlaceholderState state, DiscordApplicationCommand command) => PlaceholderFormatting.Replace(builder, state, command.Id);
        public static void Name(StringBuilder builder, PlaceholderState state, DiscordApplicationCommand command) => PlaceholderFormatting.Replace(builder, state, command.Name);
        public static void Mention(StringBuilder builder, PlaceholderState state, DiscordApplicationCommand command) => PlaceholderFormatting.Replace(builder, state, command.Mention);
        public static void MentionCustom(StringBuilder builder, PlaceholderState state, DiscordApplicationCommand command) => PlaceholderFormatting.Replace(builder, state, command.MentionCustom(state.Format));
        
        internal static void RegisterPlaceholders()
        {
            RegisterPlaceholders(DiscordExtensionCore.Instance, "command", nameof(DiscordApplicationCommand));
        }
        
        public static void RegisterPlaceholders(Plugin plugin, string placeholderPrefix, string dataKey)
        {
            DiscordPlaceholders placeholders = DiscordExtension.DiscordPlaceholders;
            placeholders.RegisterPlaceholder<DiscordApplicationCommand>(plugin, $"{placeholderPrefix}.id", dataKey, Id);
            placeholders.RegisterPlaceholder<DiscordApplicationCommand>(plugin, $"{placeholderPrefix}.name", dataKey, Name);
            placeholders.RegisterPlaceholder<DiscordApplicationCommand>(plugin, $"{placeholderPrefix}.mention", dataKey, Mention);
            placeholders.RegisterPlaceholder<DiscordApplicationCommand>(plugin, $"{placeholderPrefix}.mention.custom", dataKey, MentionCustom);
        }
    }
}