using Oxide.Core.Plugins;
using Oxide.Ext.Discord.Entities.Applications;
using Oxide.Ext.Discord.Entities.Interactions;
using Oxide.Ext.Discord.Hooks;

namespace Oxide.Ext.Discord.Libraries.AppCommands.Commands
{
    internal class AutoCompleteCommand : AppCommand
    {
        internal readonly string ArgumentName;

        public AutoCompleteCommand(Plugin plugin, DiscordApplication app, AppCommandId command, string argumentName, string callback) : base(plugin, app, InteractionType.ApplicationCommandAutoComplete, command, callback)
        {
            ArgumentName = argumentName;
        }

        public override void HandleCommand(DiscordInteraction interaction)
        {
            InteractionDataOption focused = interaction.GetFocusedOption();
            DiscordHook.CallPluginHook(Plugin, Callback, interaction, focused);
        }
    }
}