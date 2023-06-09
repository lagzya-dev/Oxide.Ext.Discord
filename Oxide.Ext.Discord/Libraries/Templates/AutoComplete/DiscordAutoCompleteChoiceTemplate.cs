using Newtonsoft.Json;
using Oxide.Ext.Discord.Entities.Interactions.ApplicationCommands;
using Oxide.Ext.Discord.Extensions;
using Oxide.Ext.Discord.Libraries.Locale;
using Oxide.Ext.Discord.Libraries.Placeholders;

namespace Oxide.Ext.Discord.Libraries.Templates.AutoComplete
{
    /// <summary>
    /// Template for Discord Auto Completes
    /// </summary>
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class DiscordAutoCompleteChoiceTemplate
    {
        /// <summary>
        /// Choice Text (1-100 characters)
        /// </summary>
        [JsonProperty("Choice Text")]
        public string Name { get; set; }

        /// <summary>
        /// Apply localizations to the auto complete choice
        /// </summary>
        /// <param name="locale">DiscordLocale being applied</param>
        /// <param name="choice">Choice to apply the template to</param>
        /// <param name="placeholders">Placeholders for be applied</param>
        public void ApplyLocalization(DiscordLocale locale, CommandOptionChoice choice, PlaceholderData placeholders = null)
        {
            string name = PlaceholderFormatting.ApplyPlaceholder(Name, placeholders);
            choice.NameLocalizations[locale.Id] = name.TrimIfLargerThan(100);
        }
        
        /// <summary>
        /// Apply the name to the auto complete choice
        /// </summary>
        /// <param name="choice">Choice to apply the template to</param>
        /// <param name="placeholders">Placeholders for be applied</param>
        public void ApplyName(CommandOptionChoice choice, PlaceholderData placeholders = null)
        {
            string name = PlaceholderFormatting.ApplyPlaceholder(Name, placeholders);
            choice.Name = name.TrimIfLargerThan(100);;
        }
    }
}