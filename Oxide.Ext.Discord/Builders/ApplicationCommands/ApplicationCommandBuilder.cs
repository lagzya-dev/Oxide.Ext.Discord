using System;
using System.Collections.Generic;
using Oxide.Core.Plugins;
using Oxide.Ext.Discord.Entities.Interactions.ApplicationCommands;
using Oxide.Ext.Discord.Entities.Permissions;
using Oxide.Ext.Discord.Exceptions.Builders;
using Oxide.Ext.Discord.Exceptions.Entities.Interactions.ApplicationCommands;
using Oxide.Ext.Discord.Libraries.Templates.Commands;

namespace Oxide.Ext.Discord.Builders.ApplicationCommands
{
    /// <summary>
    /// Builder to use when building application commands
    /// </summary>
    public class ApplicationCommandBuilder
    {
        internal readonly CommandCreate Command;
        private CommandOptionType? _chosenType;

        /// <summary>
        /// Creates a new Application Command Builder
        /// </summary>
        /// <param name="name">Name of the command</param>
        /// <param name="description">Description of the command</param>
        /// <param name="type">Command type</param>
        public ApplicationCommandBuilder(string name, string description, ApplicationCommandType type)
        {
            InvalidApplicationCommandException.ThrowIfInvalidName(name, false);
            InvalidApplicationCommandException.ThrowIfInvalidDescription(description, false);
            
            Command = new CommandCreate(name, description, type, new List<CommandOption>());
        }

        /// <summary>
        /// Set whether the command is enabled by default when the app is added to a guild
        /// </summary>
        /// <param name="enabled">If the command is enabled</param>
        /// <returns>This</returns>
        public ApplicationCommandBuilder SetEnabled(bool enabled)
        {
            Command.DefaultPermissions = enabled;
            return this;
        }

        /// <summary>
        /// Adds command name localizations for a given plugin and lang key
        /// </summary>
        /// <param name="plugin">Plugin containing the localizations</param>
        /// <param name="langKey">Lang Key containing the localized text</param>
        /// <returns></returns>
        [Obsolete("AddNameLocalizations has been deprecated and will be removed in the future. Please upgrade to DiscordCommandLocalizations for Application Command localization")]
        public ApplicationCommandBuilder AddNameLocalizations(Plugin plugin, string langKey)
        {
            Command.NameLocalizations =  DiscordExtension.DiscordLang.GetCommandLocalization(plugin, langKey);
            return this;
        }
        
        /// <summary>
        /// Adds command description localizations for a given plugin and lang key
        /// </summary>
        /// <param name="plugin">Plugin containing the localizations</param>
        /// <param name="langKey">Lang Key containing the localized text</param>
        /// <returns></returns>
        [Obsolete("AddDescriptionLocalizations has been deprecated and will be removed in the future. Please upgrade to DiscordCommandLocalizations for Application Command localization")]
        public ApplicationCommandBuilder AddDescriptionLocalizations(Plugin plugin, string langKey)
        {
            Command.DescriptionLocalizations =  DiscordExtension.DiscordLang.GetCommandLocalization(plugin, langKey);
            return this;
        }
        
        /// <summary>
        /// Adds default command permissions
        /// </summary>
        /// <param name="permissions">Default Permissions for the command</param>
        /// <returns></returns>
        public ApplicationCommandBuilder AddDefaultPermissions(PermissionFlags permissions)
        {
            Command.DefaultMemberPermissions = permissions;
            return this;
        }
        
        /// <summary>
        /// Allows the command to be used in a direct message
        /// </summary>
        /// <param name="allowInDm">Allows a command to be used in a direct message</param>
        /// <returns></returns>
        public ApplicationCommandBuilder AddDirectMessagePermission(bool allowInDm)
        {
            Command.DmPermission = allowInDm;
            return this;
        }

        /// <summary>
        /// Creates a new SubCommandGroup
        /// SubCommandGroups contain subcommands
        /// Your root command can only contain 
        /// </summary>
        /// <param name="name">Name of the command</param>
        /// <param name="description">Description of the command</param>
        /// <returns><see cref="SubCommandGroupBuilder"/></returns>
        /// <exception cref="Exception">Thrown if trying to add a subcommand group to</exception>
        public SubCommandGroupBuilder AddSubCommandGroup(string name, string description)
        {
            InvalidCommandOptionException.ThrowIfInvalidName(name, false);
            InvalidCommandOptionException.ThrowIfInvalidDescription(description, false);

            ApplicationCommandBuilderException.ThrowIfMixingSubCommandGroups(_chosenType);
            ApplicationCommandBuilderException.ThrowIfAddingSubCommandToMessageOrUser(this);

            _chosenType = CommandOptionType.SubCommandGroup;

            return new SubCommandGroupBuilder(name, description, this);
        }

        /// <summary>
        /// Adds a sub command to the root command
        /// </summary>
        /// <param name="name">Name of the sub command</param>
        /// <param name="description">Description for the sub command</param>
        /// <returns><see cref="ApplicationSubCommandBuilder"/></returns>
        /// <exception cref="Exception">Thrown if previous type was not SubCommand or Creation type is not ChatInput</exception>
        public ApplicationSubCommandBuilder AddSubCommand(string name, string description)
        {
            InvalidCommandOptionException.ThrowIfInvalidName(name, false);
            InvalidCommandOptionException.ThrowIfInvalidDescription(description, false);

            ApplicationCommandBuilderException.ThrowIfMixingSubCommandGroups(_chosenType);
            ApplicationCommandBuilderException.ThrowIfAddingSubCommandToMessageOrUser(this);

            _chosenType = CommandOptionType.SubCommand;

            return new ApplicationSubCommandBuilder(Command.Options, name, description, this);
        }

        /// <summary>
        /// Adds a command option.
        /// </summary>
        /// <param name="type">The type of option. Cannot be SubCommand or SubCommandGroup</param>
        /// <param name="name">Name of the option</param>
        /// <param name="description">Description for the option</param>
        /// <returns><see cref="ApplicationCommandOptionBuilder"/></returns>
        public ApplicationCommandOptionBuilder AddOption(CommandOptionType type, string name, string description)
        {
            ApplicationCommandBuilderException.ThrowIfMixingCommandOptions(_chosenType);

            return new ApplicationCommandOptionBuilder(Command.Options, type, name, description, this);
        }

        /// <summary>
        /// Returns the created command
        /// </summary>
        /// <returns></returns>
        public CommandCreate Build()
        {
            return Command;
        }

        /// <summary>
        /// Returns a built <see cref="DiscordCommandLocalization"/> using the provided name / descriptions as the default
        /// </summary>
        /// <returns></returns>
        public DiscordCommandLocalization BuildCommandLocalization()
        {
            return new DiscordCommandLocalization(Command);
        }
    }
}