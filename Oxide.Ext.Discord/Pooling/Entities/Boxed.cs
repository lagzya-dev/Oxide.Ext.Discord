using System;
using Oxide.Ext.Discord.Cache;
using Oxide.Ext.Discord.Libraries.Pooling;

namespace Oxide.Ext.Discord.Pooling.Entities
{
    internal class Boxed<T> : IBoxed
    {
        public T Value;
        private bool _disposed;
        internal DiscordPluginPool _pool;

        public override string ToString() => StringCache<T>.Instance.ToString(Value);

        internal void LeavePool()
        {
            _disposed = false;
        }

        public IBoxed Copy()
        {
            return _pool.GetBoxed(Value);
        }
        
        public void Dispose()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException($"{nameof(Boxed<T>)}<{typeof(T).Name}>");
            }
            
            _disposed = true;
            DiscordPool.Internal.FreeBoxed(this);
        }
    }

    internal interface IBoxed : IDisposable
    {
        IBoxed Copy();
    }
}