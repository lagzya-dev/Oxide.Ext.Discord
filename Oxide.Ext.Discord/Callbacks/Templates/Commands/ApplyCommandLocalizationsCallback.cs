using System.Threading.Tasks;
using Oxide.Ext.Discord.Entities.Interactions.ApplicationCommands;
using Oxide.Ext.Discord.Libraries.Templates;
using Oxide.Ext.Discord.Libraries.Templates.Commands;
using Oxide.Ext.Discord.Pooling;
using Oxide.Ext.Discord.Promise;

namespace Oxide.Ext.Discord.Callbacks.Templates.Commands
{
    internal class ApplyCommandLocalizationsCallback : BaseAsyncCallback
    {
        private readonly DiscordCommandLocalizations _localizations = DiscordExtension.DiscordCommandLocalizations;
        private TemplateId _id;
        private CommandCreate _create;
        private DiscordPromise _promise;

        public static void Start(TemplateId id, CommandCreate create, DiscordPromise promise)
        {
            ApplyCommandLocalizationsCallback load = DiscordPool.Get<ApplyCommandLocalizationsCallback>();
            load.Init(id, create, promise);
            load.Run();
        }

        private void Init(TemplateId id, CommandCreate create, DiscordPromise promise)
        {
            _id = id;
            _create = create;
            _promise = promise;
        }

        protected override Task HandleCallback()
        {
            return _localizations.HandleApplyCommandLocalizationsAsync(_id, _create, _promise);
        }

        protected override void EnterPool()
        {
            _id = default(TemplateId);
            _create = null;
            _promise = null;
        }
    }
}