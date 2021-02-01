using Newtonsoft.Json;

namespace Oxide.Ext.Discord.Entities.Gatway.Events
{
    /// <summary>
    /// Represents <a href="https://discord.com/developers/docs/topics/gateway#message-reaction-remove-all">Message Reaction Remove All</a>
    /// </summary>
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class MessageReactionRemoveAll
    {
        /// <summary>
        /// The id of the channel
        /// </summary>
        [JsonProperty("channel_id")]
        public string ChannelId { get; set; }

        /// <summary>
        /// The id of the message
        /// </summary>
        [JsonProperty("message_id")]
        public string MessageId { get; set; }
        
        /// <summary>
        /// The id of the guild
        /// </summary>
        [JsonProperty("guild_id")]
        public string GuildId { get; set; }
    }
}
