using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Oxide.Ext.Discord.Pooling;
using Oxide.Plugins;

namespace Oxide.Ext.Discord.Exceptions.Pool
{
    /// <summary>
    /// Represents an exception on the Discord Pool
    /// </summary>
    public class DiscordPoolException : BaseDiscordException
    {
        private DiscordPoolException(string message) : base(message) { }

        internal static void ThrowIfList(Type type)
        {
            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(List<>))
            {
                throw new DiscordPoolException($"Cannot use {nameof(DiscordPool)}.{nameof(DiscordPool.Get)} for a list type. Use {nameof(DiscordPool)}.{nameof(DiscordPool.GetList)} instead");
            }
        }
        
        internal static void ThrowIfHash(Type type)
        {
            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Hash<,>))
            {
                throw new DiscordPoolException($"Cannot use {nameof(DiscordPool)}.{nameof(DiscordPool.Get)} for a list type. Use {nameof(DiscordPool)}.{nameof(DiscordPool.GetHash)} instead");
            }
        }
        
        internal static void ThrowIfStringBuilder(Type type)
        {
            if(type == typeof(StringBuilder))
            {
                throw new DiscordPoolException($"Cannot use {nameof(DiscordPool)}.{nameof(DiscordPool.Get)} for a StringBuilder type. Use {nameof(DiscordPool)}.{nameof(DiscordPool.GetStringBuilder)} instead");
            }
        }
        
        internal static void ThrowIfMemoryStream(Type type)
        {
            if(type == typeof(MemoryStream))
            {
                throw new DiscordPoolException($"Cannot use {nameof(DiscordPool)}.{nameof(DiscordPool.Get)} for a MemoryStream type. Use {nameof(DiscordPool)}.{nameof(DiscordPool.GetMemoryStream)} instead");
            }
        }
    }
}