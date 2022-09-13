using System.Collections.Generic;
using Newtonsoft.Json;
using Oxide.Ext.Discord.Entities.Interactions.ApplicationCommands;
using Oxide.Plugins;

namespace Oxide.Ext.Discord.Libraries.Templates.Commands
{
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class CommandLocalization
    {
        [JsonProperty("Command Name")]
        public string Name { get; set; }
        
        [JsonProperty("Command Description")]
        public string Description { get; set; }
        
        [JsonProperty("Command Options")]
        public List<CommandLocalization> Options { get; set; }
        
        [JsonProperty("Argument Localization")]
        public Hash<string, ArgumentLocalization> Arguments { get; set; }

        [JsonConstructor]
        public CommandLocalization() { }

        public CommandLocalization(string name, string description)
        {
            Name = name;
            Description = description;
        }

        public CommandLocalization(CommandCreate create)
        {
            Name = create.Name;
            Description = create.Description;
            if (create.Options == null)
            {
                return;
            }

            for (int index = 0; index < create.Options.Count; index++)
            {
                ProcessOption(create.Options[index]);
            }
        }

        public CommandLocalization(CommandOption opt)
        {
            Name = opt.Name;
            Description = opt.Description;
            if (opt.Options == null)
            {
                return;
            }

            for (int index = 0; index < opt.Options.Count; index++)
            {
                ProcessOption(opt.Options[index]);
            }
        }

        private void ProcessOption(CommandOption option)
        {
            if (option.Type == CommandOptionType.SubCommand || option.Type == CommandOptionType.SubCommandGroup)
            {
                if (Options == null)
                {
                    Options = new List<CommandLocalization>();
                }
                    
                Options.Add(new CommandLocalization(option));
                return;
            }

            if (Arguments == null)
            {
                Arguments = new Hash<string, ArgumentLocalization>();
            }

            Arguments[option.Name] = new ArgumentLocalization(option);
        }
        
        public void ApplyCommandLocalization(CommandCreate create, string language)
        {
            create.NameLocalizations[language] = Name;
            create.DescriptionLocalizations[language] = Description;
            if (create.Options == null)
            {
                return;
            }

            for (int index = 0; index < create.Options.Count; index++)
            {
                ApplyOptionLocalization(create.Options[index], language);
            }
        }

        private void ApplyOptionLocalization(CommandOption opt, string language)
        {
            if (opt.NameLocalizations == null)
            {
                opt.NameLocalizations = new Hash<string, string>();
            }

            if (opt.DescriptionLocalizations == null)
            {
                opt.DescriptionLocalizations = new Hash<string, string>();
            }
            
            if (opt.Type == CommandOptionType.SubCommand || opt.Type == CommandOptionType.SubCommandGroup)
            {
                opt.NameLocalizations[language] = Name;
                opt.DescriptionLocalizations[language] = Description;

                if (opt.Options != null)
                {
                    for (int index = 0; index < opt.Options.Count; index++)
                    {
                        ApplyOptionLocalization(opt.Options[index], language);
                    }
                }

                return;
            }

            Arguments?[opt.Name]?.ApplyArgumentLocalization(opt, language);
        }
    }
}