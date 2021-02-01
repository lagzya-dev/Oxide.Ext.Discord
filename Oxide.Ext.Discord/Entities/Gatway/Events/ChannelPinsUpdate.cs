using System;
using Newtonsoft.Json;

namespace Oxide.Ext.Discord.Entities.Gatway.Events
{
    /// <summary>
    /// Represents <a href="https://discord.com/developers/docs/topics/gateway#channel-pins-update">Channel Pins Update</a>
    /// </summary>
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class ChannelPinsUpdate
    {
        /// <summary>
        /// The id of the guild
        /// </summary>
        [JsonProperty("guild_id")]
        public string GuildId { get; set; }
        
        /// <summary>
        /// The id of the channel
        /// </summary>
        [JsonProperty("channel_id")]
        public string ChannelId { get; set; }

        /// <summary>
        /// The time at which the most recent pinned message was pinned
        /// </summary>
        [JsonProperty("last_pin_timestamp")]
        public DateTime LastPinTimestamp { get; set; }
    }
}
