using System;
using System.Collections.Generic;
using Oxide.Ext.Discord.Entities.Interactions.ApplicationCommands;

namespace Oxide.Ext.Discord.Entities.Builders
{
    /// <summary>
    /// Builder to use when building application commands
    /// </summary>
    public class ApplicationCommandBuilder
    {
        internal readonly CommandCreate Create;
        private CommandOptionType? _chosenType;

        /// <summary>
        /// Creates a new Application Command Builder
        /// </summary>
        /// <param name="name">Name of the command</param>
        /// <param name="description">Description of the command</param>
        /// <param name="type">Command type</param>
        public ApplicationCommandBuilder(string name, string description, ApplicationCommandType type)
        {
            Create = new CommandCreate
            {
                Name = name,
                Description = description,
                Type = type,
                Options = new List<CommandOption>()
            };
        }

        /// <summary>
        /// Set whether the command is enabled by default when the app is added to a guild
        /// </summary>
        /// <param name="enabled">If the command is enabled</param>
        /// <returns>This</returns>
        public ApplicationCommandBuilder SetDefaultEnabled(bool enabled)
        {
            Create.DefaultPermissions = enabled;
            return this;
        }
        
        /// <summary>
        /// Creates a new SubCommandGroup
        /// SubCommandGroups contain subcommands
        /// Your root command can only contain 
        /// </summary>
        /// <param name="name">Name of the command</param>
        /// <param name="description">Description of the command</param>
        /// <returns><see cref="ApplicationCommandSubCommandGroupBuilder"/></returns>
        /// <exception cref="Exception">Thrown if trying to add a subcommand group to</exception>
        public ApplicationCommandSubCommandGroupBuilder AddSubCommandGroup(string name, string description)
        {
            if (_chosenType.HasValue && _chosenType.Value != CommandOptionType.SubCommandGroup)
            {
                throw new Exception("Cannot mix SubCommands and SubCommandGroups");
            }

            if (Create.Type == ApplicationCommandType.Message || Create.Type == ApplicationCommandType.User)
            {
                throw new Exception("Message and User commands cannot have sub command groups");
            }

            _chosenType = CommandOptionType.SubCommandGroup;
            
            return new ApplicationCommandSubCommandGroupBuilder(name, description, this);
        }

        /// <summary>
        /// Adds a sub command to the root command
        /// </summary>
        /// <param name="name">Name of the sub command</param>
        /// <param name="description">Description for the sub command</param>
        /// <returns><see cref="SubCommandOptionsBuilder"/></returns>
        /// <exception cref="Exception">Thrown if previous type was not SubCommand or Creation type is not ChatInput</exception>
        public SubCommandOptionsBuilder AddSubCommand(string name, string description)
        {
            if (_chosenType.HasValue && _chosenType.Value != CommandOptionType.SubCommand)
            {
                throw new Exception("Cannot mix SubCommands and SubCommandGroups");
            }
            
            if (Create.Type == ApplicationCommandType.Message || Create.Type == ApplicationCommandType.User)
            {
                throw new Exception("Message and User commands cannot have sub commands");
            }

            _chosenType = CommandOptionType.SubCommand;
            
            return new SubCommandOptionsBuilder(name, description, this);
        }
        
        /// <summary>
        /// Creates an option builder
        /// </summary>
        /// <returns><see cref="BaseSubCommandOptionsBuilder{T}"/></returns>
        /// <exception cref="Exception">Thrown if SubCommand or SubCommandGroup were previously specified</exception>
        public BaseSubCommandOptionsBuilder<ApplicationCommandBuilder> AddOptions()
        {
            if (_chosenType.HasValue && (_chosenType.Value == CommandOptionType.SubCommand || _chosenType.Value == CommandOptionType.SubCommandGroup))
            {
                throw new Exception("Cannot mix SubCommands and SubCommandGroups and Options");
            }

            _chosenType = CommandOptionType.Boolean;
            return new CommandOptionBuilder(this);
        }
        
        /// <summary>
        /// Returns the created command
        /// </summary>
        /// <returns></returns>
        public CommandCreate Build()
        {
            return Create;
        }
    }

    /// <summary>
    /// Build for Sub Command Groups
    /// </summary>
    public class ApplicationCommandSubCommandGroupBuilder
    {
        internal readonly CommandOption Option;

        internal ApplicationCommandSubCommandGroupBuilder(string name, string description, ApplicationCommandBuilder builder)
        {
            Option = new CommandOption
            {
                Name = name,
                Description = description,
                Type = CommandOptionType.SubCommandGroup,
                Options = new List<CommandOption>()
            };
            builder.Create.Options.Add(Option);
        }

        /// <summary>
        /// Adds a sub command to this sub command group
        /// </summary>
        /// <param name="name">Name of the command</param>
        /// <param name="description">Description of the command</param>
        /// <returns><see cref="SubCommandGroupOptionsBuilder"/></returns>
        public SubCommandGroupOptionsBuilder AddSubCommand(string name, string description)
        {
            return new SubCommandGroupOptionsBuilder(name, description, this);
        }
    }
    
    /// <summary>
    /// Base Sub Command builder
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BaseSubCommandOptionsBuilder<T>
    {
        /// <summary>
        /// Options list to have options added to
        /// </summary>
        protected List<CommandOption> Options;
        
        /// <summary>
        /// Builder that is adding options
        /// </summary>
        protected T Builder;
        
        internal BaseSubCommandOptionsBuilder()
        {
            
        }

        /// <summary>
        /// Adds a new option
        /// </summary>
        /// <param name="type">Option data type (Cannot be SubCommand or SubCommandGroup)</param>
        /// <param name="name">Name of the option</param>
        /// <param name="description">Description of the option</param>
        /// <param name="required">Is the option required</param>
        /// <param name="choices">Predefined choices for the option (Only for Integer and String types)</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public BaseSubCommandOptionsBuilder<T> AddOption(CommandOptionType type, string name, string description, bool required = false, List<CommandOptionChoice> choices = null)
        {
            if (type == CommandOptionType.SubCommand || type == CommandOptionType.SubCommandGroup)
            {
                throw new Exception($"{type} is not allowed to be used here. Valid types are any non command type.");
            }

            if (type != CommandOptionType.Integer && type != CommandOptionType.String && choices != null)
            {
                throw new Exception("Choices are only valid for integer and string command option types.");
            }
            
            Options.Add(new CommandOption
            {
                Type = type,
                Name = name,
                Description = description,
                Required = required,
                Choices = choices
            });
            return this;
        }

        /// <summary>
        /// Returns the builder
        /// </summary>
        /// <returns></returns>
        public T Build()
        {
            return Builder;
        }
    }

    /// <summary>
    /// Options Builder for SubCommand
    /// </summary>
    public class SubCommandOptionsBuilder : BaseSubCommandOptionsBuilder<ApplicationCommandBuilder>
    {
        internal SubCommandOptionsBuilder(string name, string description, ApplicationCommandBuilder builder)
        {
            Builder = builder;
            Options = new List<CommandOption>();
            Builder.Create.Options.Add(new CommandOption
            {
                Name = name,
                Description = description,
                Type = CommandOptionType.SubCommand,
                Options = Options
            });
        }
    }
    
    /// <summary>
    /// Options Builder for SubCommandGroup
    /// </summary>
    public class SubCommandGroupOptionsBuilder : BaseSubCommandOptionsBuilder<ApplicationCommandSubCommandGroupBuilder>
    {
        internal SubCommandGroupOptionsBuilder(string name, string description, ApplicationCommandSubCommandGroupBuilder builder)
        {
            Builder = builder;
            Options = new List<CommandOption>();
            Builder.Option.Options.Add(new CommandOption
            {
                Name = name,
                Description = description,
                Type = CommandOptionType.SubCommand,
                Options = Options
            });
        }
    }
    
    /// <summary>
    /// Options builder for ApplicationCommand builder
    /// </summary>
    public class CommandOptionBuilder : BaseSubCommandOptionsBuilder<ApplicationCommandBuilder>
    {
        internal CommandOptionBuilder(ApplicationCommandBuilder builder)
        {
            Builder = builder;
            Options = new List<CommandOption>();
            Builder.Create.Options = Options;
        }
    }
}