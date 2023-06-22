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
        public static string Id(IPlayer player) => player.Id;
        
        /// <summary>
        /// <see cref="IPlayer.Name"/> placeholder
        /// </summary>
        public static string Name(IPlayer player) => player.Name;
        
        /// <summary>
        /// <see cref="IPlayer.Health"/> placeholder
        /// </summary>
        public static float Health(IPlayer player) => player.Health;
        
        /// <summary>
        /// <see cref="IPlayer.MaxHealth"/> placeholder
        /// </summary>
        public static float MaxHealth(IPlayer player) => player.MaxHealth;
        
        /// <summary>
        /// <see cref="IPlayer.Position()"/> placeholder
        /// </summary>
        public static GenericPosition Position(IPlayer player) => player.Position();
        
        /// <summary>
        /// <see cref="IPlayer.Ping"/> placeholder
        /// </summary>
        public static int Ping(IPlayer player) => player.Ping;

        /// <summary>
        /// <see cref="IPlayer.Ping"/> placeholder
        /// </summary>
        public static string SteamProfileUrl(IPlayer player) => $"https://steamcommunity.com/profiles/{player.Id}";

        /// <summary>
        /// <see cref="IPlayer.Ping"/> placeholder
        /// </summary>
        public static string SteamAvatarUrl(IPlayer player) => DiscordExtensionCore.Instance.GetPlayerAvatarUrl(player.Id);
        
        /// <summary>
        /// <see cref="PlayerExt.IsLinked"/> placeholder
        /// </summary>
        public static bool IsLinked(PlaceholderState state, IPlayer player) => player.IsLinked();

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
            placeholders.RegisterPlaceholder<IPlayer, string>(plugin, $"{placeholderPrefix}.id", dataKey, Id);
            placeholders.RegisterPlaceholder<IPlayer, string>(plugin, $"{placeholderPrefix}.name", dataKey, Name);
            placeholders.RegisterPlaceholder<IPlayer, float>(plugin, $"{placeholderPrefix}.health", dataKey, Health);
            placeholders.RegisterPlaceholder<IPlayer, float>(plugin, $"{placeholderPrefix}.health.max", dataKey, MaxHealth);
            placeholders.RegisterPlaceholder<IPlayer, GenericPosition>(plugin, $"{placeholderPrefix}.position", dataKey, Position);
            placeholders.RegisterPlaceholder<IPlayer, int>(plugin, $"{placeholderPrefix}.ping", dataKey, Ping);
            placeholders.RegisterPlaceholder<IPlayer, string>(plugin, $"{placeholderPrefix}.steam.profile", dataKey, SteamProfileUrl);
            placeholders.RegisterPlaceholder<IPlayer, string>(plugin, $"{placeholderPrefix}.steam.avatar", dataKey, SteamAvatarUrl);
            placeholders.RegisterPlaceholder<IPlayer, bool>(plugin, $"{placeholderPrefix}.islinked", dataKey, IsLinked);
        }
    }
}