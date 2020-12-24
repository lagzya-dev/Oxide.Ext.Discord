using System;
using Newtonsoft.Json;

namespace Oxide.Ext.Discord.Entities.Gatway.Events
{
    public class ChannelPinsUpdate
    {
        [JsonProperty("guild_id")]
        public string GuildId { get; set; }
        
        [JsonProperty("channel_id")]
        public string ChannelId { get; set; }

        [JsonProperty("last_pin_timestamp")]
        public DateTime LastPinTimestamp { get; set; }
    }
}
