using System.Collections.Generic;
using Newtonsoft.Json;

namespace Oxide.Ext.Discord.Entities.Gatway.Events
{
    /// <summary>
    /// Represents <a href="https://discord.com/developers/docs/topics/gateway#message-delete-bulk">Message Delete Bulk</a>
    /// </summary>
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class MessageDeleteBulk
    {
        /// <summary>
        /// The ids of the messages
        /// </summary>
        [JsonProperty("ids")]
        public List<string> Ids { get; set; }

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
