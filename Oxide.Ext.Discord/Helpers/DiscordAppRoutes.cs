using System;
using Oxide.Ext.Discord.Entities;

namespace Oxide.Ext.Discord.Helpers
{
    /// <summary>
    /// Discord App Routes for navigating the client within the discord app using links
    /// Sourced from: https://gist.github.com/ghostrider-05/8f1a0bfc27c7c4509b4ea4e8ce718af0
    /// </summary>
    public class DiscordAppRoutes
    {
        private const string BaseUrl = "discord://-";

        public string User(Snowflake userId) => $"{BaseUrl}/users/{userId.ToString()}";
        
        public class General
        {
            public const string Apps = BaseUrl + "/apps";
            public const string GuildDiscovery = BaseUrl + "/guild-discovery";
            public const string CreateGuild = BaseUrl + "/guilds/create";

            public string Gift(string code) => $"{BaseUrl}/gifts/{code}";
            public string ServerInvite(string code) => $"{BaseUrl}/invite/{code}";

            public string Settings(SettingsPage page)
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
                    default:
                        throw new ArgumentOutOfRangeException(nameof(page), page, null);
                }

                return $"{BaseUrl}/{path}";
            }
        }

        public class DMs
        {
            public string DmChannel(Snowflake channelId) => $"{BaseUrl}/channels/@me/{channelId.ToString()}";
            public string DmMessage(Snowflake channelId, Snowflake messageId) => $"{BaseUrl}/channels/@me/{channelId.ToString()}/{messageId.ToString()}";
        }

        public class Guilds
        {
            public const string Favorites = BaseUrl + "/channels/@favorites";
            public string FavoritesChannel(Snowflake channelId) => $"{BaseUrl}/channels/@favorites/{channelId.ToString()}";
            public string Guild(Snowflake guildId) => $"{BaseUrl}/channels/{guildId.ToString()}";
            public string GuildChannel(Snowflake guildId, Snowflake channelId) => $"{BaseUrl}/channels/{guildId.ToString()}/{channelId.ToString()}";
            public string GuildHomeChannel(Snowflake guildId) => $"{BaseUrl}/channels/{guildId.ToString()}/@home";
            public string GuildEvent(Snowflake guildId, Snowflake eventId) => $"{BaseUrl}/events/{guildId.ToString()}/{eventId}";
            public string GuildMessage(Snowflake guildId, Snowflake channelId, Snowflake messageId) => $"{BaseUrl}/channels/{guildId.ToString()}/{channelId.ToString()}/{messageId.ToString()}";
            public string GuildMembershipScreening(Snowflake guildId) => $"{BaseUrl}/member-verification/{guildId.ToString()}";
            public string GuildRoleSubscriptions(Snowflake guildId) => $"{BaseUrl}/guild-role-subscriptions/{guildId.ToString()}";
        }
    }
}