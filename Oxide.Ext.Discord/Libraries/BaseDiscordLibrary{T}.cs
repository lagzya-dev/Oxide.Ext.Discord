using System;

namespace Oxide.Ext.Discord.Libraries
{
    public abstract class BaseDiscordLibrary<T> : BaseDiscordLibrary where T : BaseDiscordLibrary<T>
    {
        internal static T Instance;
        
        protected BaseDiscordLibrary()
        {
            if (Instance != null)
            {
                throw new Exception($"Duplicate Library Instances for type {typeof(T).FullName}");
            }
            
            Instance = (T)this;
        }
    }
}