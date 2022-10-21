using Newtonsoft.Json;
using Oxide.Ext.Discord.Entities.Interactions.ApplicationCommands;

namespace Oxide.Ext.Discord.Libraries.Templates.Commands
{
    /// <summary>
    /// Localization for Select Menu Choices
    /// </summary>
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class ChoicesLocalization
    {
        /// <summary>
        /// Localization for <see cref="CommandOptionChoice.Name"/>
        /// </summary>
        [JsonProperty("Choice Name")]
        public string Name { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        [JsonConstructor]
        public ChoicesLocalization() { }

        /// <summary>
        /// Constructor for command option choice localization
        /// </summary>
        /// <param name="name">Localized choice name</param>
        public ChoicesLocalization(string name)
        {
            Name = name;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="option">Option to localize</param>
        /// <param name="lang">Oxide lang of the localization</param>
        public ChoicesLocalization(CommandOption option, string lang) : this(option.NameLocalizations[lang]) { }
        
        /// <summary>
        /// Apply Choice Localizations
        /// </summary>
        /// <param name="choice"></param>
        /// <param name="language"></param>
        public void ApplyChoiceLocalization(CommandOptionChoice choice, string language)
        {
            choice.NameLocalizations[language] = Name;
        }
    }
}