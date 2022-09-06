using System.Threading.Tasks;
using Oxide.Core.Plugins;
using Oxide.Ext.Discord.Entities.Interactions;
using Oxide.Ext.Discord.Interfaces.Callbacks.Async;
using Oxide.Ext.Discord.Libraries.Templates;
using Oxide.Ext.Discord.Libraries.Templates.Modals;
using Oxide.Ext.Discord.Pooling;

namespace Oxide.Ext.Discord.Callbacks.Templates.Modals
{
    internal class LoadModalTemplateCallback : BaseAsyncCallback
    {
        private readonly DiscordModalTemplates _templates = DiscordExtension.DiscordModalTemplates;
        private TemplateId _id;
        private DiscordInteraction _interaction;
        private IDiscordAsyncCallback<DiscordModalTemplate> _callback;

        public static void Start(TemplateId id, DiscordInteraction interaction, IDiscordAsyncCallback<DiscordModalTemplate> callback)
        {
            LoadModalTemplateCallback load = DiscordPool.Get<LoadModalTemplateCallback>();
            load.Init(id, interaction, callback);
            load.Run();
        }

        private void Init(TemplateId id, DiscordInteraction interaction, IDiscordAsyncCallback<DiscordModalTemplate> callback)
        {
            _id = id;
            _interaction = interaction;
            _callback = callback;
        }

        protected override Task HandleCallback()
        {
            return _templates.HandleGetModalTemplate(_id, _interaction, _callback);
        }

        protected override void EnterPool()
        {
            _id = default(TemplateId);
            _interaction = null;
            _callback = null;
        }

        protected override void DisposeInternal()
        {
            _callback?.Dispose();
            DiscordPool.Free(this);
        }
    }
}