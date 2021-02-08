using Newtonsoft.Json;
using Oxide.Ext.Discord.Entities.Guilds;
using Oxide.Ext.Discord.Helpers.Interfaces;

namespace Oxide.Ext.Discord.Entities.Voice
{
    /// <summary>
    /// Represents <a href="https://discord.com/developers/docs/resources/voice#voice-state-object">Voice State Structure</a>
    /// </summary>
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class VoiceState : IGetEntityId
    {
        /// <summary>
        /// The guild id this voice state is for
        /// </summary>
        [JsonProperty("guild_id")]
        public Snowflake GuildId { get; set; }

        /// <summary>
        /// The channel id this user is connected to
        /// </summary>
        [JsonProperty("channel_id")]
        public Snowflake ChannelId { get; set; }

        /// <summary>
        /// The user id this voice state is for
        /// </summary>
        [JsonProperty("user_id")]
        public Snowflake UserId { get; set; }
        
        /// <summary>
        /// The guild member this voice state is for
        /// </summary>
        [JsonProperty("member")]
        public GuildMember Member { get; set; }

        /// <summary>
        /// The session id for this voice state
        /// </summary>
        [JsonProperty("session_id")]
        public string SessionId { get; set; }

        /// <summary>
        /// Whether this user is deafened by the server
        /// </summary>
        [JsonProperty("deaf")]
        public bool Deaf { get; set; }

        /// <summary>
        /// Whether this user is muted by the server
        /// </summary>
        [JsonProperty("mute")]
        public bool Mute { get; set; }

        /// <summary>
        /// Whether this user is locally deafened
        /// </summary>
        [JsonProperty("self_deaf")]
        public bool SelfDeaf { get; set; }

        /// <summary>
        /// Whether this user is locally muted
        /// </summary>
        [JsonProperty("self_mute")]
        public bool SelfMute { get; set; }
        
        /// <summary>
        /// Whether this user is streaming using "Go Live"
        /// </summary>
        [JsonProperty("self_stream")]
        public bool SelfStream { get; set; }
        
        /// <summary>
        /// Whether this user's camera is enabled
        /// </summary>
        [JsonProperty("self_video")]
        public bool SelfVideo { get; set; }

        /// <summary>
        /// Whether this user is muted by the current user
        /// </summary>
        [JsonProperty("suppress")]
        public bool Suppress { get; set; }

        public Snowflake GetEntityId()
        {
            return UserId;
        }
    }
}
