using Oxide.Core.Plugins;
using Oxide.Ext.Discord.Entities.Applications;
using Oxide.Ext.Discord.Entities.Interactions;
using Oxide.Ext.Discord.Hooks;

namespace Oxide.Ext.Discord.Libraries.AppCommands.Commands
{
    internal class AppCommand : BaseAppCommand
    {
        internal readonly AppCommandId Command;

        public AppCommand(Plugin plugin, DiscordApplication app, InteractionType type, AppCommandId command, string callback) : base(plugin, app, type, callback)
        {
            Command = command;
        }
        
        public override void HandleCommand(DiscordInteraction interaction)
        {
            DiscordHook.CallPluginHook(Plugin, Callback, interaction, interaction.Parsed);
        }
    }
}