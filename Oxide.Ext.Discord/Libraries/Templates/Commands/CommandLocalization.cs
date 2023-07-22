using System.Collections.Generic;
using Newtonsoft.Json;
using Oxide.Ext.Discord.Entities.Interactions.ApplicationCommands;
using Oxide.Ext.Discord.Libraries.Locale;
using Oxide.Plugins;

namespace Oxide.Ext.Discord.Libraries.Templates.Commands
{
    /// <summary>
    /// Localization for Application Commands
    /// </summary>
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class CommandLocalization
    {
        /// <summary>
        /// Localization for <see cref="CommandCreate.Name"/> or <see cref="CommandOption.Name"/>
        /// </summary>
        [JsonProperty("Command Name")]
        public string Name { get; set; }
        
        /// <summary>
        /// Localization for <see cref="CommandCreate.Description"/> or <see cref="CommandOption.Description"/>
        /// </summary>
        [JsonProperty("Command Description")]
        public string Description { get; set; }
        
        /// <summary>
        /// Localized Options for the Command
        /// </summary>
        [JsonProperty("Command Options", NullValueHandling = NullValueHandling.Ignore)]
        public Hash<string, CommandLocalization> Options { get; set; }
        
        /// <summary>
        /// Localized Argument Options
        /// </summary>
        [JsonProperty("Argument Localization", NullValueHandling = NullValueHandling.Ignore)]
        public Hash<string, ArgumentLocalization> Arguments { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        [JsonConstructor]
        private CommandLocalization() { }
        
        private CommandLocalization(string name, string description)
        {
            Name = name;
            Description = description;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="create">Command to be created</param>
        /// <param name="locale">Oxide lang of the localization</param>
        public CommandLocalization(CommandCreate create, DiscordLocale locale) : this(create.NameLocalizations[locale.Id], create.DescriptionLocalizations[locale.Id])
        {
            if (create.Options != null)
            {
                for (int index = 0; index < create.Options.Count; index++)
                {
                    ProcessOption(create.Options[index], locale);
                }
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="opt"></param>
        /// <param name="locale"></param>
        public CommandLocalization(CommandOption opt, DiscordLocale locale) : this(opt.NameLocalizations[locale.Id], opt.DescriptionLocalizations[locale.Id])
        {
            if (opt.Options != null)
            { 
                for (int index = 0; index < opt.Options.Count; index++)
                {
                    ProcessOption(opt.Options[index], locale);
                }
            }
        }

        private void ProcessOption(CommandOption option, DiscordLocale locale)
        {
            if (option.Type == CommandOptionType.SubCommand || option.Type == CommandOptionType.SubCommandGroup)
            {
                if (Options == null)
                {
                    Options = new Hash<string, CommandLocalization>();
                }

                Options[option.Name] = new CommandLocalization(option, locale);
                return;
            }

            if (Arguments == null)
            {
                Arguments = new Hash<string, ArgumentLocalization>();
            }

            Arguments[option.Name] = new ArgumentLocalization(option, locale);
        }
        
        /// <summary>
        /// Apply Command Localizations to the <see cref="CommandCreate"/>
        /// </summary>
        /// <param name="create"></param>
        /// <param name="locale"></param>
        public void ApplyCommandLocalization(CommandCreate create, DiscordLocale locale)
        {
            if (create.NameLocalizations == null)
            {
                create.NameLocalizations = new Hash<string, string>();
            }

            if (create.DescriptionLocalizations == null)
            {
                create.DescriptionLocalizations = new Hash<string, string>();
            }
            
            create.NameLocalizations[locale.Id] = Name;
            create.DescriptionLocalizations[locale.Id] = Description;
            List<CommandOption> options = create.Options;
            if (options != null)
            {
                for (int index = 0; index < options.Count; index++)
                {
                    CommandOption option = options[index];
                    Options[option.Name]?.ApplyOptionLocalization(option, locale);
                }
            }
        }

        private void ApplyOptionLocalization(CommandOption opt, DiscordLocale locale)
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
                opt.NameLocalizations[locale.Id] = Name;
                opt.DescriptionLocalizations[locale.Id] = Description;

                if (Options != null)
                {
                    for (int index = 0; index < opt.Options.Count; index++)
                    {
                        CommandOption option = opt.Options[index];
                        Options[option.Name]?.ApplyOptionLocalization(option, locale);
                    }
                }

                return;
            }

            Arguments?[opt.Name]?.ApplyArgumentLocalization(opt, locale);
        }
    }
}