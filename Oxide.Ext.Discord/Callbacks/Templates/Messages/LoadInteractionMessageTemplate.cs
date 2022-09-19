using System.Threading.Tasks;
using Oxide.Ext.Discord.Entities.Interactions;
using Oxide.Ext.Discord.Interfaces.Callbacks.Async;
using Oxide.Ext.Discord.Libraries.Templates;
using Oxide.Ext.Discord.Libraries.Templates.Messages;
using Oxide.Ext.Discord.Pooling;

namespace Oxide.Ext.Discord.Callbacks.Templates.Messages
{
    internal class LoadInteractionMessageTemplate<TTemplate> : BaseAsyncCallback where TTemplate : BaseTemplate, new()
    {
        private BaseMessageTemplatesLibrary<TTemplate> _templates;
        private TemplateId _id;
        private DiscordInteraction _interaction;
        private IDiscordAsyncCallback<TTemplate> _callback;

        public static void Start(BaseMessageTemplatesLibrary<TTemplate> templates, TemplateId id, DiscordInteraction interaction, IDiscordAsyncCallback<TTemplate> callback)
        {
            LoadInteractionMessageTemplate<TTemplate> load = DiscordPool.Get<LoadInteractionMessageTemplate<TTemplate>>();
            load.Init(templates, id, interaction, callback);
            load.Run();
        }
        
        private void Init(BaseMessageTemplatesLibrary<TTemplate> templates, TemplateId id, DiscordInteraction interaction, IDiscordAsyncCallback<TTemplate> callback)
        {
            _templates = templates;
            _id = id;
            _interaction = interaction;
            _callback = callback;
        }

        protected override Task HandleCallback()
        {
            return _templates.HandleGetMessageTemplate(_id, _interaction, _callback);
        }

        protected override void EnterPool()
        {
            _templates = null;
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