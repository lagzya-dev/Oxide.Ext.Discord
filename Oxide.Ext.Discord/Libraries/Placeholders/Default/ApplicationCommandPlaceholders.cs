using System.Text;
using Oxide.Core.Plugins;
using Oxide.Ext.Discord.Entities.Interactions.ApplicationCommands;
using Oxide.Ext.Discord.Plugins.Core;

namespace Oxide.Ext.Discord.Libraries.Placeholders.Default
{
    /// <summary>
    /// <see cref="DiscordApplicationCommand"/> placeholders
    /// </summary>
    public static class ApplicationCommandPlaceholders
    {
        /// <summary>
        /// <see cref="DiscordApplicationCommand.Id"/> placeholder
        /// </summary>
        public static void Id(StringBuilder builder, PlaceholderState state, DiscordApplicationCommand command) => PlaceholderFormatting.Replace(builder, state, command.Id);
        
        /// <summary>
        /// <see cref="DiscordApplicationCommand.Name"/> placeholder
        /// </summary>
        public static void Name(StringBuilder builder, PlaceholderState state, DiscordApplicationCommand command) => PlaceholderFormatting.Replace(builder, state, command.Name);
        
        /// <summary>
        /// <see cref="DiscordApplicationCommand.Mention"/> placeholder
        /// </summary>
        public static void Mention(StringBuilder builder, PlaceholderState state, DiscordApplicationCommand command) => PlaceholderFormatting.Replace(builder, state, command.Mention);
        
        /// <summary>
        /// <see cref="DiscordApplicationCommand.MentionCustom"/> placeholder
        /// </summary>
        public static void MentionCustom(StringBuilder builder, PlaceholderState state, DiscordApplicationCommand command) => PlaceholderFormatting.Replace(builder, state, command.MentionCustom(state.Format));
        
        internal static void RegisterPlaceholders()
        {
            RegisterPlaceholders(DiscordExtensionCore.Instance, "command");
        }
        
        /// <summary>
        /// Registers placeholders for the given plugin. 
        /// </summary>
        /// <param name="plugin">Plugin to register placeholders for</param>
        /// <param name="placeholderPrefix">Prefix to use for the placeholders</param>
        /// <param name="dataKey">Data key in <see cref="PlaceholderData"/></param>
        public static void RegisterPlaceholders(Plugin plugin, string placeholderPrefix, string dataKey = nameof(DiscordApplicationCommand))
        {
            DiscordPlaceholders placeholders = DiscordExtension.DiscordPlaceholders;
            placeholders.RegisterPlaceholder<DiscordApplicationCommand>(plugin, $"{placeholderPrefix}.id", dataKey, Id);
            placeholders.RegisterPlaceholder<DiscordApplicationCommand>(plugin, $"{placeholderPrefix}.name", dataKey, Name);
            placeholders.RegisterPlaceholder<DiscordApplicationCommand>(plugin, $"{placeholderPrefix}.mention", dataKey, Mention);
            placeholders.RegisterPlaceholder<DiscordApplicationCommand>(plugin, $"{placeholderPrefix}.mention.custom", dataKey, MentionCustom);
        }
    }
}