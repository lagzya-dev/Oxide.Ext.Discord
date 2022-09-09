using System.Text;
using Oxide.Core.Plugins;
using Oxide.Ext.Discord.Entities;
using Oxide.Ext.Discord.Plugins.Core;

namespace Oxide.Ext.Discord.Libraries.Placeholders.Default
{
    public static class SnowflakePlaceholders
    {
        public static void Id(StringBuilder builder, PlaceholderState state, Snowflake id) => PlaceholderFormatting.Replace(builder, state, id);
        public static void Created(StringBuilder builder, PlaceholderState state, Snowflake id) => PlaceholderFormatting.Replace(builder, state, id.GetCreationDate());

        internal static void RegisterPlaceholders()
        {
            RegisterPlaceholders(DiscordExtensionCore.Instance, "snowflake", nameof(Snowflake));
        }
        
        public static void RegisterPlaceholders(Plugin plugin, string placeholderPrefix, string dataKey)
        {
            DiscordPlaceholders placeholders = DiscordExtension.DiscordPlaceholders;
            placeholders.RegisterPlaceholder<Snowflake>(plugin,$"{placeholderPrefix}.id", dataKey, Id);
            placeholders.RegisterPlaceholder<Snowflake>(plugin,$"{placeholderPrefix}.Created", dataKey, Created);
        }
    }
}