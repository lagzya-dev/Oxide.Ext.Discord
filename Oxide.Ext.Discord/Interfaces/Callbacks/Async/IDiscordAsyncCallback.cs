using System;

namespace Oxide.Ext.Discord.Interfaces.Callbacks.Async
{
    public interface IDiscordAsyncCallback<T> : IDisposable
    {
        IDiscordAsyncCallback<T> OnSuccess(Action<T> complete);
        void InvokeSuccess(T data);
    }
}