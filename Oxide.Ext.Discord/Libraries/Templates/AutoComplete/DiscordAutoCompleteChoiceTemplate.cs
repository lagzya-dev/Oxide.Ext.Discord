using Newtonsoft.Json;
using Oxide.Ext.Discord.Entities.Interactions.ApplicationCommands;
using Oxide.Ext.Discord.Extensions;
using Oxide.Ext.Discord.Libraries.Locale;
using Oxide.Ext.Discord.Libraries.Placeholders;

namespace Oxide.Ext.Discord.Libraries.Templates.AutoComplete
{
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class DiscordAutoCompleteChoiceTemplate
    {
        /// <summary>
        /// Choice Text (1-100 characters)
        /// </summary>
        [JsonProperty("Choice Text")]
        public string Name { get; set; }

        public void ApplyLocalization(DiscordLocale locale, CommandOptionChoice choice, PlaceholderData placeholders = null)
        {
            string name = PlaceholderFormatting.ApplyPlaceholder(Name, placeholders);
            choice.NameLocalizations[locale.Id] = name.TrimIfLargerThan(100);
        }
        
        public void ApplyName(CommandOptionChoice choice, PlaceholderData placeholders = null)
        {
            string name = PlaceholderFormatting.ApplyPlaceholder(Name, placeholders);
            choice.Name = name.TrimIfLargerThan(100);;
        }
    }
}