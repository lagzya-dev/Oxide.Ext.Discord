using Oxide.Core.Plugins;
using Oxide.Ext.Discord.Entities;
using Oxide.Ext.Discord.Entities.Interactions;
using Oxide.Ext.Discord.Hooks;

namespace Oxide.Ext.Discord.Libraries.AppCommands.Commands
{
    internal class AutoCompleteCommand : AppCommand
    {
        public AutoCompleteCommand(Plugin plugin, Snowflake appId, AppCommandId command, string callback) : base(plugin, appId, InteractionType.ApplicationCommandAutoComplete, command, callback) { }
        
        protected override string GetCommandType() => "AutoComplete Command";

        public override void HandleCommand(DiscordInteraction interaction)
        {
            DiscordHook.CallPluginHook(Plugin, Callback, interaction, interaction.Focused);
        }
    }
}