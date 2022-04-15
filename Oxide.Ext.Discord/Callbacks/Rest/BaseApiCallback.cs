using Oxide.Ext.Discord.Rest.Requests;

namespace Oxide.Ext.Discord.Callbacks.Rest
{
    internal abstract class BaseApiCallback : BaseCallback
    {
        protected Request Request;
        protected DiscordClient Client;

        protected void Init(Request request, DiscordClient client)
        {
            Request = request;
            Client = client;
        }

        protected override void EnterPool()
        {
            Request = null;
            Client = null;
        }
    }
}