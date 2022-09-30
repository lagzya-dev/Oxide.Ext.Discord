using System;
using System.Collections.Generic;
using Oxide.Core;
using Oxide.Ext.Discord.Interfaces.Callbacks.Async;
using Oxide.Ext.Discord.Pooling;

namespace Oxide.Ext.Discord.Callbacks.Async
{
    /// <summary>
    /// Represents the base class for Discord Async Callbacks
    /// </summary>
    public class DiscordAsyncCallback : BasePoolable, IDiscordAsyncCallback
    {
        protected bool IsInternal;
        
        private readonly Action _callback;
        private readonly List<Action> _success = new List<Action>();

        public DiscordAsyncCallback()
        {
            _callback = InvokeSuccessInternal;
        }

        internal static DiscordAsyncCallback Create(bool isInternal = false)
        {
            DiscordAsyncCallback callback = DiscordPool.Get<DiscordAsyncCallback>();
            callback.IsInternal = isInternal;
            return callback;
        }

        /// <summary>
        /// Registers a callback once the async call is completed
        /// </summary>
        /// <param name="complete">Callback to register</param>
        /// <returns>this</returns>
        public IDiscordAsyncCallback OnSuccess(Action complete)
        {
            _success.Add(complete);
            return this;
        }
        
        /// <summary>
        /// Handles calling the callbacks and disposing of the object
        /// </summary>
        protected virtual void InvokeSuccessInternal()
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

        /// <summary>
        /// Invokes the <see cref="InvokeSuccessInternal"/> method
        /// </summary>
        internal void InvokeSuccess()
        {
            if (IsInternal)
            {
                InvokeSuccessInternal();
                return;
            }
            
            Interface.Oxide.NextTick(_callback);
        }

        ///<inheritdoc/>
        protected override void EnterPool()
        {
            _success.Clear();
        }

        protected override void DisposeInternal()
        {
            DiscordPool.Free(this);
        }
    }
}