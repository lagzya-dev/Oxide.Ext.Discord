using System;
using System.Linq;
using System.Reflection;

namespace Oxide.Ext.Discord.Singleton
{
    /// <summary>
    /// Represents a singleton of type {T}
    /// </summary>
    /// <typeparam name="T">Type of the singleton</typeparam>
    public abstract class Singleton<T> where T : Singleton<T>
    {
        /// <summary>
        /// Retrieves the instance of the singleton
        /// </summary>
        public static readonly T Instance;

        static Singleton()
        {
            try 
            {
                ConstructorInfo[] constructors = typeof(T).GetConstructors(BindingFlags.NonPublic | BindingFlags.Instance);
                Instance = (T)constructors.Single().Invoke(null);
            }
            catch 
            {
                throw new Exception($"{typeof(T)} {ErrorMessage}");
            }
        }

        private const string ErrorMessage = "must have a parameterless constructor and all constructors have to be NonPublic.";

        /// <summary>
        /// Constructor
        /// </summary>
        /// <exception cref="Exception"></exception>
        protected Singleton() 
        {
            ConstructorInfo[] constructors = typeof(T).GetConstructors(BindingFlags.Public | BindingFlags.Instance);
            if (constructors.Length != 0)
            {
                throw new Exception($"{typeof(T)} {ErrorMessage}");
            }
        }
    }
}