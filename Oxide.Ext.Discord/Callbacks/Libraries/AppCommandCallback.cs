using Oxide.Ext.Discord.Entities.Interactions;
using Oxide.Ext.Discord.Libraries.AppCommands.Commands;
using Oxide.Ext.Discord.Libraries.Pooling;

namespace Oxide.Ext.Discord.Callbacks.Libraries
{
    internal class AppCommandCallback : BaseNextTickCallback
    {
        private BaseAppCommand _command;
        private DiscordInteraction _interaction;
        
        public static void Start(BaseAppCommand command, DiscordInteraction interaction)
        {
            AppCommandCallback sub = DiscordPool.Internal.Get<AppCommandCallback>();
            sub.Init(command, interaction);
            sub.Run();
        }
        
        private void Init(BaseAppCommand command, DiscordInteraction interaction)
        {
            _command = command;
            _interaction = interaction;
        }
        
        protected override void HandleCallback()
        {
            _command.HandleCommandInternal(_interaction);
        }

        protected override void EnterPool()
        {
            _command = null;
            _interaction = null;
        }
    }
}