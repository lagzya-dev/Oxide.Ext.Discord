// Originally from: https://github.com/Real-Serious-Games/C-Sharp-Promise
// Modified by: MJSU

using System;
using System.Collections.Generic;
using Oxide.Core;
using Oxide.Ext.Discord.Entities;
using Oxide.Ext.Discord.Exceptions.Promise;
using Oxide.Ext.Discord.Extensions;
using Oxide.Ext.Discord.Factory;
using Oxide.Ext.Discord.Interfaces.Promises;
using Oxide.Ext.Discord.Pooling;

namespace Oxide.Ext.Discord.Promises
{
    /// <summary>
    /// Represents the base class for all promises
    /// </summary>
    public class BasePromise : BasePoolable, IRejectable
    {
        ///<inheritdoc/>
        public Snowflake Id { get; private set; }

        ///<inheritdoc/>
        public string Name { get; protected set; }

        /// <summary>
        /// Tracks the current state of the promise.
        /// </summary>
        public PromiseState State { get; protected set; } = PromiseState.Pending;
        
        /// <summary>
        /// The exception when the promise is rejected.
        /// </summary>
        protected Exception Exception;

        /// <summary>
        /// If this promise is an promise internal to the DiscordExtension only.
        /// This promise and all child promises are allowed to run off the main thread
        /// </summary>
        protected bool IsInternal;
        
        /// <summary>
        /// Collection of handlers for rejected promises
        /// </summary>
        protected readonly List<RejectHandler> Rejects = new List<RejectHandler>();

        internal readonly Action<Exception> OnReject;
        private readonly Action _onRejectInternal;
        private readonly Action _dispose;

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
            if (ThreadEx.IsMain || IsInternal)
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
                Rejects[i].Reject(Exception);
            }

            ClearHandlers();
        }
        
        /// <summary>
        /// Clears all the handlers for the promises
        /// Called after completion
        /// </summary>
        protected virtual void ClearHandlers() => Rejects.Clear();

        /// <summary>
        /// Delays disposing the promise till NextTick
        /// </summary>
        protected void DelayedDispose() => Interface.Oxide.NextTick(_dispose);

        ///<inheritdoc/>
        protected override void LeavePool()
        {
            Id = SnowflakeIdFactory.Instance.Generate();
            base.LeavePool();
        }

        ///<inheritdoc/>
        protected override void EnterPool()
        {
            Id = default(Snowflake);
            Name = null;
            State = PromiseState.Pending;
            Exception = null;
            Rejects.Clear();
            IsInternal = false;
            base.EnterPool();
        }
    }
}