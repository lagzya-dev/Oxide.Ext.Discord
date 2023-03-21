using System.Text;
using Oxide.Core.Libraries.Covalence;
using Oxide.Core.Plugins;
using Oxide.Ext.Discord.Extensions;
using Oxide.Ext.Discord.Plugins.Core;

namespace Oxide.Ext.Discord.Libraries.Placeholders.Default
{
    /// <summary>
    /// <see cref="IPlayer"/> placeholders
    /// </summary>
    public static class PlayerPlaceholders
    {
        /// <summary>
        /// <see cref="IPlayer.Id"/> placeholder
        /// </summary>
        public static void Id(StringBuilder builder, PlaceholderState state, IPlayer player) => PlaceholderFormatting.Replace(builder, state, player.Id);
        
        /// <summary>
        /// <see cref="IPlayer.Name"/> placeholder
        /// </summary>
        public static void Name(StringBuilder builder, PlaceholderState state, IPlayer player) => PlaceholderFormatting.Replace(builder, state, player.Name);
        
        /// <summary>
        /// <see cref="IPlayer.Health"/> placeholder
        /// </summary>
        public static void Health(StringBuilder builder, PlaceholderState state, IPlayer player) => PlaceholderFormatting.Replace(builder, state, player.Health);
        
        /// <summary>
        /// <see cref="IPlayer.MaxHealth"/> placeholder
        /// </summary>
        public static void MaxHealth(StringBuilder builder, PlaceholderState state, IPlayer player) => PlaceholderFormatting.Replace(builder, state, player.MaxHealth);
        
        /// <summary>
        /// <see cref="IPlayer.Position()"/> placeholder
        /// </summary>
        public static void Position(StringBuilder builder, PlaceholderState state, IPlayer player) => PlaceholderFormatting.Replace(builder, state, player.Position());
        
        /// <summary>
        /// <see cref="IPlayer.Ping"/> placeholder
        /// </summary>
        public static void Ping(StringBuilder builder, PlaceholderState state, IPlayer player) => PlaceholderFormatting.Replace(builder, state, player.Ping);
        
        /// <summary>
        /// <see cref="PlayerExt.IsLinked"/> placeholder
        /// </summary>
        public static void IsLinked(StringBuilder builder, PlaceholderState state, IPlayer player) => PlaceholderFormatting.Replace(builder, state, player.IsLinked());

        internal static void RegisterPlaceholders()
        {
            RegisterPlaceholders(DiscordExtensionCore.Instance, "player");
        }
        
        /// <summary>
        /// Registers placeholders for the given plugin. 
        /// </summary>
        /// <param name="plugin">Plugin to register placeholders for</param>
        /// <param name="placeholderPrefix">Prefix to use for the placeholders</param>
        /// <param name="dataKey">Data key in <see cref="PlaceholderData"/></param>
        public static void RegisterPlaceholders(Plugin plugin, string placeholderPrefix, string dataKey = nameof(IPlayer))
        {
            DiscordPlaceholders placeholders = DiscordPlaceholders.Instance;
            placeholders.RegisterPlaceholder<IPlayer>(plugin, $"{placeholderPrefix}.id", dataKey, Id);
            placeholders.RegisterPlaceholder<IPlayer>(plugin, $"{placeholderPrefix}.name", dataKey, Name);
            placeholders.RegisterPlaceholder<IPlayer>(plugin, $"{placeholderPrefix}.health", dataKey, Health);
            placeholders.RegisterPlaceholder<IPlayer>(plugin, $"{placeholderPrefix}.health.max", dataKey, MaxHealth);
            placeholders.RegisterPlaceholder<IPlayer>(plugin, $"{placeholderPrefix}.position", dataKey, Position);
            placeholders.RegisterPlaceholder<IPlayer>(plugin, $"{placeholderPrefix}.ping", dataKey, Ping);
            placeholders.RegisterPlaceholder<IPlayer>(plugin, $"{placeholderPrefix}.islinked", dataKey, IsLinked);
        }
    }
}