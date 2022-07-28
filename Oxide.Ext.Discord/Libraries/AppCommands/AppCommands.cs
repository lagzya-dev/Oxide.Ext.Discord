using System.Collections.Generic;
using Oxide.Core.Plugins;
using Oxide.Ext.Discord.Entities.Interactions;
using Oxide.Ext.Discord.Libraries.AppCommands.Commands;
using Oxide.Ext.Discord.Logging;
using Oxide.Plugins;

namespace Oxide.Ext.Discord.Libraries.AppCommands
{
    internal class AppCommands
    {
        public bool IsEmpty => _commands.Count == 0 && _componentCommands.Count == 0;
        private readonly Hash<InteractionType, Dictionary<AppCommandId, AppCommand>> _commands = new Hash<InteractionType, Dictionary<AppCommandId, AppCommand>>();
        private readonly List<ComponentCommand> _componentCommands = new List<ComponentCommand>();
        private readonly ILogger _logger;

        public AppCommands(ILogger logger) 
        {
            _logger = logger;
        }

        public Dictionary<AppCommandId, AppCommand> GetCommandsByType(InteractionType type)
        {
            return _commands[type];
        }
        
        public AppCommand GetCommandById(InteractionType type, AppCommandId id)
        {
            if (_commands[type]?.TryGetValue(id, out AppCommand command) ?? false)
            {
                return command;
            }
            
            return null;
        }
        
        public ComponentCommand GetComponentCommandById(Plugin plugin, string customId)
        {
            foreach (ComponentCommand command in _componentCommands)
            {
                if (command.IsForPlugin(plugin) && command.CustomId == customId)
                {
                    return command;
                }
            }

            return null;
        }
        
        public Dictionary<AppCommandId, AppCommand> GetOrAddCommandsByType(InteractionType type)
        {
            Dictionary<AppCommandId, AppCommand> commands = _commands[type];
            if (commands == null)
            {
                commands = new Dictionary<AppCommandId, AppCommand>(AppCommandId.AppCommandIdComparer);
                _commands[type] = commands;
            }

            return commands;
        }

        public void AddAppCommand(AppCommand command)
        {
            Dictionary<AppCommandId, AppCommand> commands = GetOrAddCommandsByType(command.Type);
            commands[command.Command] = command;
        }

        public bool RemoveAppCommand(AppCommand command)
        {
            Dictionary<AppCommandId, AppCommand> commands = GetCommandsByType(command.Type);
            if (commands == null)
            {
                return false;
            }

            if (!commands.Remove(command.Command))
            {
                return false;
            }
            
            command.OnRemoved();
            if (commands.Count == 0)
            {
                _commands.Remove(command.Type);
            }
            return true;
        }

        public void AddComponentCommand(ComponentCommand command)
        {
            for (int index = _componentCommands.Count - 1; index >= 0; index--)
            {
                ComponentCommand componentCommand = _componentCommands[index];
                if (componentCommand.CustomId == command.CustomId)
                {
                    if (componentCommand.IsForPlugin(command.Plugin))
                    {
                        _logger.Warning("{0} has replaced the '{1}' ({2}) discord message component command previously registered by {3}", command.Plugin.Name, command.CustomId, InteractionType.MessageComponent, componentCommand.Plugin?.Name);
                    }
                    _componentCommands.RemoveAt(index);
                    componentCommand.OnRemoved();
                }
            }
            
            _logger.Debug("Adding App Message Component Command For: {0} Command: {1} Callback: {2}", command.Plugin.Name, command.CustomId, command.Callback);
            _componentCommands.Add(command);
        }

        public bool RemoveComponentCommand(ComponentCommand command)
        {
            for (int index = _componentCommands.Count - 1; index >= 0; index--)
            {
                ComponentCommand componentCommand = _componentCommands[index];
                if (componentCommand.IsForPlugin(command.Plugin) && componentCommand.CustomId == command.CustomId)
                {
                    _componentCommands.RemoveAt(index);
                    return true;
                }
            }

            return false;
        }

        public IEnumerable<AppCommand> GetCommandsForPlugin(Plugin plugin)
        {
            foreach (Dictionary<AppCommandId, AppCommand> types in _commands.Values)
            {
                foreach (AppCommand command in types.Values)
                {
                    if (command.IsForPlugin(plugin))
                    {
                        yield return command;
                    }
                }
            }
        }

        public IEnumerable<ComponentCommand> GetComponentCommandsForPlugin(Plugin plugin)
        {
            foreach (ComponentCommand command in _componentCommands)
            {
                if (command.IsForPlugin(plugin))
                {
                    yield return command;
                }
            }
        }

        public BaseAppCommand GetInteractionCommand(DiscordInteraction interaction)
        {
            if (interaction.Type == InteractionType.ApplicationCommand || interaction.Type == InteractionType.ApplicationCommandAutoComplete)
            {
                return GetCommandById(interaction.Type, interaction.GetCommandId());
            }
            
            if (interaction.Type == InteractionType.MessageComponent || interaction.Type == InteractionType.ModalSubmit)
            {
                for (int index = 0; index < _componentCommands.Count; index++)
                {
                    ComponentCommand command = _componentCommands[index];
                    if (interaction.Data.CustomId.StartsWith(command.CustomId))
                    {
                        return command;
                    }
                }
            }

            return null;
        }
    }
}