using System.Text;
using Oxide.Core.Plugins;
using Oxide.Ext.Discord.Entities.Interactions;
using Oxide.Ext.Discord.Plugins.Core;

namespace Oxide.Ext.Discord.Libraries.Placeholders.Default
{
    /// <summary>
    /// <see cref="DiscordInteraction"/> placeholders
    /// </summary>
    public static class InteractionPlaceholders
    {
        /// <summary>
        /// <see cref="DiscordInteraction.GetLangMessage(Oxide.Core.Plugins.Plugin,string)"/> placeholder
        /// </summary>
        public static void Lang(StringBuilder builder, PlaceholderState state, DiscordInteraction interaction) => PlaceholderFormatting.Replace(builder, state, interaction.GetLangMessage(state.Data.Get<Plugin>(), state.Format));

        internal static void RegisterPlaceholders()
        {
            RegisterPlaceholders(DiscordExtensionCore.Instance, "interaction");
        }
        
        /// <summary>
        /// Registers placeholders for the given plugin. 
        /// </summary>
        /// <param name="plugin">Plugin to register placeholders for</param>
        /// <param name="placeholderPrefix">Prefix to use for the placeholders</param>
        /// <param name="dataKey">Data key in <see cref="PlaceholderData"/></param>
        public static void RegisterPlaceholders(Plugin plugin, string placeholderPrefix, string dataKey = nameof(DiscordInteraction))
        {
            DiscordPlaceholders placeholders = DiscordExtension.DiscordPlaceholders;
            placeholders.RegisterPlaceholder<DiscordInteraction>(plugin, $"{placeholderPrefix}.lang", dataKey, Lang);
        }
    }
}