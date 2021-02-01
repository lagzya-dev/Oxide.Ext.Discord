using Newtonsoft.Json;

namespace Oxide.Ext.Discord.Entities.Channels
{
    /// <summary>
    /// Represents a <a href="https://discord.com/developers/docs/resources/channel#channel-mention-object-channel-mention-structure">Channel Mention Structure</a> in a message 
    /// </summary>
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class ChannelMention
    {
        /// <summary>
        /// ID of the channel
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }
        
        /// <summary>
        /// ID of the guild containing the channel
        /// </summary>
        [JsonProperty("guild_id")]
        public string GuildId { get; set; }
        
        /// <summary>
        /// The type of channel
        /// <see cref="ChannelType"/>
        /// </summary>
        [JsonProperty("type")]
        public ChannelType Type { get; set; }
        
        /// <summary>
        /// The name of the channel
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}