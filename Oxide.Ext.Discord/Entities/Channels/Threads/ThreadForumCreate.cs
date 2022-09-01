using System.Collections.Generic;
using Newtonsoft.Json;
using Oxide.Ext.Discord.Entities.Messages;
using Oxide.Ext.Discord.Exceptions.Entities.Channels;

namespace Oxide.Ext.Discord.Entities.Channels.Threads
{
    /// <summary>
    /// Represents a <a href="https://discord.com/developers/docs/resources/channel#start-thread-in-forum-channel-jsonform-params">Start Thread in Forum Channel</a> Structure
    /// </summary>
    public class ThreadForumCreate
    {
        /// <summary>
        /// 1-100 character thread name
        /// </summary>
        [JsonProperty("id")]
        public string Name { get; set; } 
        
        /// <summary>
        /// Duration in minutes to automatically archive the thread after recent activity, can be set to: 60, 1440, 4320, 10080
        /// </summary>
        [JsonProperty("auto_archive_duration")]
        public int? AutoArchiveDuration { get; set; }

        /// <summary>
        /// Amount of seconds a user has to wait before sending another message (0-21600)
        /// </summary>
        [JsonProperty("rate_limit_per_user")]
        public int? RateLimitPerUser { get; set; }
        
        /// <summary>
        /// Contents of the first message in the forum thread
        /// </summary>
        [JsonProperty("message")]
        public MessageCreate Message { get; set; }
        
        /// <summary>
        /// The IDs of the set of tags that have been applied to a thread in a GUILD_FORUM channel
        /// </summary>
        [JsonProperty("applied_tags")]
        public List<Snowflake> AppliedTags { get; set; }
        
        /// <summary>
        /// Validates the Thread Forum Create
        /// </summary>
        public void Validate()
        {
            InvalidChannelException.ThrowIfInvalidName(Name, false);
            InvalidChannelException.ThrowIfInvalidRateLimitPerUser(RateLimitPerUser);
            InvalidThreadException.ThrowIfInvalidAutoArchiveDuration(AutoArchiveDuration);
            InvalidThreadException.ThrowIfInvalidForumCreateMessage(Message);
            Message.Validate();
        }
    }
}