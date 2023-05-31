using Oxide.Core.Plugins;
using Oxide.Ext.Discord.Entities;
using Oxide.Ext.Discord.Entities.Interactions;
using Oxide.Ext.Discord.Hooks;
using Oxide.Ext.Discord.Logging;

namespace Oxide.Ext.Discord.Libraries.AppCommands.Commands
{
    internal class ComponentCommand : BaseAppCommand
    {
        public readonly string CustomId;
        
        public ComponentCommand(Plugin plugin, Snowflake appId, InteractionType type, string customId, string callback) : base(plugin, appId, type, callback)
        {
            CustomId = customId;
        }

        public override void HandleCommand(DiscordInteraction interaction)
        {
            DiscordHook.CallPluginHook(Plugin, Callback, interaction);
        }

        protected override string GetCommandType() => "Component Command";

        public override void LogDebug(DebugLogger logger)
        {
            base.LogDebug(logger);
            logger.AppendField("Command Name", CustomId);
        }
    }
}