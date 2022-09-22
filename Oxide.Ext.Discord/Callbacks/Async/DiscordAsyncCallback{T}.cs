using System;
using System.Collections.Generic;
using Oxide.Ext.Discord.Interfaces.Callbacks.Async;
using Oxide.Ext.Discord.Pooling;

namespace Oxide.Ext.Discord.Callbacks.Async
{
    /// <summary>
    /// Represents the base class for Discord Async Callbacks of {T}
    /// </summary>
    /// <typeparam name="TResult">Type to callback with</typeparam>
    public class DiscordAsyncCallback<TResult> : DiscordAsyncCallback, IDiscordAsyncCallback<TResult>
    {
        /// <summary>
        /// The data to callback with
        /// </summary>
        private TResult _data;
        
        private readonly List<Action<TResult>> _success = new List<Action<TResult>>();

        internal static DiscordAsyncCallback<TResult> Create(bool isInternal = false)
        {
            DiscordAsyncCallback<TResult> callback = DiscordPool.Get<DiscordAsyncCallback<TResult>>();
            callback.IsInternal = isInternal;
            return callback;
        }
        
        /// <summary>
        /// Registers a callback once the async call is completed with {T} type
        /// </summary>
        /// <param name="complete">Callback to register</param>
        /// <returns>This</returns>
        public IDiscordAsyncCallback<TResult> OnSuccess(Action<TResult> complete)
        {
            _success.Add(complete);
            return this;
        }
        
        /// <summary>
        /// Handles calling the callbacks and disposing of the object
        /// </summary>
        protected override void InvokeSuccessInternal()
        {
            if (_success.Count != 0)
            {
                for (int index = 0; index < _success.Count; index++)
                {
                    Action<TResult> callback = _success[index];
                    callback.Invoke(_data);
                }
            }
            
            base.InvokeSuccessInternal();
        }

        /// <summary>
        /// Invokes the <see cref="InvokeSuccessInternal"/> method
        /// </summary>
        /// <param name="data">Data to invoke with</param>
        internal void InvokeSuccess(TResult data)
        {
            _data = data;
            InvokeSuccessInternal();
        }

        ///<inheritdoc/>
        protected override void EnterPool()
        {
            base.EnterPool();
            _data = default(TResult);
            _success.Clear();
        }

        protected override void DisposeInternal()
        {
            DiscordPool.Free(this);
        }
    }
}