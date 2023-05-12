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
        
        string Title { get; }

        string Description { get; }

        string Author { get; }

        VersionNumber Version { get; }

        /// <summary>
        /// Gets / Sets the DiscordClient on a plugin
        /// </summary>
        DiscordClient Client { get; set; }
    }
}