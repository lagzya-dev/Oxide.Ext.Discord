using Oxide.Core;

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
        
        /// <summary>
        /// Title of the plugin
        /// </summary>
        string Title { get; }

        /// <summary>
        /// Description of the plugin
        /// </summary>
        string Description { get; }

        /// <summary>
        /// Author of the plugin
        /// </summary>
        string Author { get; }

        /// <summary>
        /// Version of the plugin
        /// </summary>
        VersionNumber Version { get; }

        /// <summary>
        /// Gets / Sets the DiscordClient on a plugin
        /// </summary>
        DiscordClient Client { get; set; }
    }
}