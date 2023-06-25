using Oxide.Core;
using Oxide.Core.Libraries;
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
        internal const string TargetPlayerKey = "TargetPlayerKey";
        
        private static Permission _permission;
        private static Permission Permission => _permission ?? (_permission = Interface.Oxide.GetLibrary<Permission>());
        
        /// <summary>
        /// <see cref="IPlayer.Id"/> placeholder
        /// </summary>
        public static string Id(IPlayer player) => player.Id;
        
        /// <summary>
        /// <see cref="IPlayer.Name"/> placeholder
        /// </summary>
        public static string Name(IPlayer player) => player.Name;
        
        /// <summary>
        /// <see cref="IPlayer.IsConnected"/> placeholder
        /// </summary>
        public static bool Connected(IPlayer player) => player.IsConnected;
        
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
        
        public static string[] Permissions(IPlayer player) => Permission.GetUserPermissions(player.Id);
        public static string[] Groups(IPlayer player) => Permission.GetUserGroups(player.Id);

        /// <summary>
        /// Steam Profile Url Placeholder 
        /// </summary>
        public static string SteamProfileUrl(IPlayer player) => $"https://steamcommunity.com/profiles/{player.Id}";

        /// <summary>
        /// Steam Avatar Url Placeholder
        /// </summary>
        public static string SteamAvatarUrl(IPlayer player) => DiscordExtensionCore.Instance.GetPlayerAvatarUrl(player.Id);

        /// <summary>
        /// Battle metrics Steam ID Url Placeholder
        /// </summary>
        public static string BattleMetricsSteamIdUrl(IPlayer player) => $"https://www.battlemetrics.com/rcon/players?filter[search]={player.Id}";
        
        /// <summary>
        /// Battle metrics Place Name Url Placeholder
        /// </summary>
        public static string BattleMetricsNameUrl(IPlayer player) => $"https://www.battlemetrics.com/rcon/players?filter[search]={player.Name}";        
        /// <summary>
        /// Battle metrics Place Name Url Placeholder
        /// </summary>
        public static string ServerArmorUrl(IPlayer player) => $"https://io.serverarmour.com/profile/{player.Id}";
        
        /// <summary>
        /// <see cref="PlayerExt.IsLinked"/> placeholder
        /// </summary>
        public static bool IsLinked(PlaceholderState state, IPlayer player) => player.IsLinked();

        internal static void RegisterPlaceholders()
        {
            RegisterPlaceholders(DiscordExtensionCore.Instance, "player");
            RegisterPlaceholders(DiscordExtensionCore.Instance, "target", TargetPlayerKey);
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
            placeholders.RegisterPlaceholder<IPlayer, bool>(plugin, $"{placeholderPrefix}.connected", dataKey, Connected);
            placeholders.RegisterPlaceholder<IPlayer, string[]>(plugin, $"{placeholderPrefix}.permissions", dataKey, Permissions);
            placeholders.RegisterPlaceholder<IPlayer, string[]>(plugin, $"{placeholderPrefix}.groups", dataKey, Groups);
            placeholders.RegisterPlaceholder<IPlayer, float>(plugin, $"{placeholderPrefix}.health", dataKey, Health);
            placeholders.RegisterPlaceholder<IPlayer, float>(plugin, $"{placeholderPrefix}.health.max", dataKey, MaxHealth);
            placeholders.RegisterPlaceholder<IPlayer, GenericPosition>(plugin, $"{placeholderPrefix}.position", dataKey, Position);
            placeholders.RegisterPlaceholder<IPlayer, int>(plugin, $"{placeholderPrefix}.ping", dataKey, Ping);
            placeholders.RegisterPlaceholder<IPlayer, string>(plugin, $"{placeholderPrefix}.steam.profile", dataKey, SteamProfileUrl);
            placeholders.RegisterPlaceholder<IPlayer, string>(plugin, $"{placeholderPrefix}.steam.avatar", dataKey, SteamAvatarUrl);
            placeholders.RegisterPlaceholder<IPlayer, string>(plugin, $"{placeholderPrefix}.battlemetrics.steamid", dataKey, BattleMetricsSteamIdUrl);
            placeholders.RegisterPlaceholder<IPlayer, string>(plugin, $"{placeholderPrefix}.battlemetrics.name", dataKey, BattleMetricsNameUrl);
            placeholders.RegisterPlaceholder<IPlayer, string>(plugin, $"{placeholderPrefix}.serverarmor.profile", dataKey, ServerArmorUrl);
            placeholders.RegisterPlaceholder<IPlayer, bool>(plugin, $"{placeholderPrefix}.islinked", dataKey, IsLinked);
        }
    }
}