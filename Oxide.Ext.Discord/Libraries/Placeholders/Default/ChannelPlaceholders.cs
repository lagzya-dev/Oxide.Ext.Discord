using System.Text;
using Oxide.Core.Plugins;
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
        public static void Id(StringBuilder builder, PlaceholderState state, DiscordChannel channel) => PlaceholderFormatting.Replace(builder, state, channel.Id);
        
        /// <summary>
        /// <see cref="DiscordChannel.Name"/> placeholder
        /// </summary>
        public static void Name(StringBuilder builder, PlaceholderState state, DiscordChannel channel) => PlaceholderFormatting.Replace(builder, state, channel.Name);
        
        /// <summary>
        /// <see cref="DiscordChannel.IconUrl"/> placeholder
        /// </summary>
        public static void Icon(StringBuilder builder, PlaceholderState state, DiscordChannel channel) => PlaceholderFormatting.Replace(builder, state, channel.IconUrl);
        
        /// <summary>
        /// <see cref="DiscordChannel.Topic"/> placeholder
        /// </summary>
        public static void Topic(StringBuilder builder, PlaceholderState state, DiscordChannel channel) => PlaceholderFormatting.Replace(builder, state, channel.Topic);
        
        /// <summary>
        /// <see cref="DiscordChannel.Mention"/> placeholder
        /// </summary>
        public static void Mention(StringBuilder builder, PlaceholderState state, DiscordChannel channel) => PlaceholderFormatting.Replace(builder, state, channel.Mention);

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
            DiscordPlaceholders placeholders = DiscordExtension.DiscordPlaceholders;
            placeholders.RegisterPlaceholder<DiscordChannel>(plugin, $"{placeholderPrefix}.id", dataKey, Id);
            placeholders.RegisterPlaceholder<DiscordChannel>(plugin, $"{placeholderPrefix}.name", dataKey, Name);
            placeholders.RegisterPlaceholder<DiscordChannel>(plugin, $"{placeholderPrefix}.icon", dataKey, Icon);
            placeholders.RegisterPlaceholder<DiscordChannel>(plugin, $"{placeholderPrefix}.topic", dataKey, Topic);
            placeholders.RegisterPlaceholder<DiscordChannel>(plugin, $"{placeholderPrefix}.mention", dataKey, Mention);
        }
    }
}