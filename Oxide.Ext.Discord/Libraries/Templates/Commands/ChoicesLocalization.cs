using Newtonsoft.Json;
using Oxide.Ext.Discord.Entities.Interactions.ApplicationCommands;

namespace Oxide.Ext.Discord.Libraries.Templates.Commands
{
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class ChoicesLocalization
    {
        [JsonProperty("Choice Name")]
        public string Name { get; set; }

        [JsonConstructor]
        public ChoicesLocalization() { }
        
        public ChoicesLocalization(string name)
        {
            Name = name;
        }
        
        public void ApplyChoiceLocalization(CommandOptionChoice choice, string language)
        {
            choice.NameLocalizations[language] = Name;
        }
    }
}