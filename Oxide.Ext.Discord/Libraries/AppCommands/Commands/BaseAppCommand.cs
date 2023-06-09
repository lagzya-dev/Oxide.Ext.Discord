using Oxide.Core.Plugins;
using Oxide.Ext.Discord.Entities;
using Oxide.Ext.Discord.Entities.Applications;
using Oxide.Ext.Discord.Entities.Interactions;
using Oxide.Ext.Discord.Extensions;
using Oxide.Ext.Discord.Interfaces.Logging;
using Oxide.Ext.Discord.Logging;

namespace Oxide.Ext.Discord.Libraries.AppCommands.Commands
{
    /// <summary>
    /// Represents a Base Registered Application Command
    /// </summary>
    internal abstract class BaseAppCommand : IDebugLoggable
    {
        internal Plugin Plugin;
        internal Snowflake AppId;
        internal readonly InteractionType Type;
        internal readonly string Callback;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="plugin">Plugin for the command</param>
        /// <param name="appId">ID of the <see cref="DiscordApplication"/> for the command</param>
        /// <param name="type">Interaction type for the command</param>
        /// <param name="callback">Hook callback method name</param>
        protected BaseAppCommand(Plugin plugin, Snowflake appId, InteractionType type, string callback)
        {
            Plugin = plugin;
            AppId = appId;
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

        protected abstract string GetCommandType();

        public virtual void LogDebug(DebugLogger logger)
        {
            logger.AppendField("Type", GetCommandType());
            logger.AppendFieldEnum("Interaction Type", Type);
            logger.AppendField("Plugin", Plugin.FullName());
            logger.AppendField("Callback", Callback);
        }
    }
}