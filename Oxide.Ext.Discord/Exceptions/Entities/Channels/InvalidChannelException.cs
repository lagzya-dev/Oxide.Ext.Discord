using Oxide.Ext.Discord.Entities.Channels;

namespace Oxide.Ext.Discord.Exceptions.Entities.Channels
{
    /// <summary>
    /// Represents using an invalid channel
    /// </summary>
    public class InvalidChannelException : BaseDiscordException
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="message">Exception message</param>
        private InvalidChannelException(string message): base(message)
        {
            
        }

        internal static void ThrowIfInvalidName(string name, bool allowNullOrEmpty)
        {
            if (!allowNullOrEmpty && string.IsNullOrEmpty(name))
            {
                throw new InvalidChannelException("Name cannot be less than 1 character");
            }
            
            if (name.Length > 100)
            {
                throw new InvalidChannelException("Name cannot be more than 100 characters");
            }
        }

        internal static void ThrowIfInvalidTopic(string topic, bool allowNullOrEmpty)
        {
            if (!allowNullOrEmpty && string.IsNullOrEmpty(topic))
            {
                throw new InvalidChannelException("Topic cannot be less than 1 character");
            }
            
            if (topic.Length > 1024)
            {
                throw new InvalidChannelException("Topic cannot be more than 1024 characters");
            }
        }
        
        internal static void ThrowIfInvalidRateLimitPerUser(int? rateLimitPerUser)
        {
            if (rateLimitPerUser.HasValue && (rateLimitPerUser.Value < 0 || rateLimitPerUser.Value > 21600))
            {
                throw new InvalidChannelException("RateLimitPerUser cannot be less than 0 or more than 21600");
            }
        }
        
        internal static void ThrowIfInvalidBitRate(int? bitRate)
        {
            if (bitRate.HasValue && (bitRate.Value < 8000  || bitRate.Value > 128000))
            {
                throw new InvalidChannelException("BitRate cannot be less than 8000 or more than 128000");
            }
        }
        
        internal static void ThrowIfInvalidUserLimit(int? userLimit)
        {
            if (userLimit.HasValue && (userLimit.Value < 0  || userLimit.Value > 99 ))
            {
                throw new InvalidChannelException("UserLimit cannot be less than 0 or more than 99");
            }
        }
        
        internal static void ThrowIfInvalidParentId(Discord.Entities.Snowflake? parentId)
        {
            if (parentId.HasValue && !parentId.Value.IsValid())
            {
                throw new InvalidChannelException("ParentId is not a valid snowflake");
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
        
        internal static void ThrowIfChannelToSelf(Discord.Entities.Snowflake userId, DiscordClient client)
        {
            if (userId == client.Bot.BotUser.Id)
            {
                throw new InvalidChannelException("Tried to create a direct message channel to yourself which is not allowed.");
            }
        }
    }
}