using Oxide.Ext.Discord.Entities.Api;
using Oxide.Ext.Discord.Entities.Channels;
using Oxide.Ext.Discord.Libraries.Pooling;

namespace Oxide.Ext.Discord.Callbacks.Api.Completed
{
    internal class DmChannelMessageCreateCompletedCallback : BaseApiCompletedCallback
    {
        private DiscordChannel _channel;

        public static DmChannelMessageCreateCompletedCallback Create(DiscordClient client, DiscordChannel channel)
        {
            DmChannelMessageCreateCompletedCallback callback = DiscordPool.Internal.Get<DmChannelMessageCreateCompletedCallback>();
            callback.Init(client);
            callback._channel = channel;
            return callback;
        }
        
        protected override void HandleCallback(RequestResponse response)
        {
           
        }

        protected override void EnterPool()
        {
            base.EnterPool();
            _channel = null;
        }
    }
}