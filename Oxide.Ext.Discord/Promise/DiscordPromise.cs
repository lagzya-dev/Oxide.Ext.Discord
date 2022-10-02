using System;
using System.Collections.Generic;
using Oxide.Core;
using Oxide.Ext.Discord.Exceptions.Promise;
using Oxide.Ext.Discord.Pooling;

namespace Oxide.Ext.Discord.Promise
{
    /// <summary>
    /// Represents the base class for Discord Async Callbacks
    /// </summary>
    public class DiscordPromise : BasePoolable, IDiscordPromise
    {
        protected bool IsInternal;
        internal PromiseState State { get; private set; }
        protected Exception Exception;
        
        protected readonly List<Action<Exception>> Fails = new List<Action<Exception>>();
        
        private readonly Action _resolve;
        private readonly Action _fail;
        private readonly Action _dispose;

        private readonly List<Action> _resolves = new List<Action>();

        private readonly List<DiscordPromise> _children = new List<DiscordPromise>();

        public DiscordPromise()
        {
            _resolve = InvokeResolveInternal;
            _fail = InvokeFailInternal;
            _dispose = Dispose;
        }

        internal static DiscordPromise Create(bool isInternal = false)
        {
            DiscordPromise promise = DiscordPool.Get<DiscordPromise>();
            promise.IsInternal = isInternal;
            return promise;
        }

        /// <summary>
        /// Registers a callback once the async call is completed
        /// </summary>
        /// <param name="onResolved">Callback to register</param>
        /// <returns>this</returns>
        public IDiscordPromise Then(Action onResolved)
        {
            if (State == PromiseState.Resolved)
            {
                onResolved();
                return this;
            }
            
            _resolves.Add(onResolved);
            return this;
        }

        public IDiscordPromise Fail(Action<Exception> onFail)
        {
            if (State == PromiseState.Failed)
            {
                onFail(Exception);
                return this;
            }

            Fails.Add(onFail);
            return this;
        }

        public IDiscordPromise Done(Action onResolved, Action<Exception> onFail)
        {
            Then(onResolved);
            Fail(onFail);
            return this;
        }

        /// <summary>
        /// Handles calling the callbacks and disposing of the object
        /// </summary>
        protected virtual void InvokeResolveInternal()
        {
            for (int index = 0; index < _resolves.Count; index++)
            {
                Action callback = _resolves[index];
                callback.Invoke();
            }
            
            DelayedDispose();
        }

        private void InvokeFailInternal()
        {
            for (int index = 0; index < Fails.Count; index++)
            {
                Action<Exception> callback = Fails[index];
                callback.Invoke(Exception);
            }

            for (int index = 0; index < _children.Count; index++)
            {
                DiscordPromise child = _children[index];
                child.Fail(Exception);
            }
            
            DelayedDispose();
        }

        /// <summary>
        /// Invokes the <see cref="InvokeResolveInternal"/> method
        /// </summary>
        public void Resolve()
        {
            SetState(PromiseState.Resolved);
            State = PromiseState.Resolved;
            if (IsInternal)
            {
                InvokeResolveInternal();
                return;
            }
            
            Interface.Oxide.NextTick(_resolve);
        }

        public void Fail(Exception ex)
        {
            SetState(PromiseState.Failed);
            Exception = ex;
            if (IsInternal)
            {
                InvokeFailInternal();
                return;
            }
            
            Interface.Oxide.NextTick(_fail);
        }

        protected void AddChild(DiscordPromise child)
        {
            if (State == PromiseState.Failed)
            {
                child.Fail(Exception);
                return;
            }
            
            if (!_children.Contains(child))
            {
                _children.Add(child);
            }
        }

        protected void SetState(PromiseState state)
        {
            PromiseResolvedException.ThrowIfNotPending(this);
            State = state;
        }

        private void DelayedDispose()
        {
            Interface.Oxide.NextTick(_dispose);
        }

        protected override void LeavePool()
        {
            State = PromiseState.Pending;
        }

        ///<inheritdoc/>
        protected override void EnterPool()
        {
            IsInternal = false;
            State = PromiseState.Pending;
            Fails.Clear();
            _resolves.Clear();
            _children.Clear();
        }
    }
}