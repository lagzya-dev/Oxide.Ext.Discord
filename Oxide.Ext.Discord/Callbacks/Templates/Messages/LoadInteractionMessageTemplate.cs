using System.Threading.Tasks;
using Oxide.Ext.Discord.Entities.Interactions;
using Oxide.Ext.Discord.Interfaces.Callbacks.Async;
using Oxide.Ext.Discord.Libraries.Templates;
using Oxide.Ext.Discord.Libraries.Templates.Messages;
using Oxide.Ext.Discord.Pooling;

namespace Oxide.Ext.Discord.Callbacks.Templates.Messages
{
    internal class LoadInteractionMessageTemplate : BaseAsyncCallback
    {
        private readonly DiscordMessageTemplates _templates = DiscordExtension.DiscordMessageTemplates;
        private TemplateId _id;
        private DiscordInteraction _interaction;
        private IDiscordAsyncCallback<DiscordMessageTemplate> _callback;

        public static void Start(TemplateId id, DiscordInteraction interaction, IDiscordAsyncCallback<DiscordMessageTemplate> callback)
        {
            LoadInteractionMessageTemplate load = DiscordPool.Get<LoadInteractionMessageTemplate>();
            load.Init(id, interaction, callback);
            load.Run();
        }
        
        private void Init(TemplateId id, DiscordInteraction interaction, IDiscordAsyncCallback<DiscordMessageTemplate> callback)
        {
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