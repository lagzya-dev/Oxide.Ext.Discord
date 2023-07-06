// Originally from: https://github.com/Real-Serious-Games/C-Sharp-Promise
// Modified by: MJSU

using System;
using System.Collections.Generic;
using Oxide.Core;
using Oxide.Ext.Discord.Entities;
using Oxide.Ext.Discord.Exceptions.Libraries.Promise;
using Oxide.Ext.Discord.Extensions;
using Oxide.Ext.Discord.Factory;
using Oxide.Ext.Discord.Interfaces.Promises;
using Oxide.Ext.Discord.Pooling;

#if PROMISE_DEBUG
using Oxide.Ext.Discord.Logging;
#endif

namespace Oxide.Ext.Discord.Promises
{
    /// <summary>
    /// Represents the base class for all promises
    /// </summary>
    public class BasePromise : BasePoolable, IRejectable
    {
        /// <summary>
        /// ID of the promise
        /// </summary>
        public Snowflake Id { get; private set; }

        /// <summary>
        /// Tracks the current state of the promise.
        /// </summary>
        public PromiseState State { get; protected set; } = PromiseState.Pending;
        
        /// <summary>
        /// The exception when the promise is rejected.
        /// </summary>
        protected Exception Exception;

        private bool _isDisposing;
        
        /// <summary>
        /// Collection of handlers for rejected promises
        /// </summary>
        protected readonly List<RejectHandler> Rejects = new List<RejectHandler>();

        internal readonly Action<Exception> OnReject;
        private readonly Action _onRejectInternal;
        private readonly Action _dispose;
        
#if PROMISE_DEBUG
        private System.Timers.Timer _timer;
#endif

        /// <summary>
        /// Constructor
        /// </summary>
        protected BasePromise()
        {
            OnReject = Reject;
            _onRejectInternal = InvokeRejectHandlersInternal;
            _dispose = Dispose;
        }
        
        ///<inheritdoc/>
        public void Reject(Exception ex)
        {
            PromiseException.ThrowIfDisposed(this);
            PromiseException.ThrowIfNotPending(State);

            Exception = ex;
            State = PromiseState.Rejected;

            InvokeRejectHandlers(ex);            
        }
        
        /// <summary>
        /// Invoke all resolve handlers.
        /// </summary>
        private void InvokeRejectHandlers(Exception ex)
        {
            Exception = ex;
            if (ThreadEx.IsMain)
            {
                InvokeRejectHandlersInternal();
                return;
            }
            
            Interface.Oxide.NextTick(_onRejectInternal);
        }

        private void InvokeRejectHandlersInternal()
        {
            for (int i = 0; i < Rejects.Count; ++i)
            {
                RejectHandler reject = Rejects[i];
#if PROMISE_DEBUG
                DiscordExtension.GlobalLogger.Info($"Invoking Reject ID: {Id}");
#endif
                reject.Reject(Exception);
            }

            ClearHandlers();
            DelayedDispose();
        }
        
        /// <summary>
        /// Clears all the handlers for the promises
        /// Called after completion
        /// </summary>
        protected virtual void ClearHandlers()
        {
            Rejects.Clear();
        }

        /// <summary>
        /// Delays disposing the promise till NextTick
        /// </summary>
        protected void DelayedDispose()
        {
            if (!_isDisposing)
            {
                _isDisposing = true;
                Interface.Oxide.NextTick(_dispose);
            }
        }

        ///<inheritdoc/>
        protected override void LeavePool()
        {
            Id = SnowflakeIdFactory.Instance.Generate();
            base.LeavePool();
#if PROMISE_DEBUG
            Snowflake id = Id;
            DiscordExtension.GlobalLogger.Error($"Creating Promise. ID: {Id}");
            _timer = new System.Timers.Timer(5000);
            _timer.Elapsed += (sender, args) =>
            {
                if (Id == id && !Disposed)
                {
                    DiscordExtension.GlobalLogger.Error($"Promise not disposed!!! ID: {Id}");
                }
            };
#endif
        }

        ///<inheritdoc/>
        protected override void EnterPool()
        {
#if PROMISE_DEBUG
            DiscordExtension.GlobalLogger.Info($"Disposed Promise: {Id}");
            _timer?.Stop();
            _timer?.Dispose();
            _timer = null;
#endif
            Id = default(Snowflake);
            State = PromiseState.Pending;
            Exception = null;
            Rejects.Clear();
            _isDisposing = false;
            base.EnterPool();
        }
    }
}