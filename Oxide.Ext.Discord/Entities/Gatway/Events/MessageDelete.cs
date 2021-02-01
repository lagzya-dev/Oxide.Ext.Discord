using Newtonsoft.Json;

namespace Oxide.Ext.Discord.Entities.Gatway.Events
{
    /// <summary>
    /// Represents <a href="https://discord.com/developers/docs/topics/gateway#message-delete">Message Delete</a>
    /// </summary>
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class MessageDelete
    {
        /// <summary>
        /// The id of the message
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }

        /// <summary>
        /// The id of the channel
        /// </summary>
        [JsonProperty("channel_id")]
        public string ChannelId { get; set; }
        
        /// <summary>
        /// The id of the guild
        /// </summary>
        [JsonProperty("guild_id")]
        public string GuildId { get; set; }
    }
}
