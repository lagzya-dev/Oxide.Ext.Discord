using Oxide.Core.Plugins;
using Oxide.Ext.Discord.Cache;
using Oxide.Ext.Discord.Entities.Applications;
using Oxide.Ext.Discord.Entities.Interactions;
using Oxide.Ext.Discord.Extensions;
using Oxide.Ext.Discord.Hooks;
using Oxide.Ext.Discord.Logging;

namespace Oxide.Ext.Discord.Libraries.AppCommands.Commands
{
    internal class ComponentCommand : BaseAppCommand
    {
        public readonly string CustomId;
        
        public ComponentCommand(Plugin plugin, DiscordApplication app, InteractionType type, string customId, string callback) : base(plugin, app, type, callback)
        {
            CustomId = customId;
        }

        public override void HandleCommand(DiscordInteraction interaction)
        {
            DiscordHook.CallPluginHook(Plugin, Callback, interaction);
        }

        public override void LogDebug(DebugLogger logger)
        {
            logger.AppendField("Type", "Component Command");
            logger.AppendField("Command Name", CustomId);
            logger.AppendField("Interaction Type", EnumCache<InteractionType>.Instance.ToString(Type));
            logger.AppendField("Plugin", Plugin.FullName());
        }
    }
}