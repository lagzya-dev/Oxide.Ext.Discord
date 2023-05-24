using System;
using Oxide.Ext.Discord.Promise;

namespace Oxide.Ext.Discord.Exceptions.Promise
{
    /// <summary>
    /// Represents an exception related to promises
    /// </summary>
    public class PromiseException : BaseDiscordException
    {
        private PromiseException(string message) : base(message) { }

        internal static void ThrowIfNotPending(DiscordPromise promise)
        {
            if (promise.State != PromiseState.Pending)
            {
                throw new PromiseException($"Tried to resolve promise {promise.GetType().Name} which has already been resolved with state: {promise.State}.");
            }
        }

        internal static void ThrowIfDisposed(DiscordPromise promise)
        {
            if (promise.Disposed)
            {
                throw new ObjectDisposedException($"{promise.GetType().Name}");
            }
        }
    }
}