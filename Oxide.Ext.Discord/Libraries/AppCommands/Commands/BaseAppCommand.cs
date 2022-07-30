using Oxide.Core.Plugins;
using Oxide.Ext.Discord.Entities;
using Oxide.Ext.Discord.Entities.Applications;
using Oxide.Ext.Discord.Entities.Interactions;

namespace Oxide.Ext.Discord.Libraries.AppCommands.Commands
{
    /// <summary>
    /// Represents a Base Registered Application Command
    /// </summary>
    internal abstract class BaseAppCommand
    {
        internal Plugin Plugin;
        internal Snowflake AppId;
        internal readonly InteractionType Type;
        internal readonly string Callback;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="plugin">Plugin for the command</param>
        /// <param name="app"><see cref="DiscordApplication"/> for the command</param>
        /// <param name="type">Interaction type for the command</param>
        /// <param name="callback">Hook callback method name</param>
        protected BaseAppCommand(Plugin plugin, DiscordApplication app, InteractionType type, string callback)
        {
            Plugin = plugin;
            AppId = app.Id;
            Type = type;
            Callback = callback;
        }
        
        public abstract void HandleCommand(DiscordInteraction interaction);
        
        public bool IsForPlugin(Plugin plugin)
        {
            return Plugin == null || !Plugin.IsLoaded || Plugin == plugin;
        }
        
        internal void OnRemoved()
        {
            Plugin = null;
        }
    }
}