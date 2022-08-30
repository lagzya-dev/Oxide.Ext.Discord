using System;
using System.Collections.Generic;
using Oxide.Core;
using Oxide.Ext.Discord.Pooling;

namespace Oxide.Ext.Discord.Callbacks.Async
{
    public class DiscordAsyncCallback<T> : BasePoolable
    {
        private readonly List<Action<T>> _success = new List<Action<T>>();
        private readonly Action _successCallback;
        private T _data;

        public DiscordAsyncCallback()
        {
            _successCallback = InvokeSuccessInternal;
        }
        
        internal static DiscordAsyncCallback<T> Create()
        {
            return DiscordPool.Get<DiscordAsyncCallback<T>>();
        }

        public DiscordAsyncCallback<T> OnSuccess(Action<T> complete)
        {
            _success.Add(complete);
            return this;
        }

        internal void InvokeSuccess(T data)
        {
            _data = data;
            Interface.Oxide.NextTick(_successCallback);
        }

        private void InvokeSuccessInternal()
        {
            if (_success.Count != 0)
            {
                for (int index = 0; index < _success.Count; index++)
                {
                    Action<T> callback = _success[index];
                    callback.Invoke(_data);
                }
            }
        }

        ///<inheritdoc/>
        protected override void EnterPool()
        {
            _success.Clear();
            _data = default(T);
        }

        ///<inheritdoc/>
        protected override void DisposeInternal()
        {
            DiscordPool.Free(this);
        }
    }
}