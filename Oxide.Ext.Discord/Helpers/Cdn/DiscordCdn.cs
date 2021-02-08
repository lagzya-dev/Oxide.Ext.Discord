using System;
using Oxide.Ext.Discord.Entities;

namespace Oxide.Ext.Discord.Helpers.Cdn
{
    /// <summary>
    /// Represents Discord <a href="https://discord.com/developers/docs/reference#image-formatting-cdn-endpoints">CDN Endpoints</a>
    /// </summary>
    public static class DiscordCdn
    {
        /// <summary>
        /// Base CDN Url
        /// </summary>
        public const string CdnUrl = "https://cdn.discordapp.com";

        /// <summary>
        /// Returns the Url to the custom emoji
        /// </summary>
        /// <param name="emojiId">ID of the emoji</param>
        /// <param name="format">The format the emoji is in</param>
        /// <returns>Url of the emoji</returns>
        /// <exception cref="ArgumentException">Thrown if format is Jpg or WebP</exception>
        public static string GetCustomEmojiUrl(Snowflake emojiId, ImageFormat format = ImageFormat.Auto)
        {
            if (format == ImageFormat.Jpg || format == ImageFormat.WebP)
            {
                throw new ArgumentException("ImageFormat is not valid for Custom Emoji. Valid types are (Auto, Png, Gif)", nameof(format));
            }

            return $"{CdnUrl}/emojis/{emojiId}.{GetExtension(format, emojiId.ToString())}";
        }
        
        /// <summary>
        /// Returns the Url to the Guild Icon
        /// </summary>
        /// <param name="guildId">Guild ID for the icon</param>
        /// <param name="guildIcon">Guild Icon from guild</param>
        /// <param name="format">Format the icon is in</param>
        /// <returns>Url of the guild icon</returns>
        public static string GetGuildIconUrl(Snowflake guildId, string guildIcon, ImageFormat format = ImageFormat.Auto)
        {
            return $"{CdnUrl}/icons/{guildId}/{guildIcon}.{GetExtension(format, guildIcon)}";
        }
        
        /// <summary>
        /// Returns the Url of the Guild Splash
        /// </summary>
        /// <param name="guildId">Guild ID for the icon</param>
        /// <param name="guildSplash">Guild Splash from guild</param>
        /// <param name="format">Format the icon is in</param>
        /// <returns>Url of the guild splash</returns>
        /// <exception cref="ArgumentException">Thrown if format is Gif</exception>
        public static string GetGuildSplashUrl(Snowflake guildId, string guildSplash, ImageFormat format = ImageFormat.Auto)
        {
            if (format == ImageFormat.Gif)
            {
                throw new ArgumentException("ImageFormat is not valid for Guild Splash. Valid types are (Auto, Png, Jpeg, WebP)", nameof(format));
            }
            
            return $"{CdnUrl}/splashes/{guildId}/{guildSplash}.{GetExtension(format, guildSplash)}";
        }
        
        /// <summary>
        /// Return the Url of the Guild Discovery Splash
        /// </summary>
        /// <param name="guildId">Guild ID for the icon</param>
        /// <param name="guildDiscoverySplash">Guild Discovery Splash from guild</param>
        /// <param name="format">Format the icon is in</param>
        /// <returns>Url of the guild discovery splash</returns>
        /// <exception cref="ArgumentException">Thrown if format is Gif</exception>
        public static string GetGuildDiscoverySplashUrl(Snowflake guildId, string guildDiscoverySplash, ImageFormat format = ImageFormat.Auto)
        {
            if (format == ImageFormat.Gif)
            {
                throw new ArgumentException("ImageFormat is not valid for Guild Discovery Splash. Valid types are (Auto, Png, Jpeg, WebP)", nameof(format));
            }
            
            return $"{CdnUrl}/discovery-splashes/{guildId}/{guildDiscoverySplash}.{GetExtension(format, guildDiscoverySplash)}";
        }
        
        /// <summary>
        /// Returns the Url of the Guild Banner
        /// </summary>
        /// <param name="guildId">Guild ID for the icon</param>
        /// <param name="guildBanner">Guild Banner from guild</param>
        /// <param name="format">Format the icon is in</param>
        /// <returns>Url of the guild banner</returns>
        /// <exception cref="ArgumentException">Thrown if format is Gif</exception>
        public static string GetGuildBannerUrl(Snowflake guildId, string guildBanner, ImageFormat format = ImageFormat.Auto)
        {
            if (format == ImageFormat.Gif)
            {
                throw new ArgumentException("ImageFormat is not valid for Guild Banner. Valid types are (Auto, Png, Jpeg, WebP)", nameof(format));
            }
            
            return $"{CdnUrl}/banners/{guildId}/{guildBanner}.{GetExtension(format, guildBanner)}";
        }
        
        /// <summary>
        /// Returns the Url of the users default avatar
        /// </summary>
        /// <param name="userId">Discord User ID</param>
        /// <param name="userDiscriminator">Discord User Discriminator</param>
        /// <returns>Url of the default avatar url</returns>
        public static string GetUserDefaultAvatarUrl(Snowflake userId, string userDiscriminator)
        {
            uint discriminator = uint.Parse(userDiscriminator) % 5;
            return $"{CdnUrl}/embed/avatars/{userId}/{discriminator}.png";
        }
        
        /// <summary>
        /// Returns the Url of the users avatar
        /// </summary>
        /// <param name="userId">Discord User ID</param>
        /// <param name="userAvatar">User avatar from user</param>
        /// <param name="format">Format the avatar is in</param>
        /// <returns>Url of the users avatar</returns>
        public static string GetUserAvatarUrl(Snowflake userId, string userAvatar, ImageFormat format = ImageFormat.Auto)
        {
            return $"{CdnUrl}/avatars/{userId}/{userAvatar}.{GetExtension(format, userAvatar)}";
        }
        
        /// <summary>
        /// Returns the url to the application icon
        /// </summary>
        /// <param name="applicationId">Application ID</param>
        /// <param name="icon">Icon field from application</param>
        /// <param name="format">Format the icon is in</param>
        /// <returns>Url of the application icon</returns>
        /// <exception cref="ArgumentException">Throw if format is Gif</exception>
        public static string GetApplicationIconUrl(Snowflake applicationId, string icon, ImageFormat format = ImageFormat.Auto)
        {
            if (format == ImageFormat.Gif)
            {
                throw new ArgumentException("ImageFormat is not valid for Application Icon. Valid types are (Auto, Png, Jpeg, WebP)", nameof(format));
            }
            
            return $"{CdnUrl}/app-icons/{applicationId}/{icon}.{GetExtension(format, icon)}";
        }
        
        /// <summary>
        /// Returns the applications asset icon url
        /// </summary>
        /// <param name="applicationId">Application ID of the icon</param>
        /// <param name="assetId">Asset ID for the application</param>
        /// <param name="format">Format the icon is in</param>
        /// <returns>Url of the application asset icon</returns>
        /// <exception cref="ArgumentException">Throw if format is Gif</exception>
        public static string GetApplicationAssetUrl(Snowflake applicationId, string assetId, ImageFormat format = ImageFormat.Auto)
        {
            if (format == ImageFormat.Gif)
            {
                throw new ArgumentException("ImageFormat is not valid for Application Asset. Valid types are (Auto, Png, Jpeg, WebP)", nameof(format));
            }
            
            return $"{CdnUrl}/app-assets/{applicationId}/{assetId}.{GetExtension(format, assetId.ToString())}";
        }
        
        /// <summary>
        /// Returns the Url of the Achievement Icon
        /// </summary>
        /// <param name="applicationId">Application ID of the icon</param>
        /// <param name="achievementId">Achievement ID</param>
        /// <param name="iconHash">Achievement Icon Hash</param>
        /// <param name="format">Format the icon is in</param>
        /// <returns>Url of the achievement icon</returns>
        /// <exception cref="ArgumentException">Throw if format is Gif</exception>
        public static string GetAchievementIconUrl(Snowflake applicationId, Snowflake achievementId, string iconHash, ImageFormat format = ImageFormat.Auto)
        {
            if (format == ImageFormat.Gif)
            {
                throw new ArgumentException("ImageFormat is not valid for Achievement Icon. Valid types are (Auto, Png, Jpeg, WebP)", nameof(format));
            }
            
            return $"{CdnUrl}/app-assets/{applicationId}/achievements/{achievementId}/icons/{iconHash}.{GetExtension(format, iconHash)}";
        }
        
        /// <summary>
        /// Returns the Url of the Team Icon
        /// </summary>
        /// <param name="teamId">Team ID of the Icon</param>
        /// <param name="teamIcon">Icon field from Team</param>
        /// <param name="format">Format the icon is in</param>
        /// <returns>Url of the achievement icon</returns>
        /// <exception cref="ArgumentException">Throw if format is Gif</exception>
        public static string GetTeamIconUrl(Snowflake teamId, string teamIcon, ImageFormat format = ImageFormat.Auto)
        {
            if (format == ImageFormat.Gif)
            {
                throw new ArgumentException("ImageFormat is not valid for Team Icon. Valid types are (Auto, Png, Jpeg, WebP)", nameof(format));
            }
            
            return $"{CdnUrl}/team-icons/{teamId}/{teamIcon}.{GetExtension(format, teamIcon)}";
        }

        /// <summary>
        /// Returns the extension to use for the image
        /// </summary>
        /// <param name="format">Image format that is requested</param>
        /// <param name="image">Image data from the field</param>
        /// <returns>Image extension for the image format and image data</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if Image Format is out of range</exception>
        public static string GetExtension(ImageFormat format, string image)
        {
            if (format == ImageFormat.Auto)
            {
                format = image.StartsWith("a_") ? ImageFormat.Gif : ImageFormat.Png;
            }

            switch (format)
            {
                case ImageFormat.Jpg:
                    return "jpeg";
                case ImageFormat.Png:
                    return "png";
                case ImageFormat.WebP:
                    return "webp";
                case ImageFormat.Gif:
                    return "gif";
                default:
                    throw new ArgumentOutOfRangeException(nameof(format), format, "Format is not a valid ImageFormat");
            }
        }
    }
}