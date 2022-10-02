using Oxide.Ext.Discord.Promise;

namespace Oxide.Ext.Discord.Exceptions.Promise
{
    public class PromiseResolvedException : BaseDiscordException
    {
        private PromiseResolvedException(string message) : base(message) { }

        internal static void ThrowIfNotPending(DiscordPromise promise)
        {
            if (promise.State != PromiseState.Pending)
            {
                throw new PromiseResolvedException($"Tried to resolve promise {promise.GetType().Name} which has already been resolved with state: {promise.State}.");
            }
        }
    }
}