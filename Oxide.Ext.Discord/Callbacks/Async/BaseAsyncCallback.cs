using System;
using System.Collections.Generic;
using Oxide.Ext.Discord.Interfaces.Callbacks.Async;
using Oxide.Ext.Discord.Pooling;

namespace Oxide.Ext.Discord.Callbacks.Async
{
    public abstract class BaseAsyncCallback<T> : BasePoolable, IDiscordAsyncCallback<T>
    {
        protected readonly List<Action<T>> Success = new List<Action<T>>();
        protected T Data;
        
        public IDiscordAsyncCallback<T> OnSuccess(Action<T> complete)
        {
            Success.Add(complete);
            return this;
        }
        
        protected void InvokeSuccessInternal()
        {
            if (Success.Count != 0)
            {
                for (int index = 0; index < Success.Count; index++)
                {
                    Action<T> callback = Success[index];
                    callback.Invoke(Data);
                }
            }
            
            Dispose();
        }

        public abstract void InvokeSuccess(T data);
        
        protected override void EnterPool()
        {
            Data = default(T);
            Success.Clear();
        }
    }
}