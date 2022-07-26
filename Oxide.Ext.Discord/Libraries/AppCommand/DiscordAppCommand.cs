using System;
using System.Collections.Generic;
using Oxide.Core.Libraries;
using Oxide.Core.Plugins;
using Oxide.Ext.Discord.Entities.Interactions;
using Oxide.Ext.Discord.Exceptions.Libraries;
using Oxide.Ext.Discord.Logging;
using Oxide.Ext.Discord.Pooling;
using Oxide.Plugins;

namespace Oxide.Ext.Discord.Libraries.AppCommand
{
    /// <summary>
    /// Application Command Oxide Library handler
    /// Routes Application Commands to their respective hook method handlers instead of having to manually handle it.
    /// </summary>
    public class DiscordAppCommand : Library
    {
        private readonly Hash<InteractionType, Hash<string, AppCommand>> _appCommands = new Hash<InteractionType, Hash<string, AppCommand>>();
        private readonly List<AppCommand> _messageComponentCommands = new List<AppCommand>();
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
        public void AddCommand(Plugin plugin, InteractionType type, string command, string callback)
        {
            if (plugin == null) throw new ArgumentNullException(nameof(plugin));
            if (string.IsNullOrEmpty(command)) throw new ArgumentNullException(nameof(command));
            if (string.IsNullOrEmpty(callback)) throw new ArgumentNullException(nameof(callback));
            DiscordAppCommandException.ThrowIfMessageComponent(type);
            
            Hash<string, AppCommand> commands = _appCommands[type];
            if (commands == null)
            {
                commands = new Hash<string, AppCommand>();
                _appCommands[type] = commands;
            }

            AppCommand appCommand = commands[command];
            if (appCommand != null && appCommand.Plugin != plugin)
            {
                _logger.Warning("{0} has replaced the '{1}' ({2}) discord application command previously registered by {3}", plugin.Name, command, type, appCommand.Plugin?.Name);
            }
            
            _logger.Debug("Adding App Command For: {0} Command: {1} Callback: {2}", plugin.Name, command, callback);
            commands[command] = new AppCommand(plugin, type, command, callback);
        }
        
        /// <summary>
        /// Adds a <see cref="InteractionType.MessageComponent"/> Command type.
        /// This matches CustomId with starts with
        /// </summary>
        /// <param name="plugin">Plugin the command is for</param>
        /// <param name="command">Command to match with Starts with</param>
        /// <param name="callback">Callback for the command</param>
        public void AddMessageComponentCommand(Plugin plugin, string command, string callback)
        {
            for (int index = _messageComponentCommands.Count - 1; index >= 0; index--)
            {
                AppCommand componentCommand = _messageComponentCommands[index];
                if (componentCommand.Command == command)
                {
                    if (componentCommand.Plugin != null && componentCommand.Plugin.IsLoaded && componentCommand.Plugin != plugin)
                    {
                        _logger.Warning("{0} has replaced the '{1}' ({2}) discord application command previously registered by {3}", plugin.Name, command, InteractionType.MessageComponent, componentCommand.Plugin?.Name);
                    }
                    _messageComponentCommands.RemoveAt(index);
                }
            }
            
            _logger.Debug("Adding App Message Component Command For: {0} Command: {1} Callback: {2}", plugin.Name, command, callback);
            _messageComponentCommands.Add(new AppCommand(plugin, InteractionType.MessageComponent, command, callback));
        }

        /// <summary>
        /// Removes an application command
        /// </summary>
        /// <param name="plugin">Plugin to remove the command for</param>
        /// <param name="type">Type of the command</param>
        /// <param name="command">Command name</param>
        /// <exception cref="ArgumentNullException">Thrown if command is null or empty</exception>
        public void RemoveCommand(Plugin plugin, InteractionType type, string command)
        {
            if (string.IsNullOrEmpty(command)) throw new ArgumentNullException(nameof(command));
            
            AppCommand appCommand = _appCommands[type]?[command];
            if (appCommand != null && (appCommand.Plugin == null || !appCommand.Plugin.IsLoaded || appCommand.Plugin == plugin))
            {
                RemoveCommandInternal(appCommand);
            }
        }
        
        private void RemoveCommandInternal(AppCommand appCommand)
        {
            appCommand.OnRemoved();
            
            Hash<string, AppCommand> commands = _appCommands[appCommand.Type];
            if (commands != null)
            {
                commands.Remove(appCommand.Command);
                if (commands.Count == 0)
                {
                    _appCommands.Remove(appCommand.Type);
                }
            }
        }

        internal void OnPluginUnloaded(Plugin sender)
        {
            List<AppCommand> commands = DiscordPool.GetList<AppCommand>();
            foreach (KeyValuePair<InteractionType,Hash<string,AppCommand>> types in _appCommands)
            {
                foreach (KeyValuePair<string, AppCommand> command in types.Value)
                {
                    if (command.Value.Plugin == null || command.Value.Plugin == sender)
                    {
                        commands.Add(command.Value);
                    }
                }
            }

            for (int index = 0; index < commands.Count; index++)
            {
                AppCommand command = commands[index];
                RemoveCommandInternal(command);
            }

            DiscordPool.FreeList(ref commands);
        }

        internal bool HandleInteraction(DiscordInteraction interaction)
        {
            Hash<string, AppCommand> typeCommands = _appCommands[interaction.Type];
            if (typeCommands == null)
            {
                return false;
            }
            
            AppCommand command = null;
            InteractionData data = interaction.Data;
            if (!string.IsNullOrEmpty(data.Name))
            {
                command = typeCommands[data.Name];
            } 
            else if (!string.IsNullOrEmpty(data.CustomId))
            {
                string customId = data.CustomId;
                for (int index = 0; index < _messageComponentCommands.Count; index++)
                {
                    AppCommand componentCommand = _messageComponentCommands[index];
                    if (customId.StartsWith(componentCommand.Command))
                    {
                        command = componentCommand;
                        break;
                    }
                }
            }
            
            if (command == null)
            {
                return false;
            }
            
            command.HandleCommand(interaction);
            return true;
        }
    }
}