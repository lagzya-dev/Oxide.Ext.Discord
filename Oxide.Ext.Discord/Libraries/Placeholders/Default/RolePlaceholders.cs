using System.Text;
using Oxide.Core.Plugins;
using Oxide.Ext.Discord.Entities.Permissions;
using Oxide.Ext.Discord.Plugins.Core;

namespace Oxide.Ext.Discord.Libraries.Placeholders.Default
{
    /// <summary>
    /// <see cref="DiscordRole"/> placeholders
    /// </summary>
    public static class RolePlaceholders
    {
        /// <summary>
        /// <see cref="DiscordRole.Id"/> placeholder
        /// </summary>
        public static void Id(StringBuilder builder, PlaceholderState state, DiscordRole role) => PlaceholderFormatting.Replace(builder, state, role.Id);
        
        /// <summary>
        /// <see cref="DiscordRole.Name"/> placeholder
        /// </summary>
        public static void Name(StringBuilder builder, PlaceholderState state, DiscordRole role) => PlaceholderFormatting.Replace(builder, state, role.Name);
        
        /// <summary>
        /// <see cref="DiscordRole.Mention"/> placeholder
        /// </summary>
        public static void Mention(StringBuilder builder, PlaceholderState state, DiscordRole role) => PlaceholderFormatting.Replace(builder, state, role.Mention);
        
        /// <summary>
        /// <see cref="DiscordRole.Icon"/> placeholder
        /// </summary>
        public static void Icon(StringBuilder builder, PlaceholderState state, DiscordRole role) => PlaceholderFormatting.Replace(builder, state, role.Icon);

        internal static void RegisterPlaceholders()
        {
            RegisterPlaceholders(DiscordExtensionCore.Instance, "role");
        }
        
        /// <summary>
        /// Registers placeholders for the given plugin. 
        /// </summary>
        /// <param name="plugin">Plugin to register placeholders for</param>
        /// <param name="placeholderPrefix">Prefix to use for the placeholders</param>
        /// <param name="dataKey">Data key in <see cref="PlaceholderData"/></param>
        public static void RegisterPlaceholders(Plugin plugin, string placeholderPrefix, string dataKey = nameof(DiscordRole))
        {
            DiscordPlaceholders placeholders = DiscordPlaceholders.Instance;
            placeholders.RegisterPlaceholder<DiscordRole>(plugin, $"{placeholderPrefix}.id", dataKey, Id);
            placeholders.RegisterPlaceholder<DiscordRole>(plugin, $"{placeholderPrefix}.name", dataKey, Name);
            placeholders.RegisterPlaceholder<DiscordRole>(plugin, $"{placeholderPrefix}.mention", dataKey, Mention);
            placeholders.RegisterPlaceholder<DiscordRole>(plugin, $"{placeholderPrefix}.icon", dataKey, Icon);
        }
    }
}