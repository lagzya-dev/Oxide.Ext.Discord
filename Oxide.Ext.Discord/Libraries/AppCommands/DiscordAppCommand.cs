using System;
using System.Collections.Generic;
using System.Reflection;
using Oxide.Core.Libraries;
using Oxide.Core.Plugins;
using Oxide.Ext.Discord.Attributes.ApplicationCommands;
using Oxide.Ext.Discord.Entities;
using Oxide.Ext.Discord.Entities.Applications;
using Oxide.Ext.Discord.Entities.Interactions;
using Oxide.Ext.Discord.Extensions;
using Oxide.Ext.Discord.Libraries.AppCommands.Commands;
using Oxide.Ext.Discord.Libraries.AppCommands.Handlers;
using Oxide.Ext.Discord.Logging;
using Oxide.Ext.Discord.Pooling;
using Oxide.Plugins;

namespace Oxide.Ext.Discord.Libraries.AppCommands
{
    /// <summary>
    /// Application Command Oxide Library handler
    /// Routes Application Commands to their respective hook method handlers instead of having to manually handle it.
    /// </summary>
    public class DiscordAppCommand : Library
    {
        private readonly Hash<Snowflake, ApplicationCommandHandler> _slashCommands = new Hash<Snowflake, ApplicationCommandHandler>();
        private readonly Hash<Snowflake, MessageComponentHandler> _componentCommands = new Hash<Snowflake, MessageComponentHandler>();
        private readonly ILogger _logger;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="logger">Logger for the library</param>
        public DiscordAppCommand(ILogger logger) 
        {
            _logger = logger;
        }

        private ApplicationCommandHandler GetSlashCommandHandler(Snowflake appId)
        {
            return _slashCommands[appId];
        }
        
        private ApplicationCommandHandler GetSlashCommandHandler(DiscordApplication app)
        {
            return _slashCommands[app.Id];
        }
        
        private ApplicationCommandHandler GetOrAddSlashCommandHandler(DiscordApplication app)
        {
            ApplicationCommandHandler handler = _slashCommands[app.Id];
            if (handler == null)
            {
                handler = new ApplicationCommandHandler(_logger);
                _slashCommands[app.Id] = handler;
            }

            return handler;
        }
        
        private MessageComponentHandler GetComponentHandler(Snowflake appId)
        {
            return _componentCommands[appId];
        }
        
        private MessageComponentHandler GetComponentHandler(DiscordApplication app)
        {
            return _componentCommands[app.Id];
        }
        
        private MessageComponentHandler GetOrAddComponentHandler(DiscordApplication app)
        {
            MessageComponentHandler handler = _componentCommands[app.Id];
            if (handler == null)
            {
                handler = new MessageComponentHandler(_logger);
                _componentCommands[app.Id] = handler;
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
        public void AddApplicationCommand(Plugin plugin, DiscordApplication app, string callback, string command, string group = null, string subCommand = null)
        {
            if (plugin == null) throw new ArgumentNullException(nameof(plugin));
            if (app == null) throw new ArgumentNullException(nameof(app));
            if (string.IsNullOrEmpty(command)) throw new ArgumentNullException(nameof(command));
            if (string.IsNullOrEmpty(callback)) throw new ArgumentNullException(nameof(callback));
            
            ApplicationCommandHandler handler = GetOrAddSlashCommandHandler(app);
            AppCommandId commandId = new AppCommandId(command, group, subCommand);

            const InteractionType type = InteractionType.ApplicationCommand;
            AppCommand existing = handler.GetCommandById(type, commandId);
            if (existing != null && !existing.IsForPlugin(plugin))
            {
                _logger.Warning("{0} has replaced the '{1}' ({2}) discord application command previously registered by {3}", plugin.FullName(), command, type, existing.Plugin?.FullName());
            }
            
            handler.AddAppCommand(new AppCommand(plugin, app, type, commandId, callback));
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
        public void AddAutoCompleteCommand(Plugin plugin, DiscordApplication app, string callback, string command, string argument, string group = null, string subCommand = null)
        {
            if (plugin == null) throw new ArgumentNullException(nameof(plugin));
            if (app == null) throw new ArgumentNullException(nameof(app));
            if (string.IsNullOrEmpty(callback)) throw new ArgumentNullException(nameof(callback));
            if (string.IsNullOrEmpty(command)) throw new ArgumentNullException(nameof(command));
            if (string.IsNullOrEmpty(argument)) throw new ArgumentNullException(nameof(argument));
            
            ApplicationCommandHandler handler = GetOrAddSlashCommandHandler(app);
            AppCommandId commandId = new AppCommandId(command, group, subCommand, argument);

            const InteractionType type = InteractionType.ApplicationCommandAutoComplete;
            AppCommand existing = handler.GetCommandById(type, commandId);
            if (existing != null && !existing.IsForPlugin(plugin))
            {
                _logger.Warning("{0} has replaced the '{1}' ({2}) discord application command previously registered by {3}", plugin.FullName(), command, type, existing.Plugin?.FullName());
            }
            
            handler.AddAppCommand(new AutoCompleteCommand(plugin, app, commandId, callback));
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
        public void AddMessageComponentCommand(Plugin plugin, DiscordApplication app, string customId, string callback)
        {
            if (plugin == null) throw new ArgumentNullException(nameof(plugin));
            if (app == null) throw new ArgumentNullException(nameof(app));
            if (string.IsNullOrEmpty(customId)) throw new ArgumentNullException(nameof(customId));
            if (string.IsNullOrEmpty(callback)) throw new ArgumentNullException(nameof(callback));
            
            MessageComponentHandler handler = GetOrAddComponentHandler(app);
            handler.AddComponentCommand(new ComponentCommand(plugin, app, InteractionType.MessageComponent, customId, callback));
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
        public void AddModalSubmitCommand(Plugin plugin, DiscordApplication app, string customId, string callback)
        {
            if (plugin == null) throw new ArgumentNullException(nameof(plugin));
            if (app == null) throw new ArgumentNullException(nameof(app));
            if (string.IsNullOrEmpty(customId)) throw new ArgumentNullException(nameof(customId));
            if (string.IsNullOrEmpty(callback)) throw new ArgumentNullException(nameof(callback));
            
            const InteractionType type = InteractionType.ModalSubmit;
            MessageComponentHandler handler = GetOrAddComponentHandler(app);
            handler.AddComponentCommand(new ComponentCommand(plugin, app, type, customId, callback));
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

            AppCommand appCommand = GetSlashCommandHandler(app).GetCommandById(type, new AppCommandId(command, group, subCommand));
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
            ComponentCommand command = GetComponentHandler(app)?.GetCommandById(plugin, type, customId);
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

        internal void OnPluginUnloaded(Plugin plugin)
        {
            List<AppCommand> appCommands = DiscordPool.GetList<AppCommand>();
            foreach (ApplicationCommandHandler handler in _slashCommands.Values)
            {
                appCommands.AddRange(handler.GetCommandsForPlugin(plugin));
            }
            
            RemoveAppCommandsInternal(appCommands);
            DiscordPool.FreeList(ref appCommands);

            List<ComponentCommand> componentCommands = DiscordPool.GetList<ComponentCommand>();
            foreach (MessageComponentHandler handler in _componentCommands.Values)
            {
                componentCommands.AddRange(handler.GetCommandsForPlugin(plugin));
            }
            
            RemoveComponentsInternal(componentCommands);
            DiscordPool.FreeList(ref componentCommands);
        }
        
        internal void OnBotShutdown(BotClient client)
        {
            DiscordApplication app = client.Application;
            if (app != null)
            {
                _slashCommands.Remove(app.Id);
                _componentCommands.Remove(app.Id);
            }
        }
        
        private void RemoveComponentsInternal(IEnumerable<ComponentCommand> commandList)
        {
            List<ComponentCommand> componentCommands = DiscordPool.GetList<ComponentCommand>();
            componentCommands.AddRange(commandList);

            for (int index = 0; index < componentCommands.Count; index++)
            {
                ComponentCommand command = componentCommands[index];
                RemoveMessageComponentCommandInternal(command);
            }

            DiscordPool.FreeList(ref componentCommands);
        }
        
        private void RemoveAppCommandsInternal(IEnumerable<AppCommand> commandList)
        {
            List<AppCommand> commands = DiscordPool.GetList<AppCommand>();
            commands.AddRange(commandList);

            for (int index = 0; index < commands.Count; index++)
            {
                AppCommand command = commands[index];
                RemoveApplicationCommandInternal(command);
            }

            DiscordPool.FreeList(ref commands);
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
        
        internal void RegisterApplicationCommands(DiscordApplication app, Plugin plugin)
        {
            _logger.Debug("Registering application commands for {0}", plugin.FullName());
            
            foreach (MethodInfo method in plugin.GetType().GetMethods(BindingFlags.NonPublic | BindingFlags.Instance))
            {
                Attribute[] attributes = Attribute.GetCustomAttributes(method);
                if (attributes.Length == 0)
                {
                    continue;
                }

                for (int index = 0; index < attributes.Length; index++)
                {
                    Attribute attribute = attributes[index];
                    switch (attribute)
                    {
                        case DiscordAutoCompleteCommandAttribute autoComplete:
                            DiscordExtension.DiscordAppCommand.AddAutoCompleteCommand(plugin, app, method.Name, autoComplete.Command, autoComplete.ArgumentName, autoComplete.Group, autoComplete.SubCommand);
                            break;
                        case DiscordApplicationCommandAttribute appCommand:
                            DiscordExtension.DiscordAppCommand.AddApplicationCommand(plugin, app, method.Name, appCommand.Command, appCommand.Group, appCommand.SubCommand);
                            break;
                        case DiscordMessageComponentCommandAttribute component:
                            DiscordExtension.DiscordAppCommand.AddMessageComponentCommand(plugin, app, component.CustomId, method.Name);
                            break;
                        case DiscordModalSubmitAttribute modal:
                            DiscordExtension.DiscordAppCommand.AddModalSubmitCommand(plugin, app, modal.CustomId, method.Name);
                            break;
                    }
                }
            }
        }

        internal IEnumerable<BaseAppCommand> GetCommands(Snowflake applicationId)
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
    }
}