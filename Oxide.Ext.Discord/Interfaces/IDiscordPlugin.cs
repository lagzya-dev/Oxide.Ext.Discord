namespace Oxide.Ext.Discord.Interfaces
{
    /// <summary>
    /// Represents a plugin that uses the Discord Extension
    /// </summary>
    public interface IDiscordPlugin
    {
        /// <summary>
        /// Name of the plugin
        /// </summary>
        string Name { get; }
    }
}