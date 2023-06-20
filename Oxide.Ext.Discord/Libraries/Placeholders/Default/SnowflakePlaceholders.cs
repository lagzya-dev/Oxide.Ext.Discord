using System;
using Oxide.Core.Plugins;
using Oxide.Ext.Discord.Entities;
using Oxide.Ext.Discord.Plugins.Core;

namespace Oxide.Ext.Discord.Libraries.Placeholders.Default
{
    /// <summary>
    /// <see cref="Snowflake"/> placeholders
    /// </summary>
    public static class SnowflakePlaceholders
    {
        /// <summary>
        /// <see cref="Snowflake.GetCreationDate"/> placeholder
        /// </summary>
        public static DateTimeOffset Created(Snowflake id) => id.GetCreationDate();

        internal static void RegisterPlaceholders()
        {
            RegisterPlaceholders(DiscordExtensionCore.Instance, "snowflake", nameof(Snowflake));
        }
        
        /// <summary>
        /// Registers placeholders for the given plugin. 
        /// </summary>
        /// <param name="plugin">Plugin to register placeholders for</param>
        /// <param name="placeholderPrefix">Prefix to use for the placeholders</param>
        /// <param name="dataKey">Data key in <see cref="PlaceholderData"/></param>
        public static void RegisterPlaceholders(Plugin plugin, string placeholderPrefix, string dataKey)
        {
            DiscordPlaceholders placeholders = DiscordPlaceholders.Instance;
            placeholders.RegisterPlaceholder<Snowflake>(plugin,$"{placeholderPrefix}.id", dataKey);
            placeholders.RegisterPlaceholder<Snowflake, DateTimeOffset>(plugin,$"{placeholderPrefix}.created", dataKey, Created);
        }
    }
}