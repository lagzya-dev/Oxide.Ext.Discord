// Originally from: https://github.com/Real-Serious-Games/C-Sharp-Promise
// Modified by: MJSU

using System;
using System.Collections.Generic;
using Oxide.Core;
using Oxide.Ext.Discord.Entities;
using Oxide.Ext.Discord.Exceptions.Promise;
using Oxide.Ext.Discord.Factory;
using Oxide.Ext.Discord.Interfaces.Promises;
using Oxide.Ext.Discord.Pooling;
using Oxide.Ext.Discord.Threading;

namespace Oxide.Ext.Discord.Promises
{
    public class BasePromise : BasePoolable, IRejectable
    {
        /// <summary>
        /// ID of the promise, useful for debugging.
        /// </summary>
        public Snowflake Id { get; private set; }

        /// <summary>
        /// Name of the promise, when set, useful for debugging.
        /// </summary>
        public string Name { get; protected set; }

        /// <summary>
        /// Tracks the current state of the promise.
        /// </summary>
        public PromiseState State { get; protected set; } = PromiseState.Pending;
        
        /// <summary>
        /// The exception when the promise is rejected.
        /// </summary>
        protected Exception Exception;

        protected bool IsInternal;
        
        protected readonly List<RejectHandler> Rejects = new List<RejectHandler>();

        internal readonly Action<Exception> OnReject;
        private readonly Action _onRejectInternal;
        private readonly Action _dispose;
        
        public BasePromise()
        {
            OnReject = Reject;
            _onRejectInternal = InvokeRejectHandlersInternal;
            _dispose = Dispose;
        }

        /// <summary>
        /// Reject the promise with an exception.
        /// </summary>
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
            if (ThreadState.IsMain || IsInternal)
            {
                InvokeRejectHandlersInternal();
                return;
            }
            
            Interface.Oxide.NextTick(_onRejectInternal);
        }

        private void InvokeRejectHandlersInternal()
        {
            for (int i = 0, maxI = Rejects.Count; i < maxI; ++i)
            {
                Rejects[i].Reject(Exception);
            }

            ClearHandlers();
        }
        
        protected virtual void ClearHandlers()
        {
            Rejects.Clear();
            Interface.Oxide.NextTick(_dispose);
        }

        protected override void LeavePool()
        {
            Id = SnowflakeIdFactory.Instance.Generate();
            base.LeavePool();
        }

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