using System.Text;
using Oxide.Core.Libraries.Covalence;
using Oxide.Core.Plugins;
using Oxide.Ext.Discord.Plugins.Core;

namespace Oxide.Ext.Discord.Libraries.Placeholders.Default
{
    /// <summary>
    /// <see cref="IServer"/> placeholders
    /// </summary>
    public static class ServerPlaceholders
    {
        /// <summary>
        /// <see cref="IServer.Name"/> placeholder
        /// </summary>
        public static void Name(StringBuilder builder, PlaceholderState state, IServer server) => PlaceholderFormatting.Replace(builder, state, server.Name);
        
        /// <summary>
        /// <see cref="IServer.Players"/> placeholder
        /// </summary>
        public static void Players(StringBuilder builder, PlaceholderState state, IServer server) => PlaceholderFormatting.Replace(builder, state, server.Players);
        
        /// <summary>
        /// <see cref="IServer.MaxPlayers"/> placeholder
        /// </summary>
        public static void MaxPlayers(StringBuilder builder, PlaceholderState state, IServer server) => PlaceholderFormatting.Replace(builder, state, server.MaxPlayers);
        
        /// <summary>
        /// <see cref="IServer.Version"/> placeholder
        /// </summary>
        public static void Version(StringBuilder builder, PlaceholderState state, IServer server) => PlaceholderFormatting.Replace(builder, state, server.Version);
        
        /// <summary>
        /// <see cref="IServer.Protocol"/> placeholder
        /// </summary>
        public static void Protocol(StringBuilder builder, PlaceholderState state, IServer server) => PlaceholderFormatting.Replace(builder, state, server.Protocol);
        
        internal static void RegisterPlaceholders()
        {
            RegisterPlaceholders(DiscordExtensionCore.Instance, "server");
        }
        
        /// <summary>
        /// Registers placeholders for the given plugin. 
        /// </summary>
        /// <param name="plugin">Plugin to register placeholders for</param>
        /// <param name="placeholderPrefix">Prefix to use for the placeholders</param>
        /// <param name="dataKey">Data key in <see cref="PlaceholderData"/></param>
        public static void RegisterPlaceholders(Plugin plugin, string placeholderPrefix, string dataKey = nameof(IServer))
        {
            DiscordPlaceholders placeholders = DiscordExtension.DiscordPlaceholders;
            placeholders.RegisterPlaceholder<IServer>(plugin, $"{placeholderPrefix}.name", dataKey, Name);
            placeholders.RegisterPlaceholder<IServer>(plugin, $"{placeholderPrefix}.players", dataKey, Players);
            placeholders.RegisterPlaceholder<IServer>(plugin, $"{placeholderPrefix}.players.max", dataKey, MaxPlayers);
            placeholders.RegisterPlaceholder<IServer>(plugin, $"{placeholderPrefix}.version", dataKey, Version);
            placeholders.RegisterPlaceholder<IServer>(plugin, $"{placeholderPrefix}.protocol", dataKey, Protocol);
        }
    }
}