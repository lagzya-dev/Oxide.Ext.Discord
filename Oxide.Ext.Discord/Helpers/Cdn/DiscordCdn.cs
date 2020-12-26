using System;
using Oxide.Ext.Discord.Entities;

namespace Oxide.Ext.Discord.Helpers.Cdn
{
    public static class DiscordCdn
    {
        public const string CdnUrl = "https://cdn.discordapp.com";

        public static string GetCustomEmojiUrl(Snowflake emojiId, ImageFormat format = ImageFormat.Auto)
        {
            if (format == ImageFormat.Jpg || format == ImageFormat.WebP)
            {
                throw new ArgumentException("ImageFormat is not valid for CustomEmoji. Valid types are (Auto, Png, Gif)", nameof(format));
            }

            return $"{CdnUrl}/emojis/{emojiId}.{GetExtension(format, emojiId.ToString())}";
        }
        
        public static string GetGuildIconUrl(Snowflake guildId, string guildIcon, ImageFormat format = ImageFormat.Auto)
        {
            return $"{CdnUrl}/icons/{guildId}/{guildIcon}.{GetExtension(format, guildIcon)}";
        }
        
        public static string GetGuildSplashUrl(Snowflake guildId, string guildSplash, ImageFormat format = ImageFormat.Auto)
        {
            if (format == ImageFormat.Gif)
            {
                throw new ArgumentException("ImageFormat is not valid for CustomEmoji. Valid types are (Auto, Png, Jpeg, WebP)", nameof(format));
            }
            
            return $"{CdnUrl}/splashes/{guildId}/{guildSplash}.{GetExtension(format, guildSplash)}";
        }
        
        public static string GetGuildDiscoverySplashUrl(Snowflake guildId, string guildDiscoverySplash, ImageFormat format = ImageFormat.Auto)
        {
            if (format == ImageFormat.Gif)
            {
                throw new ArgumentException("ImageFormat is not valid for CustomEmoji. Valid types are (Auto, Png, Jpeg, WebP)", nameof(format));
            }
            
            return $"{CdnUrl}/discovery-splashes/{guildId}/{guildDiscoverySplash}.{GetExtension(format, guildDiscoverySplash)}";
        }
        
        public static string GetGuildBannerUrl(Snowflake guildId, string guildBanner, ImageFormat format = ImageFormat.Auto)
        {
            if (format == ImageFormat.Gif)
            {
                throw new ArgumentException("ImageFormat is not valid for CustomEmoji. Valid types are (Auto, Png, Jpeg, WebP)", nameof(format));
            }
            
            return $"{CdnUrl}/banners/{guildId}/{guildBanner}.{GetExtension(format, guildBanner)}";
        }
        
        public static string GetUserDefaultAvatarUrl(Snowflake userId, string userDiscriminator)
        {
            uint discriminator = uint.Parse(userDiscriminator) % 5;
            return $"{CdnUrl}/embed/avatars/{userId}/{discriminator}.png";
        }
        
        public static string GetUserAvatarUrl(Snowflake userId, string userAvatar, ImageFormat format = ImageFormat.Auto)
        {
            return $"{CdnUrl}/avatars/{userId}/{userAvatar}.{GetExtension(format, userAvatar)}";
        }
        
        public static string GetApplicationIconUrl(Snowflake applicationId, string icon, ImageFormat format = ImageFormat.Auto)
        {
            if (format == ImageFormat.Gif)
            {
                throw new ArgumentException("ImageFormat is not valid for CustomEmoji. Valid types are (Auto, Png, Jpeg, WebP)", nameof(format));
            }
            
            return $"{CdnUrl}/app-icons/{applicationId}/{icon}.{GetExtension(format, icon)}";
        }
        
        public static string GetApplicationAssetUrl(Snowflake applicationId, Snowflake assetId, ImageFormat format = ImageFormat.Auto)
        {
            if (format == ImageFormat.Gif)
            {
                throw new ArgumentException("ImageFormat is not valid for CustomEmoji. Valid types are (Auto, Png, Jpeg, WebP)", nameof(format));
            }
            
            return $"{CdnUrl}/app-assets/{applicationId}/{assetId}.{GetExtension(format, assetId.ToString())}";
        }
        
        public static string GetAchievementIconUrl(Snowflake applicationId, Snowflake achievementId, string iconHash, ImageFormat format = ImageFormat.Auto)
        {
            if (format == ImageFormat.Gif)
            {
                throw new ArgumentException("ImageFormat is not valid for CustomEmoji. Valid types are (Auto, Png, Jpeg, WebP)", nameof(format));
            }
            
            return $"{CdnUrl}/app-assets/{applicationId}/achievements/{achievementId}/icons/{iconHash}.{GetExtension(format, iconHash)}";
        }
        
        public static string GetTeamIconUrl(Snowflake teamId, string teamIcon, ImageFormat format = ImageFormat.Auto)
        {
            if (format == ImageFormat.Gif)
            {
                throw new ArgumentException("ImageFormat is not valid for CustomEmoji. Valid types are (Auto, Png, Jpeg, WebP)", nameof(format));
            }
            
            return $"{CdnUrl}/team-icons/{teamId}/{teamIcon}.{GetExtension(format, teamIcon)}";
        }

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