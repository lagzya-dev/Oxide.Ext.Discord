using Oxide.Core.Plugins;
using Oxide.Ext.Discord.Entities.Interactions;
using Oxide.Ext.Discord.Hooks;

namespace Oxide.Ext.Discord.Libraries.AppCommand
{
    internal class AppCommand
    {
        internal Plugin Plugin;
        internal readonly InteractionType Type;
        internal readonly string Command;
        private readonly string _callback;

        public AppCommand(Plugin plugin, InteractionType type, string command, string callback)
        {
            Plugin = plugin;
            Command = command;
            _callback = callback;
            Type = type;
        }
        
        public void HandleCommand(DiscordInteraction interaction)
        {
            DiscordHook.CallPluginHook(Plugin, _callback, interaction);
        }

        internal void OnRemoved()
        {
            Plugin = null;
        }
    }
}