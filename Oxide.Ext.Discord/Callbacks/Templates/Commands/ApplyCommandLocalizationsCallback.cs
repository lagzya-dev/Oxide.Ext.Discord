using System.Threading.Tasks;
using Oxide.Ext.Discord.Entities.Interactions.ApplicationCommands;
using Oxide.Ext.Discord.Interfaces.Callbacks.Async;
using Oxide.Ext.Discord.Libraries.Templates;
using Oxide.Ext.Discord.Libraries.Templates.Commands;
using Oxide.Ext.Discord.Pooling;

namespace Oxide.Ext.Discord.Callbacks.Templates.Commands
{
    internal class ApplyCommandLocalizationsCallback : BaseAsyncCallback
    {
        private readonly DiscordCommandLocalizations _localizations = DiscordExtension.DiscordCommandLocalizations;
        private TemplateId _id;
        private CommandCreate _create;
        private IDiscordAsyncCallback _callback;

        public static void Start(TemplateId id, CommandCreate create, IDiscordAsyncCallback callback)
        {
            ApplyCommandLocalizationsCallback load = DiscordPool.Get<ApplyCommandLocalizationsCallback>();
            load.Init(id, create, callback);
            load.Run();
        }

        private void Init(TemplateId id, CommandCreate create, IDiscordAsyncCallback callback)
        {
            _id = id;
            _create = create;
            _callback = callback;
        }

        protected override Task HandleCallback()
        {
            return _localizations.HandleApplyCommandLocalizationsAsync(_id, _create, _callback);
        }

        protected override void EnterPool()
        {
            _id = default(TemplateId);
            _create = null;
            _callback = null;
        }

        protected override void DisposeInternal()
        {
            _callback?.Dispose();
            DiscordPool.Free(this);
        }
    }
}