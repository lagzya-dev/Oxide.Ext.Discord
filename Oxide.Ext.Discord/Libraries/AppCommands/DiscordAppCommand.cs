using System;
using System.Collections.Generic;
using Oxide.Core.Libraries;
using Oxide.Core.Plugins;
using Oxide.Ext.Discord.Entities;
using Oxide.Ext.Discord.Entities.Applications;
using Oxide.Ext.Discord.Entities.Interactions;
using Oxide.Ext.Discord.Exceptions.Libraries;
using Oxide.Ext.Discord.Libraries.AppCommands.Commands;
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
        private readonly Hash<Snowflake, AppCommands> _commands = new Hash<Snowflake, AppCommands>();
        private readonly ILogger _logger;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="logger">Logger for the library</param>
        public DiscordAppCommand(ILogger logger) 
        {
            _logger = logger;
        }

        /// <summary>
        /// Registers a new Application Command for the given plugin
        /// </summary>
        /// <param name="plugin">Plugin the Application Command is for</param>
        /// <param name="type">Type of the command</param>
        /// <param name="command">Command name</param>
        /// <param name="callback">Callback for the command</param>
        /// <exception cref="ArgumentNullException">Thrown if inputs are null</exception>
        public void AddApplicationCommand(Plugin plugin, DiscordApplication app, InteractionType type, string callback, string command, string group = null, string subCommand = null)
        {
            if (plugin == null) throw new ArgumentNullException(nameof(plugin));
            if (app == null) throw new ArgumentNullException(nameof(app));
            if (string.IsNullOrEmpty(command)) throw new ArgumentNullException(nameof(command));
            if (string.IsNullOrEmpty(callback)) throw new ArgumentNullException(nameof(callback));
            DiscordAppCommandException.ThrowIfMessageComponent(type);

            AppCommands appCommands = _commands[app.Id];
            if (appCommands == null)
            {
                appCommands = new AppCommands(_logger);
                _commands[app.Id] = appCommands;
            }
            
            AppCommandId commandId = new AppCommandId(command, group, subCommand);

            AppCommand existing = appCommands.GetCommandById(type, commandId);
            if (existing != null && !existing.IsForPlugin(plugin))
            {
                _logger.Warning("{0} has replaced the '{1}' ({2}) discord application command previously registered by {3}", plugin.Name, command, type, existing.Plugin?.Name);
            }
            
            appCommands.AddAppCommand(new AppCommand(plugin, app, type, commandId, callback));
            _logger.Debug("Adding App Command For: {0} Command: {1} Callback: {2}", plugin.Name, command, callback);
        }
        
        /// <summary>
        /// Adds a <see cref="InteractionType.MessageComponent"/> Command type.
        /// This matches CustomId with starts with
        /// </summary>
        /// <param name="plugin">Plugin the command is for</param>
        /// <param name="customId">Command to match with Starts with</param>
        /// <param name="callback">Callback for the command</param>
        public void AddMessageComponentCommand(Plugin plugin, DiscordApplication app, string customId, string callback)
        {
            if (plugin == null) throw new ArgumentNullException(nameof(plugin));
            if (app == null) throw new ArgumentNullException(nameof(app));
            if (string.IsNullOrEmpty(customId)) throw new ArgumentNullException(nameof(customId));
            if (string.IsNullOrEmpty(callback)) throw new ArgumentNullException(nameof(callback));
            
            AppCommands appCommands = _commands[app.Id];
            if (appCommands == null)
            {
                appCommands = new AppCommands(_logger);
                _commands[app.Id] = appCommands;
            }
            
            appCommands.AddComponentCommand(new ComponentCommand(plugin, app, InteractionType.MessageComponent, customId, callback));
        }

        /// <summary>
        /// Removes an application command
        /// </summary>
        /// <param name="plugin">Plugin to remove the command for</param>
        /// <param name="type">Type of the command</param>
        /// <param name="command">Command name</param>
        /// <exception cref="ArgumentNullException">Thrown if command is null or empty</exception>
        public void RemoveApplicationCommand(Plugin plugin, DiscordApplication app, InteractionType type, string command, string group, string subCommand)
        {
            if (string.IsNullOrEmpty(command)) throw new ArgumentNullException(nameof(command));

            AppCommand appCommand = _commands[app.Id]?.GetCommandById(type, new AppCommandId(command, group, subCommand));
            if (appCommand != null && appCommand.IsForPlugin(plugin))
            {
                RemoveApplicationCommandInternal(appCommand);
            }
        }
        
        private void RemoveApplicationCommandInternal(AppCommand appCommand)
        {
            AppCommands commands = _commands[appCommand.AppId];
            if (commands == null)
            {
                return;
            }

            if (!commands.RemoveAppCommand(appCommand))
            {
                return;
            }

            if (commands.IsEmpty)
            {
                _commands.Remove(appCommand.AppId);
            }
        }

        public void RemoveMessageComponentCommand(Plugin plugin, DiscordApplication app, string customId)
        {
            AppCommands commands = _commands[app.Id];
            if (commands == null)
            {
                return;
            }

            ComponentCommand command = commands.GetComponentCommandById(plugin, customId);
            if (command == null)
            {
                return;
            }
            
            RemoveMessageComponentCommandInternal(command);
        }

        private void RemoveMessageComponentCommandInternal(ComponentCommand command)
        {
            AppCommands commands = _commands[command.AppId];
            if (commands == null)
            {
                return;
            }

            if (!commands.RemoveComponentCommand(command))
            {
                return;
            }

            if (commands.IsEmpty)
            {
                _commands.Remove(command.AppId);
            }
        }

        internal void OnPluginUnloaded(Plugin plugin)
        {
            foreach (AppCommands commands in _commands.Values)
            {
                RemoveAppCommandsInternal(commands.GetCommandsForPlugin(plugin));
                RemoveComponentsInternal(commands.GetComponentCommandsForPlugin(plugin));
            }
        }
        
        internal void OnBotShutdown(BotClient client)
        {
            DiscordApplication app = client.Application;
            if (app == null)
            {
                return;
            }

            _commands.Remove(app.Id);
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
            var appCommands = _commands[interaction.ApplicationId];
            if (appCommands == null)
            {
                return false;
            }

            BaseAppCommand command = appCommands.GetInteractionCommand(interaction);
            if (command == null)
            {
                return false;
            }

            command.HandleCommand(interaction);
            return true;
        }
    }
}