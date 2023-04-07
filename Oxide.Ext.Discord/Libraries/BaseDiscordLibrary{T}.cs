using System;

namespace Oxide.Ext.Discord.Libraries
{
    /// <summary>
    /// Base Discord Library for Oxide Libraries 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class BaseDiscordLibrary<T> : BaseDiscordLibrary where T : BaseDiscordLibrary<T>
    {
        internal static T Instance;
        
        /// <summary>
        /// Constructor
        /// </summary>
        /// <exception cref="Exception">Thrown if the Library has already been initialized</exception>
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