using System;

namespace Oxide.Ext.Discord.Interfaces.Callbacks.Async
{
    public interface IDiscordAsyncCallback : IDisposable
    {
        IDiscordAsyncCallback OnSuccess(Action complete);
        void InvokeSuccess();
    }
}