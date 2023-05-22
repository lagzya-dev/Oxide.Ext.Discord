using Oxide.Ext.Discord.Entities;
using Oxide.Ext.Discord.Entities.Channels;

namespace Oxide.Ext.Discord.Exceptions.Entities.Channels
{
    /// <summary>
    /// Represents using an invalid channel
    /// </summary>
    public class InvalidChannelException : BaseDiscordException
    {
        private InvalidChannelException(string message): base(message) { }

        internal static void ThrowIfInvalidName(string name, bool allowNullOrEmpty)
        {
            const int maxLength = 100;
            
            if (!allowNullOrEmpty && string.IsNullOrEmpty(name))
            {
                throw new InvalidChannelException($"{nameof(DiscordChannel)}.{nameof(DiscordChannel.Name)} cannot be less than 1 character");
            }
            
            if (name.Length > maxLength)
            {
                throw new InvalidChannelException($"{nameof(DiscordChannel)}.{nameof(DiscordChannel.Name)} cannot be more than {maxLength} characters");
            }
        }

        internal static void ThrowIfInvalidTopic(string topic, ChannelType type, bool allowNullOrEmpty)
        {
            const int maxForumLength = 4096;
            const int maxTopicLength = 1024;
            
            if (!allowNullOrEmpty && string.IsNullOrEmpty(topic))
            {
                throw new InvalidChannelException($"{nameof(DiscordChannel)}.{nameof(DiscordChannel.Topic)}  cannot be less than 1 character");
            }

            if (type == ChannelType.GuildForum)
            {
                if (topic.Length > maxForumLength)
                {
                    throw new InvalidChannelException($"{nameof(DiscordChannel)}.{nameof(DiscordChannel.Topic)} cannot be more than {maxForumLength} characters for Guild Forum Channels");
                }

                return;
            }
            
            if (topic.Length > maxTopicLength)
            {
                throw new InvalidChannelException($"{nameof(DiscordChannel)}.{nameof(DiscordChannel.Topic)} cannot be more than {maxTopicLength} characters");
            }
        }
        
        internal static void ThrowIfInvalidRateLimitPerUser(int? rateLimitPerUser)
        {
            const int minRateLimit = 0;
            const int maxRateLimit = 21600;
            if (!rateLimitPerUser.HasValue)
            {
                return;
            }
            
            if (rateLimitPerUser.Value < minRateLimit)
            {
                throw new InvalidChannelException($"{nameof(DiscordChannel)}.{nameof(DiscordChannel.RateLimitPerUser)} cannot be less than {minRateLimit}");
            }
                
            if (rateLimitPerUser.Value > maxRateLimit)
            {
                throw new InvalidChannelException($"{nameof(DiscordChannel)}.{nameof(DiscordChannel.RateLimitPerUser)} cannot be more than {maxRateLimit}");
            }
        }
        
        internal static void ThrowIfInvalidBitRate(int? bitRate)
        {
            const int minBitRate = 8000;
            const int maxBitRate = 128000;
            if (!bitRate.HasValue)
            {
                return;
            }
            
            if (bitRate.Value < minBitRate)
            {
                throw new InvalidChannelException($"{nameof(DiscordChannel)}.{nameof(DiscordChannel.Bitrate)} cannot be less than {minBitRate}");
            }
                
            if (bitRate.Value > maxBitRate)
            {
                throw new InvalidChannelException($"{nameof(DiscordChannel)}.{nameof(DiscordChannel.Bitrate)} cannot be more than {maxBitRate}");
            }
        }
        
        internal static void ThrowIfInvalidUserLimit(int? userLimit)
        {
            const int minUserLimit = 0;
            const int maxUserLimit = 99;

            if (!userLimit.HasValue)
            {
                return;
            }
            
            if (userLimit.Value < minUserLimit)
            {
                throw new InvalidChannelException($"{nameof(DiscordChannel)}.{nameof(DiscordChannel.UserLimit)} cannot be less than {minUserLimit}");
            }
                
            if (userLimit.Value > maxUserLimit)
            {
                throw new InvalidChannelException($"{nameof(DiscordChannel)}.{nameof(DiscordChannel.UserLimit)} cannot be more than {maxUserLimit}");
            }
        }
        
        internal static void ThrowIfInvalidParentId(Snowflake? parentId)
        {
            if (parentId.HasValue && !parentId.Value.IsValid())
            {
                throw new InvalidChannelException($"{nameof(DiscordChannel)}.{nameof(DiscordChannel.ParentId)} is not a valid snowflake");
            }
        }
        
        internal static void ThrowIfNotThread(DiscordChannel channel, string message)
        {
            if (channel == null || !channel.IsThreadChannel())
            {
                throw new InvalidChannelException(message);
            }
        }
        
        internal static void ThrowIfNotGuildChannel(DiscordChannel channel, string message)
        {
            if (channel == null || !channel.IsDmChannel())
            {
                throw new InvalidChannelException(message);
            }
        }
        
        internal static void ThrowIfChannelToSelf(Snowflake userId, DiscordClient client)
        {
            if (userId == client.Bot.BotUser.Id)
            {
                throw new InvalidChannelException("Tried to create a direct message channel to yourself which is not allowed.");
            }
        }
    }
}