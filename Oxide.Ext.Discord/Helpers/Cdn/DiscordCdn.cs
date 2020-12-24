using System;

namespace Oxide.Ext.Discord.Helpers.Cdn
{
    public static class DiscordCdn
    {
        public const string CdnUrl = "https://cdn.discordapp.com";

        public static string GetCustomEmojiUrl(string emojiId, ImageFormat format = ImageFormat.Auto)
        {
            if (format == ImageFormat.Jpg || format == ImageFormat.WebP)
            {
                throw new ArgumentException("ImageFormat is not valid for CustomEmoji. Valid types are (Auto, Png, Gif)", nameof(format));
            }

            return $"{CdnUrl}/emojis/{emojiId}.{GetExtension(format, emojiId)}";
        }
        
        public static string GetGuildIconUrl(string guildId, string guildIcon, ImageFormat format = ImageFormat.Auto)
        {
            return $"{CdnUrl}/icons/{guildId}/{guildIcon}.{GetExtension(format, guildIcon)}";
        }
        
        public static string GetGuildSplashUrl(string guildId, string guildSplash, ImageFormat format = ImageFormat.Auto)
        {
            if (format == ImageFormat.Gif)
            {
                throw new ArgumentException("ImageFormat is not valid for CustomEmoji. Valid types are (Auto, Png, Jpeg, WebP)", nameof(format));
            }
            
            return $"{CdnUrl}/splashes/{guildId}/{guildSplash}.{GetExtension(format, guildSplash)}";
        }
        
        public static string GetGuildDiscoverySplashUrl(string guildId, string guildDiscoverySplash, ImageFormat format = ImageFormat.Auto)
        {
            if (format == ImageFormat.Gif)
            {
                throw new ArgumentException("ImageFormat is not valid for CustomEmoji. Valid types are (Auto, Png, Jpeg, WebP)", nameof(format));
            }
            
            return $"{CdnUrl}/discovery-splashes/{guildId}/{guildDiscoverySplash}.{GetExtension(format, guildDiscoverySplash)}";
        }
        
        public static string GetGuildBannerUrl(string guildId, string guildBanner, ImageFormat format = ImageFormat.Auto)
        {
            if (format == ImageFormat.Gif)
            {
                throw new ArgumentException("ImageFormat is not valid for CustomEmoji. Valid types are (Auto, Png, Jpeg, WebP)", nameof(format));
            }
            
            return $"{CdnUrl}/banners/{guildId}/{guildBanner}.{GetExtension(format, guildBanner)}";
        }
        
        public static string GetUserDefaultAvatarUrl(string userId, string userDiscriminator)
        {
            uint discriminator = uint.Parse(userDiscriminator) % 5;
            return $"{CdnUrl}/embed/avatars/{userId}/{discriminator}.png";
        }
        
        public static string GetUserAvatarUrl(string userId, string userAvatar, ImageFormat format = ImageFormat.Auto)
        {
            return $"{CdnUrl}/avatars/{userId}/{userAvatar}.{GetExtension(format, userAvatar)}";
        }
        
        public static string GetApplicationIconUrl(string applicationId, string icon, ImageFormat format = ImageFormat.Auto)
        {
            if (format == ImageFormat.Gif)
            {
                throw new ArgumentException("ImageFormat is not valid for CustomEmoji. Valid types are (Auto, Png, Jpeg, WebP)", nameof(format));
            }
            
            return $"{CdnUrl}/app-icons/{applicationId}/{icon}.{GetExtension(format, icon)}";
        }
        
        public static string GetApplicationAssetUrl(string applicationId, string assetId, ImageFormat format = ImageFormat.Auto)
        {
            if (format == ImageFormat.Gif)
            {
                throw new ArgumentException("ImageFormat is not valid for CustomEmoji. Valid types are (Auto, Png, Jpeg, WebP)", nameof(format));
            }
            
            return $"{CdnUrl}/app-assets/{applicationId}/{assetId}.{GetExtension(format, assetId)}";
        }
        
        public static string GetAchievementIconUrl(string applicationId, string achievementId, string iconHash, ImageFormat format = ImageFormat.Auto)
        {
            if (format == ImageFormat.Gif)
            {
                throw new ArgumentException("ImageFormat is not valid for CustomEmoji. Valid types are (Auto, Png, Jpeg, WebP)", nameof(format));
            }
            
            return $"{CdnUrl}/app-assets/{applicationId}/achievements/{achievementId}/icons/{iconHash}.{GetExtension(format, iconHash)}";
        }
        
        public static string GetTeamIconUrl(string teamId, string teamIcon, ImageFormat format = ImageFormat.Auto)
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