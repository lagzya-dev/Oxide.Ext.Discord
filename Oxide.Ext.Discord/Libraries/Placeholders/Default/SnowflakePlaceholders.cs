using System.Text;
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
        /// <see cref="Snowflake.Id"/> placeholder
        /// </summary>
        public static void Id(StringBuilder builder, PlaceholderState state, Snowflake id) => PlaceholderFormatting.Replace(builder, state, id);
        
        /// <summary>
        /// <see cref="Snowflake.GetCreationDate"/> placeholder
        /// </summary>
        public static void Created(StringBuilder builder, PlaceholderState state, Snowflake id) => PlaceholderFormatting.Replace(builder, state, id.GetCreationDate());

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
            DiscordPlaceholders placeholders = DiscordExtension.DiscordPlaceholders;
            placeholders.RegisterPlaceholder<Snowflake>(plugin,$"{placeholderPrefix}.id", dataKey, Id);
            placeholders.RegisterPlaceholder<Snowflake>(plugin,$"{placeholderPrefix}.Created", dataKey, Created);
        }
    }
}