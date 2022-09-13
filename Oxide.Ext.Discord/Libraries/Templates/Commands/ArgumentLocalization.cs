using Newtonsoft.Json;
using Oxide.Ext.Discord.Entities.Interactions.ApplicationCommands;
using Oxide.Plugins;

namespace Oxide.Ext.Discord.Libraries.Templates.Commands
{
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class ArgumentLocalization
    {
        [JsonProperty("Argument Name")]
        public string Name { get; set; }
        
        [JsonProperty("Argument Description")]
        public string Description { get; set; }
        
        [JsonProperty("Argument Choices", NullValueHandling = NullValueHandling.Ignore)]
        public Hash<string, ChoicesLocalization> Choices { get; set; }

        [JsonConstructor]
        public ArgumentLocalization() { }
        
        public ArgumentLocalization(string name, string description)
        {
            Name = name;
            Description = description;
        }

        public ArgumentLocalization(CommandOption option) : this(option.Name, option.Description)
        {
            if (option.Choices != null)
            {
                for (int index = 0; index < option.Choices.Count; index++)
                {
                    CommandOptionChoice choice = option.Choices[index];
                    Choices[choice.Name] = new ChoicesLocalization(choice.Name);
                }
            }
        }

        public void ApplyArgumentLocalization(CommandOption option, string language)
        {
            option.NameLocalizations[language] = Name;
            option.DescriptionLocalizations[language] = Description;

            if (option.Choices == null)
            {
                return;
            }

            for (int index = 0; index < option.Choices.Count; index++)
            {
                CommandOptionChoice choice = option.Choices[index];
                Choices?[choice.Name]?.ApplyChoiceLocalization(choice, language);
            }
        }
    }
}