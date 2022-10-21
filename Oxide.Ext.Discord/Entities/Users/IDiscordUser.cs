using Oxide.Ext.Discord.Entities.Permissions;

namespace Oxide.Ext.Discord.Entities.Users
{
    /// <summary>
    /// Represents the fields for a DiscordUser
    /// </summary>
    public interface IDiscordUser
    {
        /// <summary>
        /// The user's id
        /// </summary>
        Snowflake Id { get; set; }

        /// <summary>
        /// The user's username, not unique across the platform
        /// </summary>
        string Username { get; set; }

        /// <summary>
        /// The user's 4-digit discord-tag
        /// </summary>
        string Discriminator { get; set; }

        /// <summary>
        /// The user's avatar hash
        /// </summary>
        string Avatar { get; set; }

        /// <summary>
        /// Whether the user belongs to an OAuth2 application
        /// </summary>
        bool? Bot { get; set; }

        /// <summary>
        /// Whether the user is an Official Discord System user (part of the urgent message system)
        /// </summary>
        bool? System { get; set; }

        /// <summary>
        /// Whether the user has two factor enabled on their account
        /// </summary>
        bool? MfaEnabled { get; set; }

        /// <summary>
        /// The user's banner, or null if unset
        /// </summary>
        string Banner { get; set; }

        /// <summary>
        /// The user's banner color encoded as an integer representation of hexadecimal color code
        /// </summary>
        DiscordColor? AccentColor { get; set; }

        /// <summary>
        /// The user's chosen language option
        /// </summary>
        string Locale { get; set; }

        /// <summary>
        /// Whether the email on this account has been verified
        /// </summary>
        bool? Verified { get; set; }

        /// <summary>
        /// The user's email
        /// </summary>
        string Email { get; set; }

        /// <summary>
        /// The flags on a user's account
        /// <see cref="UserFlags"/>
        /// </summary>
        UserFlags? Flags { get; set; }

        /// <summary>
        /// The type of Nitro subscription on a user's account
        /// <see cref="UserPremiumType"/>
        /// </summary>
        UserPremiumType? PremiumType { get; set; }

        /// <summary>
        /// The public flags on a user's account
        /// <see cref="UserFlags"/>
        /// </summary>
        UserFlags? PublicFlags { get; set; }
    }
}