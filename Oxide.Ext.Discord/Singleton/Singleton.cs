using System;

namespace Oxide.Ext.Discord.Singleton
{
    public abstract class Singleton<T> where T : Singleton<T>, new()
    {
        public static T Instance => LazyInit.Value;

        private static readonly Lazy<T> LazyInit = new Lazy<T>(() => new T(), true);
    }
}