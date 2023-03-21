using System;
using Oxide.Ext.Discord.Libraries.Pooling;

namespace Oxide.Ext.Discord.Pooling.Entities
{
    internal class Boxed<T> : IBoxed
    {
        public T Value;
        private bool _disposed;

        public override string ToString() => Value.ToString();

        internal void LeavePool()
        {
            _disposed = false;
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

    internal interface IBoxed : IDisposable { }
}