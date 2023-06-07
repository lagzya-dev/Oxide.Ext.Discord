using System;
using System.Collections.Generic;
using Oxide.Core.Plugins;
using Oxide.Ext.Discord.Attributes.ApplicationCommands;
using Oxide.Ext.Discord.Connections;
using Oxide.Ext.Discord.Entities;
using Oxide.Ext.Discord.Entities.Applications;
using Oxide.Ext.Discord.Entities.Interactions;
using Oxide.Ext.Discord.Exceptions.Entities;
using Oxide.Ext.Discord.Extensions;
using Oxide.Ext.Discord.Factory;
using Oxide.Ext.Discord.Interfaces.Logging;
using Oxide.Ext.Discord.Libraries.AppCommands.Commands;
using Oxide.Ext.Discord.Libraries.AppCommands.Handlers;
using Oxide.Ext.Discord.Libraries.Pooling;
using Oxide.Ext.Discord.Logging;
using Oxide.Ext.Discord.Plugins;
using Oxide.Ext.Discord.Plugins.Setup;
using Oxide.Plugins;

namespace Oxide.Ext.Discord.Libraries.AppCommands
{
    /// <summary>
    /// Application Command Oxide Library handler
    /// Routes Application Commands to their respective hook method handlers instead of having to manually handle it.
    /// </summary>
    public class DiscordAppCommand : BaseDiscordLibrary<DiscordAppCommand>, IDebugLoggable
    {
        private readonly Hash<Snowflake, ApplicationCommandHandler> _slashCommands = new Hash<Snowflake, ApplicationCommandHandler>();
        private readonly Hash<Snowflake, MessageComponentHandler> _componentCommands = new Hash<Snowflake, MessageComponentHandler>();
        private readonly ILogger _logger;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="logger">Logger for the library</param>
        internal DiscordAppCommand(ILogger logger) 
        {
            _logger = logger;
        }

        private ApplicationCommandHandler GetSlashCommandHandler(Snowflake appId)
        {
            return _slashCommands[appId];
        }

        private ApplicationCommandHandler GetOrAddSlashCommandHandler(Snowflake appId)
        {
            ApplicationCommandHandler handler = _slashCommands[appId];
            if (handler == null)
            {
                handler = new ApplicationCommandHandler(_logger);
                _slashCommands[appId] = handler;
            }

            return handler;
        }
        
        private MessageComponentHandler GetComponentHandler(Snowflake appId)
        {
            return _componentCommands[appId];
        }

        private MessageComponentHandler GetOrAddComponentHandler(Snowflake appId)
        {
            MessageComponentHandler handler = _componentCommands[appId];
            if (handler == null)
            {
                handler = new MessageComponentHandler(_logger);
                _componentCommands[appId] = handler;
            }

            return handler;
        }

        /// <summary>
        /// Registers a new Application Command for the given plugin
        /// </summary>
        /// <param name="plugin"><see cref="Plugin"/> the Application Command is for</param>
        /// <param name="app"><see cref="DiscordApplication"/> for the command</param>
        /// <param name="callback">Callback for the command</param>
        /// <param name="command">Command name</param>
        /// <param name="group">Sub Command Group for the command</param>
        /// <param name="subCommand">Sub Command for the command</param>
        /// <exception cref="ArgumentNullException">Thrown if inputs are null</exception>
        public void AddApplicationCommand(Plugin plugin, Snowflake appId, string callback, string command, string group = null, string subCommand = null)
        {
            if (plugin == null) throw new ArgumentNullException(nameof(plugin));
            InvalidSnowflakeException.ThrowIfInvalid(appId, nameof(appId));
            if (string.IsNullOrEmpty(command)) throw new ArgumentNullException(nameof(command));
            if (string.IsNullOrEmpty(callback)) throw new ArgumentNullException(nameof(callback));
            
            ApplicationCommandHandler handler = GetOrAddSlashCommandHandler(appId);
            AppCommandId commandId = new AppCommandId(command, group, subCommand);

            const InteractionType Type = InteractionType.ApplicationCommand;
            AppCommand existing = handler.GetCommandById(Type, commandId);
            if (existing != null && !existing.IsForPlugin(plugin))
            {
                _logger.Warning("{0} has replaced the '{1}' ({2}) discord application command previously registered by {3}", plugin.FullName(), command, Type, existing.Plugin?.FullName());
            }
            
            handler.AddAppCommand(new AppCommand(plugin, appId, Type, commandId, callback));
            _logger.Debug("Adding App Command For: {0} Command: {1} Callback: {2}", plugin.FullName(), commandId, callback);
        }

        /// <summary>
        /// Registers a new Application Command for the given plugin
        /// </summary>
        /// <param name="plugin"><see cref="Plugin"/> the Application Command is for</param>
        /// <param name="app"><see cref="DiscordApplication"/> For the command</param>
        /// <param name="callback">Callback for the command</param>
        /// <param name="command">Command name</param>
        /// <param name="argument">Command Argument name for the Auto Complete</param>
        /// <param name="group">Sub Command Group for the command</param>
        /// <param name="subCommand">Sub Command for the command</param>
        /// <exception cref="ArgumentNullException">Thrown if inputs are null</exception>
        public void AddAutoCompleteCommand(Plugin plugin, Snowflake appId, string callback, string command, string argument, string group = null, string subCommand = null)
        {
            if (plugin == null) throw new ArgumentNullException(nameof(plugin));
            InvalidSnowflakeException.ThrowIfInvalid(appId, nameof(appId));
            if (string.IsNullOrEmpty(callback)) throw new ArgumentNullException(nameof(callback));
            if (string.IsNullOrEmpty(command)) throw new ArgumentNullException(nameof(command));
            if (string.IsNullOrEmpty(argument)) throw new ArgumentNullException(nameof(argument));
            
            ApplicationCommandHandler handler = GetOrAddSlashCommandHandler(appId);
            AppCommandId commandId = new AppCommandId(command, group, subCommand, argument);

            const InteractionType Type = InteractionType.ApplicationCommandAutoComplete;
            AppCommand existing = handler.GetCommandById(Type, commandId);
            if (existing != null && !existing.IsForPlugin(plugin))
            {
                _logger.Warning("{0} has replaced the '{1}' ({2}) discord application command previously registered by {3}", plugin.FullName(), command, Type, existing.Plugin?.FullName());
            }
            
            handler.AddAppCommand(new AutoCompleteCommand(plugin, appId, commandId, callback));
            _logger.Debug("Adding Auto Complete Command For: {0} Command: {1} Callback: {2}", plugin.FullName(), commandId, callback);
        }

        /// <summary>
        /// Adds a <see cref="InteractionType.MessageComponent"/> Command type.
        /// This matches CustomId with starts with
        /// </summary>
        /// <param name="plugin">Plugin the command is for</param>
        /// <param name="app"><see cref="DiscordApplication"/> for the command</param>
        /// <param name="customId">Command to match with Starts with</param>
        /// <param name="callback">Callback for the command</param>
        public void AddMessageComponentCommand(Plugin plugin, Snowflake appId, string customId, string callback)
        {
            if (plugin == null) throw new ArgumentNullException(nameof(plugin));
            InvalidSnowflakeException.ThrowIfInvalid(appId, nameof(appId));
            if (string.IsNullOrEmpty(customId)) throw new ArgumentNullException(nameof(customId));
            if (string.IsNullOrEmpty(callback)) throw new ArgumentNullException(nameof(callback));
            
            MessageComponentHandler handler = GetOrAddComponentHandler(appId);
            handler.AddComponentCommand(new ComponentCommand(plugin, appId, InteractionType.MessageComponent, customId, callback));
            _logger.Debug("Adding Message Component Command For: {0} CustomId: {1} Callback: {2}", plugin.FullName(), customId, callback);
        }

        /// <summary>
        /// Adds a <see cref="InteractionType.MessageComponent"/> Command type.
        /// This matches CustomId with starts with
        /// </summary>
        /// <param name="plugin">Plugin the command is for</param>
        /// <param name="app"><see cref="DiscordApplication"/> for the command</param>
        /// <param name="customId">Command to match with Starts with</param>
        /// <param name="callback">Callback for the command</param>
        public void AddModalSubmitCommand(Plugin plugin, Snowflake appId, string customId, string callback)
        {
            if (plugin == null) throw new ArgumentNullException(nameof(plugin));
            InvalidSnowflakeException.ThrowIfInvalid(appId, nameof(appId));
            if (string.IsNullOrEmpty(customId)) throw new ArgumentNullException(nameof(customId));
            if (string.IsNullOrEmpty(callback)) throw new ArgumentNullException(nameof(callback));
            
            const InteractionType Type = InteractionType.ModalSubmit;
            MessageComponentHandler handler = GetOrAddComponentHandler(appId);
            handler.AddComponentCommand(new ComponentCommand(plugin, appId, Type, customId, callback));
            _logger.Debug("Adding Modal Submit Command For: {0} CustomId: {1} Callback: {2}", plugin.FullName(), customId, callback);
        }

        /// <summary>
        /// Removes an application command
        /// </summary>
        /// <param name="plugin">Plugin to remove the command for</param>
        /// <param name="app"><see cref="DiscordApplication"/> for the command</param>
        /// <param name="type">Type of the command</param>
        /// <param name="command">Command name</param>
        /// <param name="group">Sub Command Group for the command</param>
        /// <param name="subCommand">Sub Command for the command</param>
        /// <exception cref="ArgumentNullException">Thrown if command is null or empty</exception>
        public void RemoveApplicationCommand(Plugin plugin, DiscordApplication app, InteractionType type, string command, string group, string subCommand)
        {
            if (string.IsNullOrEmpty(command)) throw new ArgumentNullException(nameof(command));

            AppCommand appCommand = GetSlashCommandHandler(app.Id).GetCommandById(type, new AppCommandId(command, group, subCommand));
            if (appCommand != null && appCommand.IsForPlugin(plugin))
            {
                RemoveApplicationCommandInternal(appCommand);
            }
        }
        
        private void RemoveApplicationCommandInternal(AppCommand appCommand)
        {
            ApplicationCommandHandler handler = GetSlashCommandHandler(appCommand.AppId);
            if (handler == null)
            {
                return;
            }

            if (!handler.RemoveAppCommand(appCommand))
            {
                return;
            }

            if (handler.IsEmpty)
            {
                _slashCommands.Remove(appCommand.AppId);
            }
        }

        /// <summary>
        /// Remove a message component command or a modal submit command
        /// </summary>
        /// <param name="plugin"><see cref="Plugin"/> the command is for</param>
        /// <param name="app"><see cref="DiscordApplication"/> for the command</param>
        /// <param name="type"><see cref="InteractionType"/> of the command</param>
        /// <param name="customId">CustomId to match StartsWith for</param>
        public void RemoveMessageComponentCommand(Plugin plugin, DiscordApplication app, InteractionType type, string customId)
        {
            ComponentCommand command = GetComponentHandler(app.Id)?.GetCommandById(plugin, type, customId);
            if (command == null)
            {
                return;
            }
            
            RemoveMessageComponentCommandInternal(command);
        }

        private void RemoveMessageComponentCommandInternal(ComponentCommand command)
        {
            MessageComponentHandler handler = GetComponentHandler(command.AppId);
            if (handler == null)
            {
                return;
            }

            if (!handler.RemoveComponentCommand(command))
            {
                return;
            }

            if (handler.IsEmpty)
            {
                _slashCommands.Remove(command.AppId);
            }
        }

        internal void RegisterApplicationCommands(PluginSetupData data, ClientConnection connection)
        {
            _logger.Debug("Registering application commands for {0}", data.PluginName);

            Plugin plugin = data.Plugin;
            Snowflake appId = connection.ApplicationId;
            foreach (PluginHookResult<BaseApplicationCommandAttribute> hook in data.GetHooksWithAttribute<BaseApplicationCommandAttribute>())
            {
                BaseApplicationCommandAttribute attribute = hook.Attribute;
                string name = hook.Name;
                
                switch (attribute)
                {
                    case DiscordAutoCompleteCommandAttribute autoComplete:
                        AddAutoCompleteCommand(plugin, appId, name, autoComplete.Command, autoComplete.ArgumentName, autoComplete.Group, autoComplete.SubCommand);
                        break;
                    case DiscordApplicationCommandAttribute appCommand:
                        AddApplicationCommand(plugin, appId, name, appCommand.Command, appCommand.Group, appCommand.SubCommand);
                        break;
                    case DiscordMessageComponentCommandAttribute component:
                        AddMessageComponentCommand(plugin, appId, component.CustomId, name);
                        break;
                    case DiscordModalSubmitAttribute modal:
                        AddModalSubmitCommand(plugin, appId, modal.CustomId, name);
                        break;
                }
            }
        }
        
        ///<inheritdoc/>
        protected override void OnPluginLoaded(PluginSetupData data)
        {
           
        }

        ///<inheritdoc/>
        protected override void OnPluginUnloaded(Plugin plugin)
        {
            List<AppCommand> appCommands = DiscordPool.Internal.GetList<AppCommand>();
            foreach (ApplicationCommandHandler handler in _slashCommands.Values)
            {
                appCommands.AddRange(handler.GetCommandsForPlugin(plugin));
            }
            
            RemoveAppCommandsInternal(appCommands);
            DiscordPool.Internal.FreeList(appCommands);

            List<ComponentCommand> componentCommands = DiscordPool.Internal.GetList<ComponentCommand>();
            foreach (MessageComponentHandler handler in _componentCommands.Values)
            {
                componentCommands.AddRange(handler.GetCommandsForPlugin(plugin));
            }
            
            RemoveComponentsInternal(componentCommands);
            DiscordPool.Internal.FreeList(componentCommands);
        }

        private void RemoveComponentsInternal(IEnumerable<ComponentCommand> commandList)
        {
            List<ComponentCommand> componentCommands = DiscordPool.Internal.GetList<ComponentCommand>();
            componentCommands.AddRange(commandList);

            for (int index = 0; index < componentCommands.Count; index++)
            {
                ComponentCommand command = componentCommands[index];
                RemoveMessageComponentCommandInternal(command);
            }

            DiscordPool.Internal.FreeList(componentCommands);
        }
        
        private void RemoveAppCommandsInternal(IEnumerable<AppCommand> commandList)
        {
            List<AppCommand> commands = DiscordPool.Internal.GetList<AppCommand>();
            commands.AddRange(commandList);

            for (int index = 0; index < commands.Count; index++)
            {
                AppCommand command = commands[index];
                RemoveApplicationCommandInternal(command);
            }

            DiscordPool.Internal.FreeList(commands);
        }

        internal bool HandleInteraction(DiscordInteraction interaction)
        {
            BaseAppCommand command = null;
            switch (interaction.Type)
            {
                case InteractionType.ApplicationCommand:
                case InteractionType.ApplicationCommandAutoComplete:
                    command = GetSlashCommandHandler(interaction.ApplicationId)?.GetInteractionCommand(interaction);
                    break;
                case InteractionType.MessageComponent:
                case InteractionType.ModalSubmit:
                    command = GetComponentHandler(interaction.ApplicationId)?.GetInteractionCommand(interaction);
                    break;
            }

            if (command == null)
            {
                return false;
            }
            
            command.HandleCommand(interaction);
            return true;
        }

        private IEnumerable<BaseAppCommand> GetCommands(Snowflake applicationId)
        {
            ApplicationCommandHandler appHandler = _slashCommands[applicationId];
            if (appHandler != null)
            {
                foreach (AppCommand command in appHandler.GetCommands())
                {
                    yield return command;
                }
            }
            
            MessageComponentHandler componentHandler = _componentCommands[applicationId];
            if (componentHandler != null)
            {
                foreach (ComponentCommand command in componentHandler.GetCommands())
                {
                    yield return command;
                }
            }
        }

        ///<inheritdoc/>
        public void LogDebug(DebugLogger logger)
        {
            logger.StartArray("Application Commands");
            foreach (BotClient client in BotClientFactory.Instance.Clients)
            {
                DiscordApplication application = client.Application;
                if (application != null)
                {
                    logger.AppendField("Application ID", application.Id);
                    logger.AppendList("Application Commands", GetCommands(application.Id));
                }
                else
                {
                    logger.AppendNullField("Application ID");
                }
            }
            logger.EndArray();
        }
    }
}