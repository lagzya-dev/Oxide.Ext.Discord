using Oxide.Ext.Discord.Data.Users;
using Oxide.Ext.Discord.Entities.Api;
using Oxide.Ext.Discord.Entities.Channels;
using Oxide.Ext.Discord.Entities.Users;
using Oxide.Ext.Discord.Logging;
using Oxide.Ext.Discord.Pooling;

namespace Oxide.Ext.Discord.Callbacks.Api.Entities
{
    internal class DmChannelMessageCreateCompletedCallback : BaseApiCompletedCallback
    {
        private DiscordChannel _channel;

        public static DmChannelMessageCreateCompletedCallback Create(DiscordClient client, DiscordChannel channel)
        {
            DmChannelMessageCreateCompletedCallback callback = DiscordPool.Get<DmChannelMessageCreateCompletedCallback>();
            callback.Init(client, channel);
            return callback;
        }

        private void Init(DiscordClient client, DiscordChannel channel)
        {
            base.Init(client);
            _channel = channel;
        }
        
        protected override void HandleCallback(RequestResponse response)
        {
            RequestErrorMessage error = response.Error?.DiscordError;
            if (error == null)
            {
                return;
            }

            UserData userData = _channel.UserData;
            if (userData == null)
            {
                return;
            }

            if (error.Code == 50007)
            {
                userData.SetDmBlock();
                DiscordUser user = userData.GetUser();
                Client.Logger.Debug("We're unable to send DM's to {0} ({1}). We are blocking attempts until {2}.", user.GetFullUserName, user.Id, userData.GetBlockedUntil());
                response.Error.SuppressErrorMessage();
            }
        }

        protected override void EnterPool()
        {
            base.EnterPool();
            _channel = null;
        }

        protected override void DisposeInternal()
        {
            DiscordPool.Free(this);
        }
    }
}