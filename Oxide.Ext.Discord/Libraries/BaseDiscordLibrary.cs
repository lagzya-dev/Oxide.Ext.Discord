using System.Collections.Generic;
using Oxide.Core.Libraries;
using Oxide.Core.Plugins;

namespace Oxide.Ext.Discord.Libraries
{
    public abstract class BaseDiscordLibrary : Library
    {
        private static readonly List<BaseDiscordLibrary> Libraries = new List<BaseDiscordLibrary>();

        protected BaseDiscordLibrary()
        {
            Libraries.Add(this);
        }

        internal static void ProcessPluginLoaded(Plugin plugin)
        {
            for (int index = 0; index < Libraries.Count; index++)
            {
                BaseDiscordLibrary library = Libraries[index];
                library.OnPluginLoaded(plugin);
            }
        }
        
        internal static void ProcessPluginUnloaded(Plugin plugin)
        {
            for (int index = 0; index < Libraries.Count; index++)
            {
                BaseDiscordLibrary library = Libraries[index];
                library.OnPluginUnloaded(plugin);
            }
        }
        
        protected abstract void OnPluginLoaded(Plugin plugin);
        protected abstract void OnPluginUnloaded(Plugin plugin);
    }
}