using System;
using Oxide.Core.Plugins;
using Oxide.Ext.Discord.Callbacks.Libraries;
using Oxide.Ext.Discord.Entities;
using Oxide.Ext.Discord.Entities.Applications;
using Oxide.Ext.Discord.Entities.Interactions;
using Oxide.Ext.Discord.Extensions;
using Oxide.Ext.Discord.Interfaces.Logging;
using Oxide.Ext.Discord.Logging;
using Oxide.Ext.Discord.Plugins;

namespace Oxide.Ext.Discord.Libraries.AppCommands.Commands
{
    /// <summary>
    /// Represents a Base Registered Application Command
    /// </summary>
    internal abstract class BaseAppCommand : IDebugLoggable
    {
        internal Plugin Plugin;
        internal readonly PluginId PluginId;
        internal readonly Snowflake AppId;
        internal readonly AppCommandId CommandId;
        protected readonly ILogger _logger;
        public bool IsValid => Plugin != null && Plugin.IsLoaded;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="plugin">Plugin for the command</param>
        /// <param name="appId">ID of the <see cref="DiscordApplication"/> for the command</param>
        /// <param name="type">Interaction type for the command</param>
        protected BaseAppCommand(Plugin plugin, Snowflake appId, AppCommandId commandId, ILogger logger)
        {
            Plugin = plugin;
            PluginId = plugin.Id();
            AppId = appId;
            CommandId = commandId;
            _logger = logger;
        }

        public void HandleCommand(DiscordInteraction interaction)
        {
            if (ThreadEx.IsMain)
            {
                HandleCommandInternal(interaction);
            }
            else
            {
                AppCommandCallback.Start(this, interaction);
            }
        }

        internal void HandleCommandInternal(DiscordInteraction interaction)
        {
            try
            {
                RunCommand(interaction);
            }
            catch (Exception ex)
            {
                _logger.Exception(GetExceptionMessage(), ex);
            }
        }

        public bool IsForPlugin(PluginId id) => Plugin == null || !Plugin.IsLoaded || PluginId == id;

        protected abstract string GetCommandType();

        protected abstract string GetExceptionMessage();

        protected abstract void RunCommand(DiscordInteraction interaction);

        public virtual void LogDebug(DebugLogger logger)
        {
            logger.AppendField("Type", GetCommandType());
            logger.AppendField("Plugin", Plugin?.FullName() ?? "Unknown Plugin");
            logger.AppendField("Command", CommandId.ToString());

        }
    }
}