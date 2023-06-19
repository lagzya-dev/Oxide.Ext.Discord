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
using Oxide.Ext.Discord.Interfaces.Logging;
using Oxide.Ext.Discord.Libraries.AppCommands.Commands;
using Oxide.Ext.Discord.Libraries.Pooling;
using Oxide.Ext.Discord.Logging;
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
        private readonly Hash<Snowflake, AppCommandHandler> _handlers = new Hash<Snowflake, AppCommandHandler>();
        private readonly ILogger _logger;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="logger">Logger for the library</param>
        internal DiscordAppCommand(ILogger logger) 
        {
            _logger = logger;
        }

        private AppCommandHandler GetCommandHandler(Snowflake applicationId) => _handlers[applicationId];

        private AppCommandHandler GetOrAddCommandHandler(Snowflake applicationId)
        {
            AppCommandHandler handler = _handlers[applicationId];
            if (handler == null)
            {
                handler = new AppCommandHandler(_logger);
                _handlers[applicationId] = handler;
            }

            return handler;
        }

        /// <summary>
        /// Registers a new Application Command for the given plugin
        /// </summary>
        /// <param name="plugin"><see cref="Plugin"/> the Application Command is for</param>
        /// <param name="applicationId">ID of the <see cref="DiscordApplication"/> for the command</param>
        /// <param name="callback">Callback for the command</param>
        /// <param name="command">Command name</param>
        /// <param name="group">Sub Command Group for the command</param>
        /// <param name="subCommand">Sub Command for the command</param>
        /// <exception cref="ArgumentNullException">Thrown if inputs are null</exception>
        public void AddApplicationCommand(Plugin plugin, Snowflake applicationId, Action<DiscordInteraction, InteractionDataParsed> callback, string command, string group = null, string subCommand = null)
        {
            if (plugin == null) throw new ArgumentNullException(nameof(plugin));
            InvalidSnowflakeException.ThrowIfInvalid(applicationId, nameof(applicationId));
            if (string.IsNullOrEmpty(command)) throw new ArgumentNullException(nameof(command));
            if (callback == null) throw new ArgumentNullException(nameof(callback));
            
            AppCommandHandler handler = GetOrAddCommandHandler(applicationId);
            const InteractionType Type = InteractionType.ApplicationCommand;
            AppCommandId commandId = new AppCommandId(Type, command, group, subCommand);
            
            BaseAppCommand existing = handler.GetCommandById(commandId);
            if (existing != null && !existing.IsForPlugin(plugin.Id()))
            {
                _logger.Warning("{0} has replaced the '{1}' ({2}) discord application command previously registered by {3}", plugin.PluginName(), command, Type, existing.PluginName);
            }

            handler.AddAppCommand(new AppCommand(plugin, applicationId, commandId, callback, _logger));
            _logger.Debug("Adding App Command For: {0} Command: {1} Callback: {2}", plugin.PluginName(), commandId, callback);
        }

        /// <summary>
        /// Registers a new Application Command for the given plugin
        /// </summary>
        /// <param name="plugin"><see cref="Plugin"/> the Application Command is for</param>
        /// <param name="applicationId">ID of <see cref="DiscordApplication"/> For the command</param>
        /// <param name="callback">Callback for the command</param>
        /// <param name="command">Command name</param>
        /// <param name="argument">Command Argument name for the Auto Complete</param>
        /// <param name="group">Sub Command Group for the command</param>
        /// <param name="subCommand">Sub Command for the command</param>
        /// <exception cref="ArgumentNullException">Thrown if inputs are null</exception>
        public void AddAutoCompleteCommand(Plugin plugin, Snowflake applicationId, Action<DiscordInteraction,InteractionDataOption> callback, string command, string argument, string group = null, string subCommand = null)
        {
            if (plugin == null) throw new ArgumentNullException(nameof(plugin));
            InvalidSnowflakeException.ThrowIfInvalid(applicationId, nameof(applicationId));
            if (callback == null) throw new ArgumentNullException(nameof(callback));
            if (string.IsNullOrEmpty(command)) throw new ArgumentNullException(nameof(command));
            if (string.IsNullOrEmpty(argument)) throw new ArgumentNullException(nameof(argument));
            
            AppCommandHandler handler = GetOrAddCommandHandler(applicationId);
            const InteractionType Type = InteractionType.ApplicationCommandAutoComplete;
            AppCommandId commandId = new AppCommandId(Type, command, group, subCommand, argument);

            BaseAppCommand existing = handler.GetCommandById(commandId);
            if (existing != null && !existing.IsForPlugin(plugin.Id()))
            {
                _logger.Warning("{0} has replaced the '{1}' ({2}) discord auto complete command previously registered by {3}", plugin.PluginName(), command, Type, existing.PluginName);
            }
            
            handler.AddAppCommand(new AutoCompleteCommand(plugin, applicationId, commandId, callback, _logger));
            _logger.Debug("Adding Auto Complete Command For: {0} Command: {1} Callback: {2}", plugin.PluginName(), commandId, callback);
        }

        /// <summary>
        /// Adds a <see cref="InteractionType.MessageComponent"/> Command type.
        /// This matches CustomId with starts with
        /// </summary>
        /// <param name="plugin">Plugin the command is for</param>
        /// <param name="applicationId">ID of <see cref="DiscordApplication"/> for the command</param>
        /// <param name="customId">Command to match with Starts with</param>
        /// <param name="callback">Callback for the command</param>
        public void AddMessageComponentCommand(Plugin plugin, Snowflake applicationId, string customId, Action<DiscordInteraction> callback)
        {
            if (plugin == null) throw new ArgumentNullException(nameof(plugin));
            InvalidSnowflakeException.ThrowIfInvalid(applicationId, nameof(applicationId));
            if (string.IsNullOrEmpty(customId)) throw new ArgumentNullException(nameof(customId));
            if (callback == null) throw new ArgumentNullException(nameof(callback));
            
            AppCommandHandler handler = GetOrAddCommandHandler(applicationId);
            const InteractionType Type = InteractionType.MessageComponent;
            AppCommandId commandId = new AppCommandId(Type, customId);
            
            BaseAppCommand existing = handler.GetCommandById(commandId);
            if (existing != null && !existing.IsForPlugin(plugin.Id()))
            {
                _logger.Warning("{0} has replaced the '{1}' ({2}) discord message component command previously registered by {3}", plugin.PluginName(), customId, Type, existing.PluginName);
            }
            
            handler.AddAppCommand(new ComponentCommand(plugin, applicationId, commandId, callback, _logger));
            _logger.Debug("Adding Message Component Command For: {0} CustomId: {1} Callback: {2}", plugin.PluginName(), customId, callback);
        }

        /// <summary>
        /// Adds a <see cref="InteractionType.MessageComponent"/> Command type.
        /// This matches CustomId with starts with
        /// </summary>
        /// <param name="plugin">Plugin the command is for</param>
        /// <param name="applicationId">ID of <see cref="DiscordApplication"/> for the command</param>
        /// <param name="customId">Command to match with Starts with</param>
        /// <param name="callback">Callback for the command</param>
        public void AddModalSubmitCommand(Plugin plugin, Snowflake applicationId, string customId, Action<DiscordInteraction> callback)
        {
            if (plugin == null) throw new ArgumentNullException(nameof(plugin));
            InvalidSnowflakeException.ThrowIfInvalid(applicationId, nameof(applicationId));
            if (string.IsNullOrEmpty(customId)) throw new ArgumentNullException(nameof(customId));
            if (callback == null) throw new ArgumentNullException(nameof(callback));
            
            const InteractionType Type = InteractionType.ModalSubmit;
            AppCommandHandler handler = GetOrAddCommandHandler(applicationId);
            AppCommandId commandId = new AppCommandId(Type, customId);
            
            BaseAppCommand existing = handler.GetCommandById(commandId);
            if (existing != null && !existing.IsForPlugin(plugin.Id()))
            {
                _logger.Warning("{0} has replaced the '{1}' ({2}) discord modal submit command previously registered by {3}", plugin.PluginName(), customId, Type, existing.PluginName);
            }
            
            handler.AddAppCommand(new ComponentCommand(plugin, applicationId, commandId, callback, _logger));
            _logger.Debug("Adding Modal Submit Command For: {0} CustomId: {1} Callback: {2}", plugin.PluginName(), customId, callback);
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

            BaseAppCommand appCommand = GetCommandHandler(app.Id).GetCommandById(new AppCommandId(type, command, group, subCommand));
            if (appCommand != null && appCommand.IsForPlugin(plugin.Id()))
            {
                RemoveApplicationCommandInternal(appCommand);
            }
        }
        
        private void RemoveApplicationCommandInternal(BaseAppCommand appCommand)
        {
            AppCommandHandler handler = GetCommandHandler(appCommand.AppId);
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
                _handlers.Remove(appCommand.AppId);
            }
        }

        internal void RegisterApplicationCommands(PluginSetup data, BotConnection connection)
        {
            _logger.Debug("Registering application commands for {0}", data.PluginName);

            Plugin plugin = data.Plugin;
            Snowflake applicationId = connection.ApplicationId;
            foreach (PluginHookResult<BaseApplicationCommandAttribute> hook in data.GetHooksWithAttribute<BaseApplicationCommandAttribute>())
            {
                switch (hook.Attribute)
                {
                    case DiscordAutoCompleteCommandAttribute autoComplete:
                    {
                        Action<DiscordInteraction, InteractionDataOption> callback = hook.Method.CreateDelegate<DiscordInteraction, InteractionDataOption>(plugin);
                        if (callback != null)
                        {
                            AddAutoCompleteCommand(plugin, applicationId, callback, autoComplete.Command, autoComplete.ArgumentName, autoComplete.Group, autoComplete.SubCommand);
                        }
                        break;

                    }
                    
                    case DiscordApplicationCommandAttribute appCommand:
                    {
                        Action<DiscordInteraction, InteractionDataParsed> callback = hook.Method.CreateDelegate<DiscordInteraction,InteractionDataParsed>(plugin);
                        if (callback != null)
                        {
                            AddApplicationCommand(plugin, applicationId, callback, appCommand.Command, appCommand.Group, appCommand.SubCommand);
                        }
                        
                        break;
                    }
                       
                    case DiscordMessageComponentCommandAttribute component:
                    {
                        Action<DiscordInteraction> callback = hook.Method.CreateDelegate<DiscordInteraction>(plugin);
                        if (callback != null)
                        {
                            AddMessageComponentCommand(plugin, applicationId, component.CustomId, callback);
                        }
                        
                        break;
                    }
                    
                    case DiscordModalSubmitAttribute modal:
                    {
                        Action<DiscordInteraction> callback = hook.Method.CreateDelegate<DiscordInteraction>(plugin);
                        if (callback != null)
                        {
                            AddModalSubmitCommand(plugin, applicationId, modal.CustomId, callback);
                        }
                        
                        break;
                    }
                }
            }
        }

        ///<inheritdoc/>
        protected override void OnPluginLoaded(PluginSetup data, BotConnection connection) => RegisterApplicationCommands(data, connection);

        ///<inheritdoc/>
        protected override void OnPluginUnloaded(Plugin plugin)
        {
            List<BaseAppCommand> appCommands = DiscordPool.Internal.GetList<BaseAppCommand>();
            foreach (AppCommandHandler handler in _handlers.Values)
            {
                appCommands.AddRange(handler.GetCommandsForPlugin(plugin));
            }
            
            RemoveAppCommandsInternal(appCommands);
        }

        private void RemoveAppCommandsInternal(IEnumerable<BaseAppCommand> commandList)
        {
            List<BaseAppCommand> commands = DiscordPool.Internal.GetList<BaseAppCommand>();
            commands.AddRange(commandList);

            for (int index = 0; index < commands.Count; index++)
            {
                BaseAppCommand command = commands[index];
                RemoveApplicationCommandInternal(command);
            }

            DiscordPool.Internal.FreeList(commands);
        }

        internal bool HandleInteraction(DiscordInteraction interaction)
        {
            BaseAppCommand command = GetCommandHandler(interaction.ApplicationId)?.GetCommandById(interaction.GetCommandId());
            if (command == null)
            {
                return false;
            }

            command.HandleCommand(interaction);
            return true;
        }

        ///<inheritdoc/>
        public void LogDebug(DebugLogger logger)
        {
            logger.StartArray("Application Commands");
            foreach (KeyValuePair<Snowflake, AppCommandHandler> handler in _handlers)
            {
                logger.AppendField("Application ID", handler.Key);
                logger.AppendList("Application Commands", handler.Value.GetCommands());
            }
            logger.EndArray();
        }
    }
}