using Oxide.Core.Plugins;
using Oxide.Ext.Discord.Entities.Channels;
using Oxide.Ext.Discord.Entities.Messages;
using Oxide.Ext.Discord.Hooks;

namespace Oxide.Ext.Discord.Libraries.Command
{
    /// <summary>
    /// Sourced from Command.cs of OxideMod (https://github.com/OxideMod/Oxide.Rust/blob/develop/src/Libraries/Command.cs#L76)
    /// </summary>
    internal abstract class BaseCommand
    {
        internal readonly string Name;
        private readonly string _hook;
        internal Plugin Plugin;

        protected BaseCommand(Plugin plugin, string name, string hook)
        {
            Name = name;
            _hook = hook;
            Plugin = plugin;
        }
        
        public void HandleCommand(DiscordMessage message, string name, string[] args)
        {
            DiscordHook.CallPluginHook(Plugin, _hook, message, name, args);
        }

        /// <summary>
        /// Returns if a command can run.
        /// They can only run for the client that they were created for.
        /// </summary>
        /// <param name="client">Client to compare against</param>
        /// <returns>True if same bot client; false otherwise</returns>
        public bool CanRun(BotClient client)
        {
            return client != null && DiscordClient.Clients[Plugin.Name]?.Bot == client;
        }

        public abstract bool CanHandle(DiscordMessage message, DiscordChannel channel);

        internal void OnRemoved()
        {
            Plugin = null;
        }
    }
}