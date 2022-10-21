using Newtonsoft.Json;
using Oxide.Ext.Discord.Entities.Interactions.ApplicationCommands;
using Oxide.Plugins;

namespace Oxide.Ext.Discord.Libraries.Templates.Commands
{
    /// <summary>
    /// Localization for Application Command Arguments
    /// </summary>
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class ArgumentLocalization
    {
        /// <summary>
        /// Localization for <see cref="CommandOption.Name"/>
        /// </summary>
        [JsonProperty("Argument Name")]
        public string Name { get; set; }
        
        /// <summary>
        /// Localization for <see cref="CommandOption.Description"/>
        /// </summary>
        [JsonProperty("Argument Description")]
        public string Description { get; set; }
        
        /// <summary>
        /// Localization for Select Menu Choices
        /// </summary>
        [JsonProperty("Argument Choices", NullValueHandling = NullValueHandling.Ignore)]
        public Hash<string, ChoicesLocalization> Choices { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        [JsonConstructor]
        public ArgumentLocalization() { }
        
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name"></param>
        /// <param name="description"></param>
        public ArgumentLocalization(string name, string description)
        {
            Name = name;
            Description = description;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="option">Option to create the localization for</param>
        /// <param name="lang">The oxide lang for the localization</param>
        public ArgumentLocalization(CommandOption option, string lang) : this(option.NameLocalizations[lang], option.DescriptionLocalizations[lang])
        {
            if (option.Choices != null)
            {
                Choices = new Hash<string, ChoicesLocalization>();
                for (int index = 0; index < option.Choices.Count; index++)
                {
                    CommandOptionChoice choice = option.Choices[index];
                    Choices[choice.Name] = new ChoicesLocalization(choice.Name);
                }
            }
        }

        /// <summary>
        /// Apply localizations to the command option
        /// </summary>
        /// <param name="option"></param>
        /// <param name="language"></param>
        public void ApplyArgumentLocalization(CommandOption option, string language)
        {
            option.NameLocalizations[language] = Name;
            option.DescriptionLocalizations[language] = Description;

            if (option.Choices != null)
            {
                for (int index = 0; index < option.Choices.Count; index++)
                {
                    CommandOptionChoice choice = option.Choices[index];
                    Choices?[choice.Name]?.ApplyChoiceLocalization(choice, language);
                }
            }
        }
    }
}