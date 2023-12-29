using Oxide.Core;

namespace Oxide.Ext.Discord.Interfaces
{
    public interface IPluginBase
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
    }
}