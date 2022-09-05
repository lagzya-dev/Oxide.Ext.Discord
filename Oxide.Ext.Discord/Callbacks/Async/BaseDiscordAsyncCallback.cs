using System;
using System.Collections.Generic;
using Oxide.Ext.Discord.Interfaces.Callbacks.Async;
using Oxide.Ext.Discord.Pooling;

namespace Oxide.Ext.Discord.Callbacks.Async
{
    public abstract class BaseDiscordAsyncCallback<T> : BasePoolable, IDiscordAsyncCallback<T>
    {
        private readonly List<Action<T>> _success = new List<Action<T>>();
        protected T Data;
        
        public IDiscordAsyncCallback<T> OnSuccess(Action<T> complete)
        {
            _success.Add(complete);
            return this;
        }
        
        protected void InvokeSuccessInternal()
        {
            if (_success.Count != 0)
            {
                for (int index = 0; index < _success.Count; index++)
                {
                    Action<T> callback = _success[index];
                    callback.Invoke(Data);
                }
            }
            
            Dispose();
        }

        public abstract void InvokeSuccess(T data);
        
        protected override void EnterPool()
        {
            Data = default(T);
            _success.Clear();
        }
    }
}