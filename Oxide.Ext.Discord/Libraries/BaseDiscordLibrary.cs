using System.Collections.Generic;
using Oxide.Core.Libraries;
using Oxide.Core.Plugins;
using Oxide.Ext.Discord.Connections;
using Oxide.Ext.Discord.Plugins.Setup;

namespace Oxide.Ext.Discord.Libraries
{
    /// <summary>
    /// Represents the base class for Discord Libraries
    /// </summary>
    public abstract class BaseDiscordLibrary : Library
    {
        private static readonly List<BaseDiscordLibrary> Libraries = new List<BaseDiscordLibrary>();

        /// <summary>
        /// Constructor
        /// </summary>
        protected BaseDiscordLibrary()
        {
            Libraries.Add(this);
        }

        internal static void ProcessPluginLoaded(PluginSetupData plugin, BotConnection connection)
        {
            for (int index = 0; index < Libraries.Count; index++)
            {
                BaseDiscordLibrary library = Libraries[index];
                library.OnPluginLoaded(plugin, connection);
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
        
        /// <summary>
        /// Called on the library when a plugin is loaded
        /// </summary>
        /// <param name="data">Plugin that was loaded</param>
        protected virtual void OnPluginLoaded(PluginSetupData data, BotConnection connection) {}
        
        /// <summary>
        /// Called on the library when a plugin is unloaded
        /// </summary>
        /// <param name="plugin">Plugin that was unloaded</param>
        protected virtual void OnPluginUnloaded(Plugin plugin) {}
    }
}