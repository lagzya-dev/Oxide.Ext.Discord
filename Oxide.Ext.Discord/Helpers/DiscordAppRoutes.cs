using System;
using Oxide.Ext.Discord.Interfaces;

namespace Oxide.Ext.Discord.Helpers
{
    /// <summary>
    /// Discord App Routes for navigating the client within the discord app using links
    /// Sourced from: https://gist.github.com/ghostrider-05/8f1a0bfc27c7c4509b4ea4e8ce718af0
    /// </summary>
    public static class DiscordAppRoutes
    {
        private const string BaseUrl = "discord://-";

        /// <summary>
        /// User App Route
        /// </summary>
        public static string User(IKey userId) => $"{BaseUrl}/users/{userId.ToString()}";

        /// <summary>
        /// Message Requests App Route
        /// </summary>
        public const string MessageRequests = BaseUrl + "/message-requests";
        
        /// <summary>
        /// General App Routes
        /// </summary>
        public static class General
        {
            /// <summary>
            /// Apps App Route
            /// </summary>
            public const string Apps = BaseUrl + "/apps";
            
            /// <summary>
            /// Guild Discovery App Route
            /// </summary>
            public const string GuildDiscovery = BaseUrl + "/guild-discovery";
            
            /// <summary>
            /// Create Guild App Route
            /// </summary>
            public const string CreateGuild = BaseUrl + "/guilds/create";

            /// <summary>
            /// Gift App Route
            /// </summary>
            public static string Gift(string code) => $"{BaseUrl}/gifts/{code}";
            
            /// <summary>
            /// Server Invite App Route
            /// </summary>
            public static string ServerInvite(string code) => $"{BaseUrl}/invite/{code}";

            /// <summary>
            /// App Settings Page Routes
            /// </summary>
            /// <exception cref="ArgumentOutOfRangeException"></exception>
            public static string Settings(SettingsPage page)
            {
                string path;
                switch (page)
                {
                    case SettingsPage.Account:
                        path = "account";
                        break;
                    case SettingsPage.ProfileCustomization:
                        path = "profile-customization";
                        break;
                    case SettingsPage.PrivacyAndSafety:
                        path = "privacy-and-safety";
                        break;
                    case SettingsPage.AuthorizedApps:
                        path = "authorized-apps";
                        break;
                    case SettingsPage.Connections:
                        path = "connections";
                        break;
                    case SettingsPage.Premium:
                        path = "premium";
                        break;
                    case SettingsPage.PremiumGuildSubscriptions:
                        path = "premium-guild-subscription";
                        break;
                    case SettingsPage.Subscriptions:
                        path = "subscriptions";
                        break;
                    case SettingsPage.Inventory:
                        path = "inventory";
                        break;
                    case SettingsPage.Billing:
                        path = "billing";
                        break;
                    case SettingsPage.Appearance:
                        path = "appearance";
                        break;
                    case SettingsPage.Accessibility:
                        path = "accessibility";
                        break;
                    case SettingsPage.Voice:
                        path = "voice";
                        break;
                    case SettingsPage.Text:
                        path = "text";
                        break;
                    case SettingsPage.Notifications:
                        path = "notifications";
                        break;
                    case SettingsPage.Keybinds:
                        path = "keybinds";
                        break;
                    case SettingsPage.Locale:
                        path = "locale";
                        break;
                    case SettingsPage.Windows:
                        path = "windows";
                        break;
                    case SettingsPage.Linux:
                        path = "linux";
                        break;
                    case SettingsPage.StreamerMode:
                        path = "streamer-mode";
                        break;
                    case SettingsPage.Advanced:
                        path = "advanced";
                        break;
                    case SettingsPage.ActivityStatus:
                        path = "activity-status";
                        break;
                    case SettingsPage.Overlay:
                        path = "overlay";
                        break;
                    case SettingsPage.HypesquadOnline:
                        path = "hypesquad-online";
                        break;
                    case SettingsPage.Changelogs:
                        path = "changelogs";
                        break;
                    case SettingsPage.Experiments:
                        path = "experiments";
                        break;
                    case SettingsPage.DeveloperOptions:
                        path = "developer-options";
                        break;
                    case SettingsPage.HotspotOptions:
                        path = "hotspot-options";
                        break;
                    case SettingsPage.DismissibleContentOptions:
                        path = "dismissible-content-options";
                        break;
                    case SettingsPage.FamilyCenter:
                        path = "family-center";
                        break;
                    case SettingsPage.Sessions:
                        path = "sessions";
                        break;
                    case SettingsPage.FriendRequests:
                        path = "friend-requests";
                        break;
                    case SettingsPage.RegisteredGames:
                        path = "registered-games";
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(page), page, null);
                }

                return $"{BaseUrl}/{path}";
            }
        }

        /// <summary>
        /// DM App routes
        /// </summary>
        public static class DMs
        {
            /// <summary>
            /// DM Channel App Route
            /// </summary>
            public static string Channel(IKey channelId) => $"{BaseUrl}/channels/@me/{channelId.ToString()}";
            
            /// <summary>
            /// DM Message App Route
            /// </summary>
            public static string Message(IKey channelId, IKey messageId) => $"{BaseUrl}/channels/@me/{channelId.ToString()}/{messageId.ToString()}";
        }

        /// <summary>
        /// Guild App Routes
        /// </summary>
        public static class Guilds
        {
            /// <summary>
            /// Favorite Channels App Route
            /// </summary>
            public const string Favorites = BaseUrl + "/channels/@favorites";
            
            /// <summary>
            /// Favorites Channel App Route
            /// </summary>
            public static string FavoritesChannel(IKey channelId) => $"{BaseUrl}/channels/@favorites/{channelId.ToString()}";
            
            /// <summary>
            /// Guild App Route
            /// </summary>
            public static string Guild(IKey guildId) => $"{BaseUrl}/channels/{guildId.ToString()}";
            
            /// <summary>
            /// Guild Channel App Route
            /// </summary>
            public static string Channel(IKey guildId, IKey channelId) => $"{BaseUrl}/channels/{guildId.ToString()}/{channelId.ToString()}";
            
            /// <summary>
            /// Browse Guild Channels App Route
            /// </summary>
            public static string BrowseChannel(IKey guildId) => $"{BaseUrl}/channels/{guildId.ToString()}/channel-browser";
            
            /// <summary>
            /// Server Guide App Route
            /// </summary>
            public static string ServerGuide(IKey guildId) => $"{BaseUrl}/channels/{guildId.ToString()}/@home";
            
            /// <summary>
            /// Event App Route
            /// </summary>
            public static string Event(IKey guildId, IKey eventId) => $"{BaseUrl}/events/{guildId.ToString()}/{eventId}";
            
            /// <summary>
            /// Message App Route
            /// </summary>
            public static string Message(IKey guildId, IKey channelId, IKey messageId) => $"{BaseUrl}/channels/{guildId.ToString()}/{channelId.ToString()}/{messageId.ToString()}";
            
            /// <summary>
            /// Membership Screening App Route
            /// </summary>
            public static string MembershipScreening(IKey guildId) => $"{BaseUrl}/member-verification/{guildId.ToString()}";
            
            /// <summary>
            /// Role Subscriptions App Route
            /// </summary>
            public static string RoleSubscriptions(IKey guildId) => $"{BaseUrl}/guild-role-subscriptions/{guildId.ToString()}";
        }
    }
}