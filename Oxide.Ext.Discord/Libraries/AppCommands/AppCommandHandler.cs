using System.Collections.Generic;
using Oxide.Core.Plugins;
using Oxide.Ext.Discord.Extensions;
using Oxide.Ext.Discord.Libraries.AppCommands.Commands;
using Oxide.Ext.Discord.Logging;
using Oxide.Ext.Discord.Plugins;
using Oxide.Plugins;

namespace Oxide.Ext.Discord.Libraries.AppCommands
{
    internal class AppCommandHandler
    {
        public bool IsEmpty => _commands.Count == 0;
        private readonly Hash<AppCommandId, BaseAppCommand> _commands = new Hash<AppCommandId, BaseAppCommand>();
        private readonly ILogger _logger;

        public AppCommandHandler(ILogger logger) 
        {
            _logger = logger;
        }

        public BaseAppCommand GetCommandById(AppCommandId id) => _commands[id];

        public void AddAppCommand(BaseAppCommand command)
        {
            _commands[command.CommandId] = command;
            _logger.Verbose($"{nameof(AppCommandHandler)}.{nameof(AddAppCommand)} Command: {{0}}", command.CommandId);
        }

        public bool RemoveAppCommand(BaseAppCommand command)
        {
            _logger.Verbose($"{nameof(AppCommandHandler)}.{nameof(RemoveAppCommand)} Command: {{0}}", command.CommandId);
            return _commands.Remove(command.CommandId);
        }

        public IEnumerable<BaseAppCommand> GetCommandsForPlugin(Plugin plugin)
        {
            PluginId id = plugin.Id();
            foreach (BaseAppCommand command in _commands.Values)
            {
                if (command.IsForPlugin(id))
                {
                    yield return command;
                }
            }
        }

        public IEnumerable<BaseAppCommand> GetCommands()
        {
            foreach (BaseAppCommand command in _commands.Values)
            {
                yield return command;
            }
        }
    }
}