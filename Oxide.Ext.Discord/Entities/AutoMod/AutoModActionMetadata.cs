using Newtonsoft.Json;

namespace Oxide.Ext.Discord.Entities.AutoMod
{
    /// <summary>
    /// Represents <a href="https://discord.com/developers/docs/resources/auto-moderation#auto-moderation-action-object-action-metadata">Auto Mod Action Metadata</a>
    /// </summary>
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class AutoModActionMetadata
    {
        /// <summary>
        /// Associated Action Type: <see cref="AutoModActionType.SendAlertMessage"/>
        /// Channel to which user content should be logged
        /// </summary>
        [JsonProperty("channel_id")]
        public Snowflake ChannelId { get; set; }
        
        /// <summary>
        /// Associated Action Type: <see cref="AutoModActionType.Timeout"/>
        /// Timeout duration in seconds
        /// </summary>
        [JsonProperty("duration_seconds")]
        public int DurationSeconds { get; set; }
    }
}