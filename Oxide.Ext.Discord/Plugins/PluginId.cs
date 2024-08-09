using System;
using Oxide.Core.Plugins;
using Oxide.Ext.Discord.Extensions;
using Oxide.Ext.Discord.Interfaces;
using Oxide.Ext.Discord.Logging;

namespace Oxide.Ext.Discord.Plugins;

/// <summary>
/// Represents a Plugin ID
/// </summary>
public readonly record struct PluginId : IDebugLoggable
{
    /// <summary>
    /// Hashcode value of the Plugin Name
    /// </summary>
    public readonly int Id;
        
    /// <summary>
    /// Returns if the PluginId is valid
    /// </summary>
    public bool IsValid => Id != 0;

    internal bool IsExtensionPlugin => IsValid && this == DiscordExtensionCore.Instance.PluginId;

    internal PluginId(Plugin plugin)
    {
        Id = plugin?.Name.GetHashCode() ?? throw new ArgumentNullException(nameof(plugin));
    }
        
    internal PluginId(string id)
    {
        Id = id?.GetHashCode() ?? throw new ArgumentNullException(nameof(id));
    }

    /// <summary>
    /// Returns the PluginName
    /// </summary>
    /// <returns></returns>
    public override string ToString() => this.PluginName();

    ///<inheritdoc/>
    public void LogDebug(DebugLogger logger)
    {
        logger.AppendField("ID", Id);
        logger.AppendField("Name", this.PluginName());
    }
}