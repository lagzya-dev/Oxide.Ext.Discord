using System.Collections.Generic;
using Oxide.Core.Libraries;
using Oxide.Core.Plugins;
using Oxide.Ext.Discord.Connections;
using Oxide.Ext.Discord.Plugins;

namespace Oxide.Ext.Discord.Libraries;

/// <summary>
/// Represents the base class for Discord Libraries
/// </summary>
public abstract class BaseDiscordLibrary : Library
{
    private static readonly List<BaseDiscordLibrary> Libraries = new();

    /// <summary>
    /// Constructor
    /// </summary>
    protected BaseDiscordLibrary()
    {
        Libraries.Add(this);
    }

    internal static void ProcessPluginLoaded(PluginSetup plugin, BotConnection connection)
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
    /// <param name="connection">Connection for the plugin</param>
    protected virtual void OnPluginLoaded(PluginSetup data, BotConnection connection) {}
        
    /// <summary>
    /// Called on the library when a plugin is unloaded
    /// </summary>
    /// <param name="plugin">Plugin that was unloaded</param>
    protected virtual void OnPluginUnloaded(Plugin plugin) {}
}