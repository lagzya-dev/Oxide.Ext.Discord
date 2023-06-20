using Oxide.Core.Plugins;
using Oxide.Ext.Discord.Entities;
using Oxide.Ext.Discord.Entities.Channels;
using Oxide.Ext.Discord.Plugins.Core;

namespace Oxide.Ext.Discord.Libraries.Placeholders.Default
{
    /// <summary>
    /// <see cref="DiscordChannel"/> Placeholders
    /// </summary>
    public static class ChannelPlaceholders
    {
        /// <summary>
        /// <see cref="DiscordChannel.Id"/> placeholder
        /// </summary>
        public static Snowflake Id(DiscordChannel channel) => channel.Id;
        
        /// <summary>
        /// <see cref="DiscordChannel.Name"/> placeholder
        /// </summary>
        public static string Name(DiscordChannel channel) => channel.Name;
        
        /// <summary>
        /// <see cref="DiscordChannel.IconUrl"/> placeholder
        /// </summary>
        public static string Icon(DiscordChannel channel) => channel.IconUrl;
        
        /// <summary>
        /// <see cref="DiscordChannel.Topic"/> placeholder
        /// </summary>
        public static string Topic(DiscordChannel channel) => channel.Topic;
        
        /// <summary>
        /// <see cref="DiscordChannel.Mention"/> placeholder
        /// </summary>
        public static string Mention(DiscordChannel channel) => channel.Mention;

        internal static void RegisterPlaceholders()
        {
            RegisterPlaceholders(DiscordExtensionCore.Instance, "channel");
        }
        
        /// <summary>
        /// Registers placeholders for the given plugin. 
        /// </summary>
        /// <param name="plugin">Plugin to register placeholders for</param>
        /// <param name="placeholderPrefix">Prefix to use for the placeholders</param>
        /// <param name="dataKey">Data key in <see cref="PlaceholderData"/></param>
        public static void RegisterPlaceholders(Plugin plugin, string placeholderPrefix, string dataKey = nameof(DiscordChannel))
        {
            DiscordPlaceholders placeholders = DiscordPlaceholders.Instance;
            placeholders.RegisterPlaceholder<DiscordChannel, Snowflake>(plugin, $"{placeholderPrefix}.id", dataKey, Id);
            placeholders.RegisterPlaceholder<DiscordChannel, string>(plugin, $"{placeholderPrefix}.name", dataKey, Name);
            placeholders.RegisterPlaceholder<DiscordChannel, string>(plugin, $"{placeholderPrefix}.icon", dataKey, Icon);
            placeholders.RegisterPlaceholder<DiscordChannel, string>(plugin, $"{placeholderPrefix}.topic", dataKey, Topic);
            placeholders.RegisterPlaceholder<DiscordChannel, string>(plugin, $"{placeholderPrefix}.mention", dataKey, Mention);
        }
    }
}