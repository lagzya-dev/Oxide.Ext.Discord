using System.Collections.Generic;
using Oxide.Core.Plugins;
using Oxide.Ext.Discord.Entities.Interactions;
using Oxide.Ext.Discord.Libraries.AppCommands.Commands;
using Oxide.Ext.Discord.Logging;
using Oxide.Plugins;

namespace Oxide.Ext.Discord.Libraries.AppCommands.Handlers
{
    internal class MessageComponentHandler
    {
        public bool IsEmpty => _commands.Count == 0;
        private readonly Hash<InteractionType, List<ComponentCommand>> _commands = new Hash<InteractionType, List<ComponentCommand>>();
        private readonly ILogger _logger;

        public MessageComponentHandler(ILogger logger)
        {
            _logger = logger;
        }

        public List<ComponentCommand> GetCommandsByType(InteractionType type)
        {
            return _commands[type];
        }
        
        public List<ComponentCommand> GetOrAddCommandsByType(InteractionType type)
        {
            List<ComponentCommand> commands = _commands[type];
            if (commands == null)
            {
                commands = new List<ComponentCommand>();
                _commands[type] = commands;
            }

            return commands;
        }
        
        public ComponentCommand GetCommandById(Plugin plugin, InteractionType type, string customId)
        {
            List<ComponentCommand> commands = GetCommandsByType(type);
            if (commands == null)
            {
                return null;
            }

            for (int index = 0; index < commands.Count; index++)
            {
                ComponentCommand command = commands[index];
                if (command.IsForPlugin(plugin) && command.CustomId == customId)
                {
                    return command;
                }
            }

            return null;
        }
        
        public void AddComponentCommand(ComponentCommand command)
        {
            List<ComponentCommand> commands = GetOrAddCommandsByType(command.Type);
            
            for (int index = commands.Count - 1; index >= 0; index--)
            {
                ComponentCommand componentCommand = commands[index];
                if (componentCommand.CustomId == command.CustomId)
                {
                    if (componentCommand.IsForPlugin(command.Plugin))
                    {
                        _logger.Warning("{0} has replaced the '{1}' ({2}) discord message component command previously registered by {3}", command.Plugin.Name, command.CustomId, InteractionType.MessageComponent, componentCommand.Plugin?.Name);
                    }
                    commands.RemoveAt(index);
                    componentCommand.OnRemoved();
                }
            }
            
            _logger.Debug("Adding App Message Component Command For: {0} Command: {1} Callback: {2}", command.Plugin.Name, command.CustomId, command.Callback);
            commands.Add(command);
        }

        public bool RemoveComponentCommand(ComponentCommand command)
        {
            List<ComponentCommand> commands = GetCommandsByType(command.Type);
            if (commands == null)
            {
                return false;
            }
            
            for (int index = commands.Count - 1; index >= 0; index--)
            {
                ComponentCommand componentCommand = commands[index];
                if (componentCommand.IsForPlugin(command.Plugin) && componentCommand.CustomId == command.CustomId)
                {
                    commands.RemoveAt(index);
                    return true;
                }
            }

            if (commands.Count == 0)
            {
                _commands.Remove(command.Type);
            }

            return false;
        }
        
        public IEnumerable<ComponentCommand> GetCommandsForPlugin(Plugin plugin)
        {
            foreach (List<ComponentCommand> commands in _commands.Values)
            {
                foreach (ComponentCommand command in commands)
                {
                    if (command.IsForPlugin(plugin))
                    {
                        yield return command;
                    }
                }
            }
        }

        public BaseAppCommand GetInteractionCommand(DiscordInteraction interaction)
        {
            List<ComponentCommand> commands = GetCommandsByType(interaction.Type);
            if (commands == null)
            {
                return null;
            }

            for (int index = 0; index < commands.Count; index++)
            {
                ComponentCommand command = commands[index];
                if (interaction.Data.CustomId.StartsWith(command.CustomId))
                {
                    return command;
                }
            }

            return null;
        }
    }
}