using System;
using Oxide.Core.Plugins;
using Oxide.Ext.Discord.Entities;
using Oxide.Ext.Discord.Entities.Interactions;
using Oxide.Ext.Discord.Extensions;
using Oxide.Ext.Discord.Logging;

namespace Oxide.Ext.Discord.Libraries.AppCommands.Commands
{
    internal class AutoCompleteCommand : BaseAppCommand
    {
        private readonly Action<DiscordInteraction, InteractionDataOption> _callback;
        
        public AutoCompleteCommand(Plugin plugin, Snowflake appId, AppCommandId command,  Action<DiscordInteraction, InteractionDataOption> callback, ILogger logger) : base(plugin, appId, command, logger)
        {
            _callback = callback;
        }
        
        protected override string GetCommandType() => "AutoComplete Command";
        
        protected override void RunCommand(DiscordInteraction interaction) => _callback(interaction, interaction.Focused);

        protected override string GetExceptionMessage() => $"An error occured during callback. Plugin: {Plugin?.PluginName()} Method: {_callback.Method.DeclaringType.Name}.{_callback.Method.Name}";
    }
}