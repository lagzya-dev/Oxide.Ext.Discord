using Oxide.Core.Plugins;
using Oxide.Ext.Discord.Entities;
using Oxide.Ext.Discord.Entities.Messages;
using Oxide.Ext.Discord.Plugins.Core;

namespace Oxide.Ext.Discord.Libraries.Placeholders.Default
{
    /// <summary>
    /// <see cref="DiscordMessage"/> placeholders
    /// </summary>
    public static class MessagePlaceholders
    {
        /// <summary>
        /// <see cref="DiscordMessage.Id"/> placeholder
        /// </summary>
        public static Snowflake Id(PlaceholderState state, DiscordMessage message) => message.Id;
        
        /// <summary>
        /// <see cref="DiscordMessage.ChannelId"/> placeholder
        /// </summary>
        public static Snowflake ChannelId(PlaceholderState state, DiscordMessage message) => message.ChannelId;
        
        /// <summary>
        /// <see cref="DiscordMessage.Content"/> placeholder
        /// </summary>
        public static string Content(PlaceholderState state, DiscordMessage message) => message.Content;

        internal static void RegisterPlaceholders()
        {
            RegisterPlaceholders(DiscordExtensionCore.Instance, "message");
        }
        
        /// <summary>
        /// Registers placeholders for the given plugin. 
        /// </summary>
        /// <param name="plugin">Plugin to register placeholders for</param>
        /// <param name="placeholderPrefix">Prefix to use for the placeholders</param>
        /// <param name="dataKey">Data key in <see cref="PlaceholderData"/></param>
        public static void RegisterPlaceholders(Plugin plugin, string placeholderPrefix, string dataKey = nameof(DiscordMessage))
        {
            DiscordPlaceholders placeholders = DiscordPlaceholders.Instance;
            placeholders.RegisterPlaceholder<DiscordMessage, Snowflake>(plugin, $"{placeholderPrefix}.id", dataKey, Id);
            placeholders.RegisterPlaceholder<DiscordMessage, Snowflake>(plugin, $"{placeholderPrefix}.channel.id", dataKey, ChannelId);
            placeholders.RegisterPlaceholder<DiscordMessage, string>(plugin, $"{placeholderPrefix}.content", dataKey, Content);
        }
    }
}