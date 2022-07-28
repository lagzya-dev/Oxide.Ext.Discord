using Oxide.Core.Plugins;
using Oxide.Ext.Discord.Entities;
using Oxide.Ext.Discord.Entities.Applications;
using Oxide.Ext.Discord.Entities.Interactions;

namespace Oxide.Ext.Discord.Libraries.AppCommands.Commands
{
    public abstract class BaseAppCommand
    {
        internal Plugin Plugin;
        internal Snowflake AppId;
        internal readonly InteractionType Type;
        internal readonly string Callback;

        protected BaseAppCommand(Plugin plugin, DiscordApplication app, InteractionType type, string callback)
        {
            Plugin = plugin;
            AppId = app.Id;
            Type = type;
            Callback = callback;
        }

        public virtual bool CanHandle(DiscordInteraction interaction)
        {
            return true;
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