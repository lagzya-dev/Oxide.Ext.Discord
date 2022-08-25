using System;
using System.Threading.Tasks;
using Oxide.Ext.Discord.Entities.Api;
using Oxide.Ext.Discord.Json.Serialization;
using Oxide.Ext.Discord.Pooling;
using Oxide.Ext.Discord.Rest.Requests;

namespace Oxide.Ext.Discord.Callbacks.Api
{
    internal class ApiSuccessCallback<T> : BaseApiCallback
    {
        private Action<T> _onSuccess;
        private T _data;

        public async Task Init(Request<T> request, RequestResponse response)
        {
            base.Init(request);
            _onSuccess = request.OnSuccess;

            DiscordJsonReader reader = DiscordPool.Get<DiscordJsonReader>();
            await reader.CopyFromAsync(response.Content).ConfigureAwait(false);
            //DiscordExtension.GlobalLogger.Verbose($"{nameof(ApiSuccessCallback<T>)}.{nameof(Init)} Body: {await reader.ReadAsStringAsync()}");
            _data = await reader.DeserializeAsync<T>(Client.Bot.JsonSerializer).ConfigureAwait(false);
            reader.Dispose();
        }

        protected override void HandleApiCallback()
        {
            _onSuccess.Invoke(_data);
        }

        ///<inheritdoc/>
        protected override void DisposeInternal()
        {
            DiscordPool.Free(this);
        }
        
        ///<inheritdoc/>
        protected override void EnterPool()
        {
            base.EnterPool();
            _data = default(T);
            _onSuccess = null;
        }
    }
}