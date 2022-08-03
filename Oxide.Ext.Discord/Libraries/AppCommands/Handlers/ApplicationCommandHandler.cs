using System.Collections.Generic;
using Oxide.Core.Plugins;
using Oxide.Ext.Discord.Entities.Interactions;
using Oxide.Ext.Discord.Libraries.AppCommands.Commands;
using Oxide.Ext.Discord.Logging;
using Oxide.Plugins;

namespace Oxide.Ext.Discord.Libraries.AppCommands.Handlers
{
    internal class ApplicationCommandHandler
    {
        public bool IsEmpty => _commands.Count == 0;
        private readonly Hash<InteractionType, Dictionary<AppCommandId, AppCommand>> _commands = new Hash<InteractionType, Dictionary<AppCommandId, AppCommand>>();
        private readonly ILogger _logger;

        public ApplicationCommandHandler(ILogger logger) 
        {
            _logger = logger;
        }

        public Dictionary<AppCommandId, AppCommand> GetCommandsByType(InteractionType type)
        {
            return _commands[type];
        }
        
        public AppCommand GetCommandById(InteractionType type, AppCommandId id)
        {
            _logger.Verbose($"{nameof(ApplicationCommandHandler)}.{nameof(GetCommandById)} Type: {{0}} ID: {{1}}", type, id);
            if (_commands[type]?.TryGetValue(id, out AppCommand command) ?? false)
            {
                return command;
            }

            foreach (KeyValuePair<InteractionType,Dictionary<AppCommandId,AppCommand>> pair in _commands)
            {
                foreach (KeyValuePair<AppCommandId,AppCommand> appCommand in pair.Value)
                {
                    _logger.Verbose("Registered Commands. Type: {0} CommandId: {1} Plugin: {2} Callback {3}", pair.Key, appCommand.Key, appCommand.Value.Plugin.Name, appCommand.Value.Callback);
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
            _logger.Verbose($"{nameof(ApplicationCommandHandler)}.{nameof(AddAppCommand)} Count: {{0}}", commands.Count);
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
            _logger.Verbose($"{nameof(ApplicationCommandHandler)}.{nameof(RemoveAppCommand)} Count: {{0}}", commands.Count);
            return true;
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
        
        public BaseAppCommand GetInteractionCommand(DiscordInteraction interaction)
        {
            return GetCommandById(interaction.Type, interaction.GetCommandId());
        }
    }
}