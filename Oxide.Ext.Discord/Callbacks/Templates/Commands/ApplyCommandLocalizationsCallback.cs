using System.Threading.Tasks;
using Oxide.Ext.Discord.Entities.Interactions.ApplicationCommands;
using Oxide.Ext.Discord.Extensions;
using Oxide.Ext.Discord.Interfaces.Promises;
using Oxide.Ext.Discord.Libraries.Pooling;
using Oxide.Ext.Discord.Libraries.Templates;
using Oxide.Ext.Discord.Libraries.Templates.Commands;

namespace Oxide.Ext.Discord.Callbacks.Templates.Commands
{
    internal class ApplyCommandLocalizationsCallback : BaseAsyncCallback
    {
        private readonly DiscordCommandLocalizations _localizations = DiscordExtension.DiscordCommandLocalizations;
        private TemplateId _id;
        private CommandCreate _create;
        private IPendingPromise _promise;

        public static void Start(TemplateId id, CommandCreate create, IPendingPromise promise)
        {
            ApplyCommandLocalizationsCallback load = DiscordPool.Internal.Get<ApplyCommandLocalizationsCallback>();
            load.Init(id, create, promise);
            load.Run();
        }

        private void Init(TemplateId id, CommandCreate create, IPendingPromise promise)
        {
            _id = id;
            _create = create;
            _promise = promise;
        }

        protected override ValueTask HandleCallback()
        {
            _localizations.HandleApplyCommandLocalizationsAsync(_id, _create, _promise);
            return new ValueTask();
        }
        
        protected override string GetExceptionMessage()
        {
            return $"Plugin: {_id.PluginId.FullName()} Name: {_id.TemplateName} Language: {_id.Language}";
        }

        protected override void EnterPool()
        {
            _id = default(TemplateId);
            _create = null;
            _promise = null;
        }
    }
}