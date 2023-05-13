using Oxide.Core.Plugins;
using Oxide.Ext.Discord.Entities.Applications;
using Oxide.Ext.Discord.Entities.Interactions;
using Oxide.Ext.Discord.Hooks;

namespace Oxide.Ext.Discord.Libraries.AppCommands.Commands
{
    internal class AutoCompleteCommand : AppCommand
    {
        public AutoCompleteCommand(Plugin plugin, DiscordApplication app, AppCommandId command, string callback) : base(plugin, app, InteractionType.ApplicationCommandAutoComplete, command, callback) { }
        
        protected override string GetCommandType() => "AutoComplete Command";

        public override void HandleCommand(DiscordInteraction interaction)
        {
            DiscordHook.CallPluginHook(Plugin, Callback, interaction, interaction.Focused);
        }
    }
}