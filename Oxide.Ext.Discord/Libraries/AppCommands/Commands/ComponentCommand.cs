using System;
using Oxide.Core.Plugins;
using Oxide.Ext.Discord.Entities;
using Oxide.Ext.Discord.Entities.Interactions;
using Oxide.Ext.Discord.Extensions;
using Oxide.Ext.Discord.Logging;

namespace Oxide.Ext.Discord.Libraries.AppCommands.Commands
{
    internal class ComponentCommand : BaseAppCommand
    {
        private readonly Action<DiscordInteraction> _callback;
        
        public ComponentCommand(Plugin plugin, Snowflake appId, AppCommandId customId, Action<DiscordInteraction> callback, ILogger logger) : base(plugin, appId, customId, logger)
        {
            _callback = callback;
        }

        protected override string GetCommandType() => "Component Command";

        protected override void RunCommand(DiscordInteraction interaction) => _callback(interaction);

        protected override string GetExceptionMessage() => $"An error occured during callback. Plugin: {Plugin?.PluginName()} Method: {_callback.Method.DeclaringType?.Name}.{_callback.Method.Name}";

        public override void LogDebug(DebugLogger logger)
        {
            base.LogDebug(logger);
            logger.AppendField("Command Name", CommandId.Command);
        }
    }
}