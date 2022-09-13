using System;
using System.Collections.Generic;
using Oxide.Ext.Discord.Interfaces.Callbacks.Async;
using Oxide.Ext.Discord.Pooling;

namespace Oxide.Ext.Discord.Callbacks.Async
{
    public abstract class BaseDiscordAsyncCallback : BasePoolable, IDiscordAsyncCallback
    {
        private readonly List<Action> _success = new List<Action>();

        public IDiscordAsyncCallback OnSuccess(Action complete)
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
                    Action callback = _success[index];
                    callback.Invoke();
                }
            }
            
            Dispose();
        }

        public abstract void InvokeSuccess();
        
        protected override void EnterPool()
        {
            _success.Clear();
        }
    }
}